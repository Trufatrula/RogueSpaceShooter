using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteBalas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            collision.GetComponent<BalaPrecisa>().ChoquePared();
        }
    }
}
