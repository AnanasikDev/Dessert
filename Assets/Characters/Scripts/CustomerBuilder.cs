using UnityEngine;

public class CustomerBuilder : MonoBehaviour
{
    [SerializeField] private CustomerData[] datas;

    public void Init()
    {
        foreach (var data in datas)
        {
            data.PreInit();
        }
    }
    public CustomerData BuildCustomer() => datas.GetRandom();
}