using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoNumero : MonoBehaviour
{
    public int texto;
    private TextMeshProUGUI textoTexto;

    public void Start()
    {
        Debug.Log("VIVI");
        textoTexto = GetComponent<TextMeshProUGUI>();
        textoTexto.text = texto.ToString();
    }

    public void Aumentar()
    {
        texto++;
        textoTexto.text = texto.ToString();
    }

    public void Reducir()
    {
        texto--;
        textoTexto.text = texto.ToString();
    }
}
