using UnityEngine;

[CreateAssetMenu(fileName = "Dessert Data", menuName = "ScriptableObjects/Dessert", order = 1)]
public class DessertSO : ScriptableObject
{
    [Header("Parameters")]
    [SerializeField]
    private string name;
    [SerializeField]
    private int price;
    [SerializeField]
    private Sprite sprite;

    public string Name { get => name; }
    public int Price { get => price; }
    public Sprite Sprite { get => sprite; }
}
