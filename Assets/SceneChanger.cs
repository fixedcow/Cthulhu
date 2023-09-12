using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneChanger : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Woo");
    }
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}
