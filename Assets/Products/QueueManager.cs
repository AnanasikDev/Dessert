using NaughtyAttributes;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Tooltip("Value 0-1 after which a new client will appear")]
    public float threshold;
    
    public float curveFactor = 1.0f;
    public float curvePower = 1.0f;

    public float updateDelay = 0.1f;
    [Tooltip("Minimum time between two customers emergence")]
    public float minCustomerDelay = 0.9f;
    private float lastTime;

    [ShowNativeProperty] public int queueLength { get; private set; }

    [ReadOnly] public AnimationCurve debug;
    [ReadOnly] public float currentValue;
    public void Init()
    {
        queueLength = 0;
        lastTime = Time.time;
        InvokeRepeating("UpdateQueue", 0, updateDelay);
    }

    public void UpdateQueue()
    {
        currentValue = CalculateValue();
        debug.AddKey(Time.time, currentValue);

        if (currentValue > threshold && Time.time - lastTime > minCustomerDelay)
        {
            lastTime = Time.time;
            AddCustomer();
        }
    }

    private float CalculateValue()
    {
        return Mathf.Pow(Mathf.PerlinNoise1D(Time.time * curveFactor), curvePower);
    }

    private void AddCustomer()
    {
        Debug.Log("New customer!");
        queueLength++;
    }
}
