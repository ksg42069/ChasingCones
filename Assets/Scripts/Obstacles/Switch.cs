using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    Animation anim;
    public GameObject wave;
    public Color buttonColor;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        if(wave.GetComponent<Renderer>() != null)
            wave.GetComponent<Renderer>().material.color = buttonColor;
        else
        {
            Renderer[] waveChilren = wave.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in waveChilren)
            {
                r.material.color = buttonColor;
            }
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = buttonColor;
    }
    public void PlaySwitch()
    {
        anim.Play("Switch");
        wave.SetActive(false);
    }
}
