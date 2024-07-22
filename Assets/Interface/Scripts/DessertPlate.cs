using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class DessertPlate : MonoBehaviour
{
    [SerializeField] Transform handler;
    [SerializeField] new Image renderer;

    [ReadOnly] public DessertSO dessert;

    public void Init()
    {
        Customer.OnQuitEvent += ResetPlate;
    }

    public void ResetPlate()
    {
        SetDessert(null);
    }
    public void SetDessert(DessertSO dessert)
    {
        this.dessert = dessert;
        if (dessert == null) renderer.gameObject.SetActive(false);
        else renderer.gameObject.SetActive(true);
        renderer.sprite = dessert == null ? null : dessert.Sprite;
    }

}