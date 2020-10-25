using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_GameOver : MonoBehaviour
{

    private Button[] buttons;

    void Awake()
    {
      buttons = GetComponentsInChildren<Button>();
      HideButtons();
    }

    public void HideButtons()
    {
      foreach(var button in buttons)
      {
        button.gameObject.SetActive(false);
      }
    }

    public void ShowButtons ()
    {
      foreach(var button in buttons)
      {
        button.gameObject.SetActive(true);
      }
    }

    public void ExitToMenu()
    {
      SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
      SceneManager.LoadScene("Level_1");
    }
}
