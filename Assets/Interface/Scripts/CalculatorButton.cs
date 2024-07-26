using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CalculatorButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPointerDownEvent;
    public UnityEvent onPointerUpEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUpEvent?.Invoke();
    }
}
