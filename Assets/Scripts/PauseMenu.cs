using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
    public SaveManager saveManager;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseState()
    {
        paused = !paused;
        if (paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void OnReturnButton(){
        saveManager.SaveGame();
        Resume();
        SceneManager.LoadScene(0);
    }
    public void OnNotSaveReturnButton(){
        Resume();
        SceneManager.LoadScene(0);
    }


}
