using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonWrapper : MonoBehaviour,IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent _hoverEnter;
    public UnityEvent _hoverExit;
    public UnityEvent RisingClick;
    [SerializeField] private Image _imgComponent;
    [SerializeField] private Sprite _default, _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _imgComponent.sprite = _pressed;
        SoundManager.Instance.PlayRandomHarp();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverExit?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _imgComponent.sprite = _default;
        SoundManager.Instance.PlayRandomHarp();
        RisingClick?.Invoke();
    }
}
