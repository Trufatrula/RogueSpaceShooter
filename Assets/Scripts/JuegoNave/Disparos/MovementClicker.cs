using UnityEngine;

public class MovementClicker : MonoBehaviour
{
    private float velocidad = 5f;

    private Vector3 posicionTarget;
    private bool moviendo = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posRaton.z = transform.position.z;

            posicionTarget = posRaton;
            moviendo = true;
        }

        if (moviendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionTarget, velocidad * Time.deltaTime);

            float xClamped = Mathf.Clamp(transform.position.x, -43.5f, -36f);
            float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
            transform.position = new Vector3(xClamped, yClamped, transform.position.z);

            if (Vector3.Distance(transform.position, posicionTarget) < 0.01f)
            {
                moviendo = false;
            }
        }
    }
    public void SetVelocidad(float vel)
    {
        velocidad = vel;
    }
}