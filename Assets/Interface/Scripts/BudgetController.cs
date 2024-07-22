using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class BudgetController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI budgetText;
    [ShowNativeProperty] public int budget { get; private set; }
    [ShowNativeProperty] public int dayEarnings { get; private set; }
    [SerializeField] private int defaultBudget = 0;

    public void Init()
    {
        budget = defaultBudget;
        Customer.OnResponseEvent += OnResponse;
    }
    
    public void OnResponse(Customer customer, Response response)
    {
        if (response.status == ResponseStatus.Accepted)
        {
            AddToBudget(Scripts.PriceController.price);
        }
    }

    public void ClearDayEarnings() => dayEarnings = 0;
    public void AddToBudget(int value)
    {
        if (value > 9999)
        {
            value = 9999;
        }

        budget += value;
        dayEarnings += value;

        budgetText.text = budget.ToString();
    }
}