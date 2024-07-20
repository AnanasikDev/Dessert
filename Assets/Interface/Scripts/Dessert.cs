using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dessert : MonoBehaviour
{
    private DessertSO dessert;
    [SerializeField]
    private Image dessertImage;

    public void Initialize(DessertSO d)
    {
        if (dessert == null)
        {
            dessert = d;
            dessertImage.sprite = d.Sprite;
        }
    }

}
