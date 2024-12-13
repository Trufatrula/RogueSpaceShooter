using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionCurrencias : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoBolas;
    private int cantidadBolas;

    [SerializeField] Button botonSalirTienda;

    public void ConseguirBola()
    {
        cantidadBolas++;
        textoBolas.text = "x " + cantidadBolas;
    }

    public void PerderBola()
    {
        cantidadBolas--;
        textoBolas.text = "x " + cantidadBolas;
    }

    public void PagarBola(TextoNumero deposito)
    {
        StartCoroutine(ReducirGradualmente(deposito));
    }

    public void GanarBola(int deposito)
    {
        StartCoroutine(AumentarGradualmente(deposito));
    }

    private IEnumerator ReducirGradualmente(TextoNumero deposito)
    {
        botonSalirTienda.interactable = false;
        while (cantidadBolas > 0 && deposito.texto > 0)
        {
            deposito.Reducir();
            PerderBola();
            yield return new WaitForSeconds(0.001f); 
        }
        botonSalirTienda.interactable = true;
    }

    private IEnumerator AumentarGradualmente(int deposito)
    {
        while (deposito > 0)
        {
            deposito--;
            ConseguirBola();
            yield return new WaitForSeconds(0.01f);
        }
    }

    public int GetCantidadBolas()
    {
        return cantidadBolas;
    }

}

