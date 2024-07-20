using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class DessertPlate : MonoBehaviour
{
    [SerializeField] Transform handler;
    [SerializeField] new Image renderer;

    [ReadOnly] public DessertSO dessert;

    public void SetDessert(DessertSO dessert)
    {
        this.dessert = dessert;
        renderer.sprite = dessert == null ? null : dessert.Sprite;
    }
}