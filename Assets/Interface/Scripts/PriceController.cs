using System;
using TMPro;
using UnityEngine;

public class PriceController : MonoBehaviour
{
    private int _price = 0;

    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    public delegate void OnPriceChanged(int price);
    public OnPriceChanged priceChanged;

    public Action<int> OnPriceConfirmed;

    public void Init()
    {
        UpdatePriceText();
    }

    public void ConfirmPrice()
    {
        OnPriceConfirmed?.Invoke(_price);
    }

    public void changePrice(int dec)
    {

        if (dec >= 0)
        {
            _price = (_price * 10) + dec;
        }
        else
        {
            _price /= 10;
        }
        if (_price > 10000)
            _price = 9999;
        priceChanged?.Invoke(_price);
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        _textMeshPro.text = _price.ToString();
    }
}
