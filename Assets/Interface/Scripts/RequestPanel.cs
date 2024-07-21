using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private Transform handler;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image dessertImage;
    [SerializeField] private Image responseImage;

    public ResponseUI[] responseUIs;
    public Dictionary<ResponseStatus, Sprite> reponseDict = new Dictionary<ResponseStatus, Sprite>();

    public void Init()
    {
        foreach (var ui in responseUIs)
        {
            reponseDict.Add(ui.status, ui.sprite);
        }

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
        dessertImage.sprite = sprite;
    }

    public void ResetPanel()
    {
        SetText("");
        SetImage(null);
        ClearResponse();
        handler.gameObject.SetActive(false);
    }

    public void ClearResponse()
    {
        responseImage.sprite = reponseDict[ResponseStatus.Pending];
    }
    public void DrawResponse(Customer customer, Response response)
    {
        responseImage.sprite = reponseDict[response.status];
    }
}

[System.Serializable]
public struct ResponseUI
{
    public Sprite sprite;
    public ResponseStatus status;
}