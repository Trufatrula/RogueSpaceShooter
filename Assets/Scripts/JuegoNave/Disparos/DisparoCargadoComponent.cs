using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisparoCargadoComponent : DisparoNaveComponent

{
    public float poderCargaLigera;
    public float poderCargaPesada;
    public bool cuartaCarga;
    public bool penetracion;
    public void Initialize(DisparoCargado disparoData)
    {
        poder = disparoData.poder;
        cooldown = disparoData.cooldown;
        poderCargaLigera = disparoData.poderCargaLigera;
        poderCargaPesada = disparoData.poderCargaPesada;
        cuartaCarga = disparoData.cuartaCarga;
        penetracion = disparoData.penetracion;
    }

    void Update()
    {
        
    }

    public override void Disparo()
    {
        //TODO
    }
}
