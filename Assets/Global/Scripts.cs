using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Interface for code to refer to all
/// static scripts in the project
/// </summary>
public class Scripts : MonoBehaviour
{
    [SerializeField][Required] public Customer _CharacterPrefab;
    public static Customer CharacterPrefab;

    [SerializeField][Required] public QueueManager _QueueManager;
    public static QueueManager QueueManager;

    [SerializeField][Required] public CustomerBuilder _CustomerBuilder;
    public static CustomerBuilder CustomerBuilder;

    [SerializeField][Required] public DessertManager _DessertManager;
    public static DessertManager DessertManager;

    [SerializeField][Required] public PriceController _PriceController;
    public static PriceController PriceController;

    [SerializeField][Required] public DessertPlate _DessertPlate;
    public static DessertPlate DessertPlate;

    [SerializeField][Required] public RequestPanel _RequestPanel;
    public static RequestPanel RequestPanel;

    [SerializeField][Required] public PriceList _PriceList;
    public static PriceList PriceList;

    private void Start()
    {
        CharacterPrefab = _CharacterPrefab;
        QueueManager = _QueueManager;
        CustomerBuilder = _CustomerBuilder;
        DessertManager = _DessertManager;
        PriceController = _PriceController;
        RequestPanel = _RequestPanel;
        DessertPlate = _DessertPlate;
        PriceList = _PriceList;

        CustomerBuilder.Init();
        PriceController.Init();
        QueueManager.Init();
        DessertPlate.Init();
        RequestPanel.Init();
        PriceList.Init();
    }
}
