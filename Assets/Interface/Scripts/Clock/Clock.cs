using NaughtyAttributes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private int daySeconds = 250;

    [SerializeField] private TextMeshProUGUI dayText;
    private int dayIndex;

    private float time;

    [SerializeField] private Animator animator;
    [ReadOnly] public bool isPlaying = true;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button button;

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (!isPlaying) return;

        time += Time.deltaTime;
        arrow.transform.Rotate(0, 0, -360f / daySeconds * Time.deltaTime);

        if (time >= daySeconds)
        {
            EndDay();
        }
    }

    private void EndDay()
    {
        audioSource.Play();
        IEnumerator animate()
        {
            isPlaying = false;
            animator.SetTrigger("end_day");
            yield return new WaitForSeconds(0.4f);
            button.interactable = true;
        }
        StartCoroutine(animate());
    }

    public void LoadNextDay()
    {
        audioSource.Play();
        IEnumerator animate()
        {
            button.interactable = false;
            animator.SetTrigger("new_day");
            yield return new WaitForSeconds(0.4f);
            isPlaying = true;
        }
        StartCoroutine(animate());
    }
}
