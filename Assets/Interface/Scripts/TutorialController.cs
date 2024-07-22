using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject pauseUI; 
    private bool isPaused = false;

    private void Awake()
    {
        
        pauseUI.SetActive(false);
    }

    private void Start()
    {
        
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
       
        yield return new WaitForEndOfFrame();

        
        pauseUI.SetActive(true);
        PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
    }

    private void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        pauseUI.SetActive(false); 
    }

    private void Update()
    {
        if (isPaused && Input.anyKeyDown)
        {
            UnpauseGame();
        }
    }
}
