using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    public Sprite locked;
    public Sprite unlocked;

    int unlockLevelIndex;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("UnlockedLevel", 1);
        unlockLevelIndex = PlayerPrefs.GetInt("UnlockedLevel");
        SetIcons();
    }
    void SetIcons()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i >= unlockLevelIndex)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = locked;
                transform.GetChild(i).GetComponent<Button>().interactable = false;
                transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().sprite = unlocked;
                transform.GetChild(i).GetComponent<Button>().interactable = true;
                transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
