using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler {

    [System.Serializable]
    public class OnClick : UnityEvent { }

    [SerializeField]
    public OnClick onChangeEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        onChangeEvent.Invoke();
    }
}
