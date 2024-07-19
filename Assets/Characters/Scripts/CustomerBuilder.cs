using UnityEngine;

public class CustomerBuilder : MonoBehaviour
{
    [SerializeField] private CustomerData[] datas;

    public CustomerData BuildCustomer() => datas.GetRandom();
}