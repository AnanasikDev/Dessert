using System;
using TMPro;
using UnityEngine;

public class PriceController : MonoBehaviour
{
    public int price { get; private set; }

    [SerializeField] private TextMeshProUGUI priceText;
    public delegate void OnPriceChanged(int price);
    public OnPriceChanged priceChanged;

    //public Action<int> OnPriceConfirmed;

    public void Init()
    {
        UpdatePriceText();
        Customer.OnQuitEvent += ResetPrice;
    }

    public void ConfirmPrice()
    {
        //OnPriceConfirmed?.Invoke(_price);
        Scripts.QueueManager.current?.GetResponse(Scripts.DessertPlate.dessert, price);
    }

    public void ResetPrice()
    {
        price = 0;
        UpdatePriceText();
    }

    public void changePrice(int dec)
    {
        if (dec >= 0)
        {
            price = (price * 10) + dec;
        }
        else
        {
            price /= 10;
        }
        if (price > 10000)
            price = 9999;
        priceChanged?.Invoke(price);
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        priceText.text = price.ToString();
    }
}
