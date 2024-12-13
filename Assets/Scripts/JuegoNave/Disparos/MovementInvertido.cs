using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInvertido : MonoBehaviour
{
    private float velocidad = 5f;

    void Update()
    {
        MovimientoLibre();
        ClampMovimientoLibre();
    }

    void MovimientoLibre()
    {
        float InputH = Input.GetAxisRaw("Horizontal");
        float InputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(InputV, InputH).normalized * velocidad * Time.deltaTime);
    }

    void ClampMovimientoLibre()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -43.5f, -36f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    public void SetVelocidad(float vel)
    {
        velocidad = vel;
    }
}
