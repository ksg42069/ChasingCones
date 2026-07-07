using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite happy;
    public Sprite hurt;
    public Sprite smile;
    public Sprite weeee;

    SpriteRenderer spRend;
    SpriteRenderer bgRend;
    SpriteRenderer levelIndicatorRend;
    Animator anim;
    Rigidbody2D rb;

    public LevelManager levelM;
    public LinesManager lineM;
    public GameObject canvas;
    public TextMeshProUGUI moveCount;
    public bool hitEnd, hitObj;
    public bool outOfBound;
    Color32 cSmile, cHappy, cHurt;

    // Start is called before the first frame update
    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
        bgRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        levelIndicatorRend = transform.GetChild(1).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cSmile = bgRend.color;
        cHappy = new Color32(255, 175, 175, 255);
        cHurt = new Color32(175, 255, 175, 255);
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        hitEnd = false;
        hitObj = false;
        outOfBound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitEnd)
        {
            spRend.sprite = happy;
            bgRend.color = cHappy;
            levelIndicatorRend.color = cHappy;
        }
        else
        {
            if (hitObj)
            {
                spRend.sprite = hurt;
                bgRend.color = cHurt;
                levelIndicatorRend.color = cHurt;
            }  
            else
            {
                if (rb.linearVelocity.magnitude > 1.5f)
                    spRend.sprite = weeee;
                else
                    spRend.sprite = smile;

                bgRend.color = cSmile;
            }
        }
        if((transform.position.x < -2.5f ||  transform.position.y < - 5.3f || transform.position.x > 2.5f) && !outOfBound)
        {
            SetBodyKinematic();
            lineM.KinematicLines();
            outOfBound = true;
            levelIndicatorRend.color = cSmile;
            StartCoroutine(AnimDelay("Lose"));
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Endeded");
            moveCount.text = "MOVES USED - " + lineM.GetLines().ToString();
            hitEnd = true;
            SetBodyKinematic();
            levelM.SetMaxLevel();
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
                StartCoroutine(AnimDelay("Over"));
            else
                StartCoroutine(AnimDelay("Win"));
        }
        if (collision.gameObject.CompareTag("Switch"))
        {
            collision.gameObject.GetComponent<Switch>().PlaySwitch();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hitObj = true;
            SetBodyKinematic();
            lineM.KinematicLines();
            StartCoroutine(AnimDelay("Lose"));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void SetBodyDynamic()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void SetBodyKinematic()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    IEnumerator AnimDelay(string state)
    {
        LevelManager.instance.LevelOver();
        anim.Play("lvlOver");
        yield return new WaitForSeconds(1.5f);
        canvas.GetComponent<Animator>().Play(state);
    }
}
