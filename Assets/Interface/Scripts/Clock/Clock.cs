using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private int daySeconds = 250;

    private void Update()
    {
        arrow.transform.Rotate(0, 0, -360f/daySeconds*Time.deltaTime);
    }
}
