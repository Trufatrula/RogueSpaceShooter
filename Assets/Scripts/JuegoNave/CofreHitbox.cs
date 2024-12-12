using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreHitbox : MonoBehaviour
{
    [SerializeField] CofreTesoro cofreTesoro;
    private bool abre;

    private void OnEnable()
    {
        abre = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            collision.GetComponent<BalaPrecisa>().ChoquePared();
            if(abre)
            {
                cofreTesoro.AbrirCofre();
                abre = false;
            }
        }
    }
}
