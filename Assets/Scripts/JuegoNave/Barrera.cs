using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Barrera : MonoBehaviour
{
    private bool activa = true;
    private SpriteRenderer sprite;
    private BoxCollider2D colliderBox;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        colliderBox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().ChoqueBarrera();
            Desactivar();
        }
    }

    public bool GetActiva()
    {
        return activa;
    }

    public void SetActiva()
    {
        activa = true;
        sprite.enabled = true;
        colliderBox.enabled = true;
    }

    public void Desactivar()
    {
        activa = false;
        sprite.enabled = false;
        colliderBox.enabled = false;
    }
}
