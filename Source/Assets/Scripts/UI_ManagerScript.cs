using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ManagerScript : MonoBehaviour {
    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("mainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }


}
