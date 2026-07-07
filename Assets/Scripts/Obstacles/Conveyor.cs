using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public int direction;
    public GameObject arrow;

    private void Start()
    {
        ShowDir(direction);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Line"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * Time.deltaTime, ForceMode2D.Force);
        }
    }
    void ShowDir(int value)
    {
        if(value > 0)
        {
            Vector3 rot = new Vector3(0, 0, 180);
            arrow.transform.eulerAngles = rot;
        }
    }
}
