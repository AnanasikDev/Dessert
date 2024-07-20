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

    [SerializeField][Required] public ProductManager _ProductManager;
    public static ProductManager ProductManager;

    private void Start()
    {
        CharacterPrefab = _CharacterPrefab;
        QueueManager = _QueueManager;
        CustomerBuilder = _CustomerBuilder;
        ProductManager = _ProductManager;

        QueueManager.Init();
    }
}
