using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject GameplayUI;
    public GameObject MainCamera;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

        private void Update()
    {
        if (starterAssetsInputs.pause)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        GameplayUI.SetActive(false);
        MainCamera.SetActive(false);
        GetComponent<ThirdPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        GameplayUI.SetActive(true);
        starterAssetsInputs.pause = false;
        MainCamera.SetActive(true);
        GetComponent<ThirdPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
