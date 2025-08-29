using System;
using TMPro;
using Unity.VisualScripting;
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
    SkillPlayer _skillPlayer;
    void Start()
    {
        _controller = FindAnyObjectByType<Controller>();
        _countDownText = _LockImage.GetComponentInChildren<TextMeshProUGUI>();
        _AimImage.SetActive(false);
        _LockImage.SetActive(false);
        _skillPlayer = _controller.Target.GetComponent<SkillSet>().SkillPlayers[SkillIndex];
        GetComponentInChildren<TextMeshProUGUI>().text = _skillPlayer.Skill.SkillName;
    }
    void Update()
    {
        // GetComponent<Button>().
        if (_skillPlayer.IsAble == true)
        {
            _LockImage.SetActive(false);
        }
        else
        {
            _countDownText.text = "" + (int)Math.Ceiling(_skillPlayer.CooldownTimer);
        }
    }
    bool IsLocked => _LockImage.activeSelf;
    bool IsSkillPlayable => _controller.IsSkillable;
    bool IsAiming => _AimImage.activeSelf;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (IsLocked || !IsSkillPlayable) return;
        _AimImage.SetActive(true);
        // _AimImage.GetComponent<Animation>().Play();
        _AimImage.GetComponent<Image>().raycastTarget = false;
        _controller.PlaySkill(SkillIndex);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (IsLocked || !IsAiming) return;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        currentPos.z = 0;
        _AimImage.transform.position = currentPos;
        _controller.Aiming(currentPos);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (IsLocked || !IsAiming) return;
        _AimImage.SetActive(false);
        _AimImage.GetComponent<Image>().raycastTarget = true;
        _controller.Shot();
        _LockImage.SetActive(true);
    }
}
