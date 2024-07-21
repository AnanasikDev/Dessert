using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Tooltip("Value 0-1 after which a new client will appear")]
    public float threshold;
    public int maxQueueLength;

    public float curveFactor = 1.0f;
    public float curvePower = 1.0f;
    public float peacksThreshold = 0.9f;

    public Vector2 randomNoise = new Vector2(-0.125f, 0.125f);

    public float updateDelay = 0.1f;
    [Tooltip("Minimum time between two customers emergence")]
    public float minCustomerDelay = 0.9f;
    private float lastTime;

    public Vector2 firstCustomerPosition;
    public Vector2 nextCustomerPositionShift;

    [ReadOnly] public Queue<Customer> queue = new Queue<Customer>();
    public Customer current { get { return queue.Count == 0 ? null : queue.Peek(); } }

    public AnimationCurve debug;
    [ReadOnly] public float currentValue;

    public void Init()
    {
        lastTime = Time.time;
        Customer.OnQuitEvent += RemoveCustomer;
        InvokeRepeating("UpdateQueue", 0, updateDelay);
    }

    public void UpdateQueue()
    {
        if (!Scripts.Clock.isPlaying) return;

        currentValue = Random.value >= peacksThreshold ? 1 : CalculateValue() + Random.Range(randomNoise.x, randomNoise.y);
        debug.AddKey(Time.time, currentValue);

        if (currentValue > threshold && Time.time - lastTime > minCustomerDelay && queue.Count < maxQueueLength)
        {
            lastTime = Time.time;
            AddCustomer();
        }
    }

    private float CalculateValue()
    {
        return Mathf.Pow(Mathf.PerlinNoise1D(Time.time * curveFactor), curvePower);
    }

    [Button]
    public void AddCustomer()
    {
        Debug.Log("New customer!");
        var customer = Customer.Create(Scripts.CustomerBuilder.BuildCustomer(), queue.Count);
        customer.transform.position = firstCustomerPosition + queue.Count * nextCustomerPositionShift;
        queue.Enqueue(customer);
    }

    public void RemoveCustomer()
    {
        if (queue.Count == 0) return;

        queue.Dequeue(); // already quit just before
        foreach (var customer in queue)
        {
            customer.MoveForward();
        }
    }

    public void ForceClear()
    {
        foreach (var customer in Scripts.QueueManager.queue)
        {
            customer.ForceQuit();
        }
        queue.Clear();
    }
}
