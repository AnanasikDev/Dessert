using UnityEngine;
using UnityEngine.UI;

public class Dessert : MonoBehaviour
{
    [HideInInspector] public DessertSO data;
    [SerializeField] private Image dessertImage;

    public void Initialize(DessertSO d)
    {
        if (data == null)
        {
            data = d;
            dessertImage.sprite = d.Sprite;
        }
    }
}
