using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] private List<GameObject> vidas = new List<GameObject>();
    [SerializeField] private Barrera barrera;

    void Start()
    {
        foreach(GameObject vida in vidas)
        {
            Barrera inBarrera = Instantiate(barrera, vida.transform);
            inBarrera.Desactivar();
        }
    }

    public void Curar(int cantidad)
    {
        foreach (GameObject vida in vidas)
        {
            if (!vida.GetComponentInChildren<Barrera>().GetActiva())
            {
                cantidad--;
                vida.GetComponentInChildren<Barrera>().SetActiva();
                if (cantidad == 0) break;
            }
        }
    }
}
