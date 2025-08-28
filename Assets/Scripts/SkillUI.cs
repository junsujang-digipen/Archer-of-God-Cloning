using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int SkillIndex = 0;
    Button _button;
    Controller _controller;
    [SerializeField]GameObject _AimImage;
    void Start()
    {
        _controller = FindAnyObjectByType<Controller>();
        _button = GetComponent<Button>();
        // _button.onClick.AddListener(() => _controller.PlaySkill(SkillIndex));
        _AimImage.SetActive(false);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        _AimImage.SetActive(true);
        _AimImage.GetComponent<Image>().raycastTarget = false;
        _controller.PlaySkill(SkillIndex);
    }

    // 드래그 중
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        currentPos.z = 0;
        _AimImage.transform.position = currentPos;
        _controller.Aiming(currentPos);
    }

    // 드래그 끝
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _AimImage.SetActive(false);
        _AimImage.GetComponent<Image>().raycastTarget = true;
        _controller.Shot();
    }
}
