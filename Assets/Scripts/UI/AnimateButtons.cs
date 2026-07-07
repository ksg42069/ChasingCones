using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButtons : MonoBehaviour
{
    Transform oriPos;
    RectTransform originalPos;

    public float changeInterval;
    public float maxMove;
    public float maxRot;

    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform;
        originalPos = GetComponent<RectTransform>();

        InvokeRepeating("AnimatePos", 0.01f, changeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AnimatePos()
    {
        Vector3 newPos = new Vector3(Random.Range(-maxMove, maxMove), Random.Range(-maxMove, maxMove), 0);
        Vector3 newRot = new Vector3(0, 0, Random.Range(-maxRot, maxRot));

        transform.position = oriPos.position + newPos;
        transform.eulerAngles = newRot;
    }
}
