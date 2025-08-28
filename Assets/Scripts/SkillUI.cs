using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int SkillIndex = 0;
    Controller _controller;
    [SerializeField] GameObject _AimImage;
    [SerializeField] GameObject _LockImage;
    TextMeshProUGUI _countDownText;
    LineRenderer _lineRenderer;
    SkillPlayer _skillPlayer;
    void Start()
    {
        _controller = FindAnyObjectByType<Controller>();
        _countDownText = _LockImage.GetComponentInChildren<TextMeshProUGUI>();
        _AimImage.SetActive(false);
        _LockImage.SetActive(false);
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _skillPlayer = _controller.Target.GetComponent<SkillSet>().SkillPlayers[SkillIndex];
    }
    void Update()
    {
        if (_skillPlayer.IsAble == true)
        {
            _LockImage.SetActive(false);
        }
        else
        {
            _countDownText.text = "" + (int)Math.Ceiling(_skillPlayer.CooldownTimer);
        }
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (_LockImage.activeSelf == true) return;
        _AimImage.SetActive(true);
        _AimImage.GetComponent<Image>().raycastTarget = false;
        _controller.PlaySkill(SkillIndex);
        _lineRenderer.enabled = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (_LockImage.activeSelf == true || _AimImage.activeSelf == false) return;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        currentPos.z = 0;
        _AimImage.transform.position = currentPos;
        _controller.Aiming(currentPos);
        DrawTrajectory(_controller.Target.transform.position, currentPos);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (_LockImage.activeSelf == true || _AimImage.activeSelf == false) return;
        _AimImage.SetActive(false);
        _AimImage.GetComponent<Image>().raycastTarget = true;
        _controller.Shot();
        _LockImage.SetActive(true);
        _lineRenderer.enabled = false;
    }
    void DrawTrajectory(Vector3 start, Vector3 target) // 임시
    {
        if (_lineRenderer == null) return;

        int segmentCount = 30;
        float flightTime = 1.0f; // 필요에 따라 조절
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        Vector2 velocity = new Vector2(
            (target.x - start.x) / flightTime,
            (target.y - start.y + 0.5f * gravity * flightTime * flightTime) / flightTime
        );

        _lineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            float t = i * flightTime / (segmentCount - 1);
            Vector3 pos = start + (Vector3)(velocity * t);
            pos.y -= 0.5f * gravity * t * t;
            _lineRenderer.SetPosition(i, pos);
        }
    }
}
