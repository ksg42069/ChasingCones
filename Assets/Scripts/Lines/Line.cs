using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lr;
    public EdgeCollider2D edgeCol;
    public Rigidbody2D rb;

    public List<Vector2> points = new List<Vector2>();
    public int pointCount = 0;

    [Header("Stats")]
    public float pointMinDistance = 0.15f;
    public float circleColRad;
    public float density;

    public Vector2 GetLastPoint()
    {
        return (Vector2)lr.GetPosition(pointCount-1);
    }
    public void UsePhysics(bool usePhx)
    {
        rb.isKinematic = !usePhx;
    }
    public void SetLineColor(Gradient color)
    {
        lr.colorGradient = color;
    }
    public void SetLineWidth(float lineWidth)
    {
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        edgeCol.edgeRadius = lineWidth/2f;
        circleColRad = lineWidth/2f;
    }
    public void AddPoint(Vector2 newPoint)
    {
        if (points.Count > 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointMinDistance)
            return;

        points.Add(newPoint);
        pointCount++;

        CircleCollider2D circleCol = this.gameObject.AddComponent<CircleCollider2D>();
        circleCol.offset = newPoint;
        circleCol.radius = circleColRad;

        lr.positionCount = pointCount;
        lr.SetPosition(pointCount - 1, newPoint);

        if(pointCount > 1)
        {
            edgeCol.points = points.ToArray();
        }
    }
    public void SetMinDistance(float minDistance)
    {
        pointMinDistance = minDistance;
    }
    public void SetMass()
    {
        rb.mass = (pointCount - 1) * density;
    }
}
