using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _imgComponent;
    [SerializeField] private Sprite _default, _pressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        _imgComponent.sprite = _pressed;
        SoundManager.Instance.PlayRandomHarp();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _imgComponent.sprite = _default;
        SoundManager.Instance.PlayRandomHarp();
    }
}
