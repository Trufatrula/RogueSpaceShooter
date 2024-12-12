using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraBoton : MonoBehaviour
{
    [SerializeField] string stat;
    [SerializeField] float cantidad;
    private MejoraManager manager;
    private GameManagerNave gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManagerNave>();
    }

    public void AplicarMejora()
    {
        gm.MejoraPlayer(stat, cantidad);
        manager.TerminarEleccion();
    }

    public void SetManager(MejoraManager manager)
    {
        this.manager = manager;
        Debug.Log("MANADEW");
    }
}
