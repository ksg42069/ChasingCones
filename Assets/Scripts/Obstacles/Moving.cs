using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moving : MonoBehaviour
{
    [SerializeField] private bool moveX;
    [SerializeField] private bool moveY;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float delay = 0f;
    
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 tempPos;
    private bool goingToEnd;

    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(moveX ? xOffset : 0, moveY ? yOffset : 0, 0);
        goingToEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 1f)
        {
            if (timer >= 0f)
            {
                transform.position = Vector3.Lerp(startPos, endPos, timer);
                timer += Time.deltaTime * speed * 0.01f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = -delay;
            if (goingToEnd)
            {
                tempPos = startPos;
                startPos = endPos;
                endPos = tempPos;
                goingToEnd = false;
            }
            else
            {
                tempPos = endPos;
                endPos = startPos;
                startPos = tempPos;
                goingToEnd = true;
            }
        }
    }
}
