using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MejorasDePago : MonoBehaviour
{
    [SerializeField] string stat;
    [SerializeField] float cantidad;
    [SerializeField] TextoNumero precio;
    private int precioOriginal;
    [SerializeField] GestionCurrencias banca;
    private GameManagerNave gm;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManagerNave>();
    }

    public void AplicarMejoraPago()
    {
        if(precio.texto <= banca.GetCantidadBolas() && precio.texto > 0)
        {
            GetComponent<Button>().interactable = false;
            banca.PagarBola(precio);
            gm.MejoraPlayer(stat, cantidad);
        }
    }

}
