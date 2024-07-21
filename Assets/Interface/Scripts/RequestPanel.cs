using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private Transform handler;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image dessert;

    public void Init()
    {
        Customer.OnQuitEvent += ResetPanel;
        Customer.OnRequestEvent += SetPanel;
    }

    private void SetPanel(Customer customer, Request request)
    {
        handler.gameObject.SetActive(true);
        SetText(customer.data.randomRequestTexts.GetRandom());
        SetImage(request.dessert.Sprite);
    }

    public void SetText(string str)
    {
        text.text = str;
    }
    public void SetImage(Sprite sprite)
    {
        dessert.sprite = sprite;
    }

    public void ResetPanel()
    {
        SetText("");
        SetImage(null);
        handler.gameObject.SetActive(false);
    }
}