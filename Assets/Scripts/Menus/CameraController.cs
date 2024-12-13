using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CameraPoint puntoActual;

    void Start()
    {
        puntoActual = GetPuntoCercano(transform.position);

        if (puntoActual != null)
        {
            transform.position = puntoActual.transform.position;
        }
    }

    void Update()
    {
        if (transform.position != puntoActual.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoActual.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void MoveToPoint(CameraPoint newPoint)
    {
        puntoActual = newPoint; 
    }

    public void MoveToForzar(string direccion)
    {
        CallEventos(direccion);
        MoveToPoint(puntoActual.GetPuntoAdjacente(direccion));
    }

    public void MoveToPointForzar(CameraPoint punto)
    {
        CallEventosPunto(punto);
        MoveToPoint(punto);
    }

    private CameraPoint GetPuntoCercano(Vector3 position)
    {
        CameraPoint[] allPoints = FindObjectsOfType<CameraPoint>();
        float minDistance = Mathf.Infinity;
        CameraPoint closestPoint = null;

        foreach (CameraPoint point in allPoints)
        {
            float distance = Vector3.Distance(position, point.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    private void CallEventos(string direction)
    {
        puntoActual.SalirDelPunto();
        puntoActual.GetPuntoAdjacente(direction).EntrarAlPunto();
    }

    private void CallEventosPunto(CameraPoint punto)
    {
        puntoActual.SalirDelPunto();
        punto.EntrarAlPunto();
    }

    public CameraPoint GetCurrentCameraPoint()
    {
        return puntoActual;
    }
}