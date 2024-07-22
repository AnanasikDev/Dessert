using System.Linq;
using UnityEngine;

public class CustomerBuilder : MonoBehaviour
{
    [SerializeField] private CustomerData[] datas;

    public void Init()
    {
        datas = datas.OrderBy(d => d.frequency).ToArray();
        foreach (var data in datas)
        {
            data.PreInit();
        }
    }
    public CustomerData BuildCustomer()
    {
        float rand = Random.value;

        float value = 0;

        // 0.1 0.2 0.3 0.4
        // 0.1 0.3 0.6 1.0

        foreach (var data in datas)
        {
            value += data.frequency;
            if (value >= rand)
            {
                return data;
            }
        }

        return datas.GetRandom();
    }
}