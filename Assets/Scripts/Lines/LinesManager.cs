using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinesManager : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask dontDrawOver;
    int layerIndex;

    [Header("Stats")]
    public float pointMinDist;
    public float lineWidth;
    public Gradient lineColor;

    Line currentLine;
    bool overSomething;
    PlayerBehaviour player;
    public List<GameObject> movableShit = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("DontDrawOver");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        GameObject[] movableStuff = GameObject.FindGameObjectsWithTag("Movable");
        for(int i = 0; i < movableStuff.Length; i++)
        {
            movableShit.Add(movableStuff[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.hitEnd && !player.outOfBound)
        {
            if (Input.GetMouseButtonDown(0))
                BeginDraw();

            if (currentLine != null)
                Draw();

            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics2D.GetRayIntersectionAll(ray, 1500f);

                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.layer == layerIndex)
                    {
                        overSomething = true;
                    }
                    else
                        overSomething = false;
                }
            }

            if (Input.GetMouseButtonUp(0))
                EndDraw();
        }
        else
        {
            KinematicLines();         
        }
        
    }
    public void BeginDraw()
    {
        currentLine = Instantiate(linePrefab,this.transform).GetComponent<Line>();

        currentLine.SetLineColor(lineColor);
        currentLine.SetLineWidth(lineWidth);
        currentLine.SetMinDistance(pointMinDist);
        currentLine.UsePhysics(false);
    }
    public void Draw()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (overSomething)
        {
            EndDraw();
        }
        else
            currentLine.AddPoint(mousePos);
    }
    public void EndDraw()
    {
        if(currentLine != null)
        {
            if(currentLine.pointCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = layerIndex;
                currentLine.SetMass();
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
        overSomething = false;
        player.SetBodyDynamic();
        for(int i = 0; i < movableShit.Count; i++)
        {
            movableShit[i].GetComponent<Movable>().MoveObject();
        }
    }
    public void KinematicLines()
    {
        Rigidbody2D[] lines = transform.gameObject.GetComponentsInChildren<Rigidbody2D>();
        foreach (var line in lines)
        {
            line.isKinematic = true;
            line.linearVelocity = Vector3.zero;
            line.angularVelocity = 0;
        }
    }
    public int GetLines()
    {
        return transform.childCount;
    }
}
