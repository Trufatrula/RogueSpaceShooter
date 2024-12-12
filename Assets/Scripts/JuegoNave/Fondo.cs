using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] Vector3 direccion;
    [SerializeField] float anchoImagen = 20f;

    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        float resto = (velocidad * Time.time) % anchoImagen;
        transform.position = posInicial + resto * direccion;
    }
}
