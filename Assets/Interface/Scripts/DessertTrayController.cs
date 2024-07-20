using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DessertTrayController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private DessertSO dessert;
    [SerializeField]
    private Dessert dessertPrefab;
    [SerializeField]
    private Image dessertImage;
    [SerializeField]
    private Transform dessertParent;
    [SerializeField]
    private RectTransform targetArea;

    private Dessert currentDessert;
    
    public delegate void DessertEventHandler(DessertSO dessert);
    public static DessertEventHandler OnSelect;
    void Start()
    {
        dessertImage.sprite = dessert.Sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentDessert = Instantiate<Dessert>(dessertPrefab, transform.position, transform.rotation, dessertParent);
        currentDessert.Initialize(dessert);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDessert.gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(targetArea, eventData.position))
        {
            Debug.Log("Given");
            OnSelect?.Invoke(dessert);
        }
        Destroy(currentDessert.gameObject);
    }
}
