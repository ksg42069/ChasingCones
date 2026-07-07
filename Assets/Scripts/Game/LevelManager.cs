using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject retryBtn;
    [SerializeField] private GameObject backBtn;

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.GetInt("UnlockedLevel") == 0)
            PlayerPrefs.SetInt("UnlockedLevel", 1);
        
        //PlayerPrefs.SetInt("UnlockedLevel", 1);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
    void Start()
    {
        if(retryBtn != null)
            retryBtn.SetActive(true);
        if(backBtn != null)
            backBtn.SetActive(true);
    }
    public void SetLevel(int value)
    {
        SceneManager.LoadScene(value);
    }
    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetMaxLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetMaxLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        RestartLevel();
    }

    public void LevelOver()
    {
        retryBtn.SetActive(false);
        backBtn.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
