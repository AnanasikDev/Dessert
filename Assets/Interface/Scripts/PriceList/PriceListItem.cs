using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceListItem : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public Image dessertImage;

    public void SetPrice(int price) => priceText.text = price.ToString();
    public void SetImage(Sprite sprite) => dessertImage.sprite = sprite;
}
