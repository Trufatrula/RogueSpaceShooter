using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNaveController : MonoBehaviour
{
    public float health;
    public MovimientoNave movimiento;
    private DisparoNaveComponent disparoEquipado;

    public void Initialize(JugadorNave playerData)
    { 
        health = playerData.health;
        movimiento = playerData.movimiento;

        DisparoNave dispaoAhora = playerData.GetDisparoNave();

        if (dispaoAhora != null)
        {
            EquiparDisparo(dispaoAhora);
        }

        switch(movimiento.movimiento)
        {
            case 0:
                Debug.Log(movimiento.velocidad);
                gameObject.AddComponent<MovementLibre>().SetVelocidad(movimiento.velocidad);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

    }
    public void EquiparDisparo(DisparoNave dataDisparo)
    {
        if (disparoEquipado != null)
        {
            Destroy(disparoEquipado);
        }

        if (dataDisparo is DisparoPreciso disparoPrecisoData)
        {
            disparoEquipado = gameObject.AddComponent<DisparoPrecisoComponent>();
            ((DisparoPrecisoComponent)disparoEquipado).Initialize(disparoPrecisoData);
        }
        else if (dataDisparo is DisparoCargado disparoCargadoData)
        {
            disparoEquipado = gameObject.AddComponent<DisparoCargadoComponent>();
            ((DisparoCargadoComponent)disparoEquipado).Initialize(disparoCargadoData);
        }
        else
        {
            Debug.Log("Todavia no hay");
        }
    }

    public DisparoNave GetDataDisparo()
    {
        if (disparoEquipado is DisparoPrecisoComponent disparoPrecisoComp)
        {
            return new DisparoPreciso(disparoPrecisoComp.poder, disparoPrecisoComp.cooldown, disparoPrecisoComp.bala, disparoPrecisoComp.dispersion, disparoPrecisoComp.cadenaCombo, disparoPrecisoComp.penetracion);
        } 
        else if (disparoEquipado is DisparoCargadoComponent disparoCargadoComp)
        {
            return new DisparoCargado(disparoCargadoComp.poder, disparoCargadoComp.cooldown, disparoCargadoComp.bala, disparoCargadoComp.poderCargaLigera, disparoCargadoComp.poderCargaPesada, disparoCargadoComp.cuartaCarga, disparoCargadoComp.penetracion);
        }
        return null;
    }
}
