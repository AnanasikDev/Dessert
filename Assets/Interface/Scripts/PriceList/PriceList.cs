using UnityEngine;

public class PriceList : MonoBehaviour
{
    [SerializeField] private PriceListItem itemPrefab;
    [SerializeField] private Transform handler;
    [SerializeField] private float verticalDistance;

    public void Init()
    {
        var datas = Scripts.DessertManager.dessertsDatas;
        for (int i = 0; i < datas.Length; i++)
        {
            var item = Instantiate(itemPrefab, handler);
            item.transform.position = handler.transform.position - Vector3.up * verticalDistance * i;
            item.SetImage(datas[i].Sprite);
            item.SetPrice(datas[i].Price);
        }
    }
}
