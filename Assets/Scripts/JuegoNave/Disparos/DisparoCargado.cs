using UnityEngine;

[System.Serializable]
public class DisparoCargado : DisparoNave
{
    public float poderCargaLigera;
    public float poderCargaPesada;
    public bool cuartaCarga;
    public bool penetracion;

    public DisparoCargado() : base("DisparoCargado")
    {
        poderCargaLigera = 10;
        poderCargaPesada = 20;
        cuartaCarga = false;
        penetracion = false;
    }

    public DisparoCargado(float poder, float cooldown, string bala, float poderCargaLigera, float poderCargaPesada, bool cuartaCarga, bool penetracion) : base("DisparoCargado", poder, cooldown, bala)
    {
        this.poderCargaLigera = poderCargaLigera;
        this.poderCargaPesada = poderCargaPesada;
        this.cuartaCarga = cuartaCarga;
        this.penetracion = penetracion;
    }

    public override string GetDescription()
    {
        return $"Cargado: Damage {poder}, Cooldown {cooldown}, PoderCargaMinima {poderCargaLigera}, PoderCargaMaxima {poderCargaPesada}, cuarta carga {cuartaCarga}, penetracion {penetracion}";
    }

    public override void SetMejora(string mejora, bool activar)
    {
        throw new System.NotImplementedException();
    }
}