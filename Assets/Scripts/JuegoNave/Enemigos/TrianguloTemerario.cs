using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrianguloTemerario : Enemigo
{
    private float velocidad;
    [SerializeField] private float vida = 1f;
    [SerializeField] private float velMin = 3f;
    [SerializeField] private float velMax = 5f;
    [SerializeField] private int dinero = 10;
    private GestionCurrencias currencias;
    private EnemigosNormales nivel;

    void Start()
    {
        velocidad = Random.Range(velMin, velMax);
        currencias = FindAnyObjectByType<GestionCurrencias>();
        nivel = FindAnyObjectByType<EnemigosNormales>();
    }

    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bala"))
        {
            vida = vida - collision.GetComponent<BalaPrecisa>().poder;
            collision.GetComponent<BalaPrecisa>().Choque();
            if(vida <= 0)
            {
                currencias.GanarBola(dinero);
                nivel.QuitarEnemigo(gameObject);
                Destroy(gameObject);
            }
        }
    }
    public override void ChoqueBarrera()
    {
        nivel.QuitarEnemigo(gameObject);
        Destroy(gameObject);
    }
}
