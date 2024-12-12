using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteEnemigos : MonoBehaviour
{
    private GameManagerNave gmNave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().ChoqueBarrera();
            RedSpyInTheBase();
        }
    }

    private void RedSpyInTheBase()
    {
        gmNave = FindObjectOfType<GameManagerNave>();
        gmNave.Derrotado();
    }
}
