using NaughtyAttributes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private AudioClip audioEndDay;
    [SerializeField] private AudioClip audioNewDay;

    [SerializeField] private TextMeshProUGUI earnedText;
    [SerializeField] private TextMeshProUGUI spendsText;
    [SerializeField] private TextMeshProUGUI budgetText;

    [SerializeField] private Button newDayButton;
    [SerializeField] private Button restartButton;

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
        audioSource.PlayOneShot(audioEndDay);
        IEnumerator animate()
        {
            isPlaying = false;
            animator.SetTrigger("end_day");
            yield return new WaitForSeconds(0.4f);

            Scripts.QueueManager.ForceClear();

            earnedText.gameObject.SetActive(true);
            earnedText.text = "Earned this day: " + Scripts.BudgetController.dayEarnings.ToString();

            yield return new WaitForSeconds(0.25f);

            spendsText.gameObject.SetActive(true);
            spendsText.text = "Spends: " + 500;

            yield return new WaitForSeconds(0.25f);

            budgetText.gameObject.SetActive(true);
            Scripts.BudgetController.AddToBudget(-500);
            budgetText.text = "Total budget: " + Scripts.BudgetController.budget.ToString();

            if (Scripts.BudgetController.budget < 0)
            {
                newDayButton.interactable = false;
            }
            else
            {
                newDayButton.interactable = true;
            }
            

            Scripts.BudgetController.ClearDayEarnings();
        }
        StartCoroutine(animate());
    }

    public void LoadNextDay()
    {
        audioSource.PlayOneShot(audioNewDay);
        IEnumerator animate()
        {
            dayIndex++;
            dayText.text = "Day " + dayIndex;
            newDayButton.interactable = false;
            budgetText.gameObject.SetActive(false);
            earnedText.gameObject.SetActive(false);
            animator.SetTrigger("new_day");
            yield return new WaitForSeconds(0.4f);
            isPlaying = true;
            time = 0;
        }
        StartCoroutine(animate());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
