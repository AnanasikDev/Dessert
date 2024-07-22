using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject playButton;
    private int index = 0;

    private void Start()
    {
        texts[index].gameObject.SetActive(true);
    }

    public void Next()
    {
        if (index >= texts.Length - 1)
        {
            playButton.SetActive(true);
            return;
        }

        texts[index].gameObject.SetActive(false);
        index++;
        texts[index].gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
