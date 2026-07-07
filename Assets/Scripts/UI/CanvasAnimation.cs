using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasAnimation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void LevelSelect()
    {
        anim.Play("LevelSelect");
    }
    public void LevelBack()
    {
        anim.Play("LevelSelectBack");
    }
    public void GameOver()
    {
        anim.Play("Over");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
