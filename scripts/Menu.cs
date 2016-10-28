using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Canvas MainCanvas, OptionsCanvas;
    public GameObject loadingImage;

    void awake()
    {
        OptionsCanvas.enabled = false;
    }

    public void OptionsOn()
    {
        OptionsCanvas.enabled = true;

        MainCanvas.enabled = false;
    }

    public void MainOn()
    {
        MainCanvas.enabled = true;

        OptionsCanvas.enabled = false;
    }

   public void loadOn(string level)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
