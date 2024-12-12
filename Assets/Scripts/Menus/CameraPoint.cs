using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CameraPoint : MonoBehaviour
{
    private CameraPoint upPoint, downPoint, leftPoint, rightPoint;
    public float adjacencyDistance = 20f;  // Distance threshold to consider a point as adjacent
    public UnityEvent eventoEnter;
    public UnityEvent eventoLeave;

    private void OnEnable()
    {
        RecalculatePoints();
    }

    private void OnDisable()
    {
        if (upPoint != null) upPoint.SetPuntoAdjacente("down", null);
        if (downPoint != null) downPoint.SetPuntoAdjacente("up", null);
        if (rightPoint != null) rightPoint.SetPuntoAdjacente("left", null);
        if (leftPoint != null) leftPoint.SetPuntoAdjacente("right", null);
    }

    private void Awake()
    {
        FindPuntosAdjacentes(false);
    }

    public void RecalculatePoints()
    {
        FindPuntosAdjacentes(true);
    }

    private void FindPuntosAdjacentes(bool recalculateAdjacents)
    {
        CameraPoint[] allPoints = FindObjectsOfType<CameraPoint>();

        foreach (CameraPoint point in allPoints)
        {
            if (point == this) continue;

            Vector2 direction = (point.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, point.transform.position);

            if (distance <= adjacencyDistance)
            {
                if (Vector2.Dot(direction, Vector2.up) > 0.7f && upPoint == null) upPoint = point;
                else if (Vector2.Dot(direction, Vector2.down) > 0.7f && downPoint == null) downPoint = point;
                else if (Vector2.Dot(direction, Vector2.left) > 0.7f && leftPoint == null) leftPoint = point;
                else if (Vector2.Dot(direction, Vector2.right) > 0.7f && rightPoint == null) rightPoint = point;
            }
        }
        if (recalculateAdjacents)
        {
            if(upPoint != null) upPoint.SetPuntoAdjacente("down", this);
            if(downPoint != null) downPoint.SetPuntoAdjacente("up", this);
            if(rightPoint != null) rightPoint.SetPuntoAdjacente("left", this);
            if(leftPoint != null) leftPoint.SetPuntoAdjacente("right", this);
        }
    }

    public void EntrarAlPunto()
    {
        eventoEnter.Invoke();
    }

    public void SalirDelPunto()
    {
        eventoLeave.Invoke();
    }

    public CameraPoint GetPuntoAdjacente(string direction)
    {
        switch (direction)
        {
            case "up":
                return upPoint;
            case "down":
                return downPoint;
            case "right":
                return rightPoint;
            case "left":
                return leftPoint;
            default:
                return null;
        }
    }

    public void SetPuntoAdjacente(string direction, CameraPoint newPoint)
    {
        switch (direction)
        {
            case "up":
                upPoint = newPoint;
                break;
            case "down":
                downPoint = newPoint;
                break;
            case "right":
                rightPoint = newPoint;
                break;
            case "left":
                leftPoint = newPoint;
                break;
            default:
                break;
        }
    }
}