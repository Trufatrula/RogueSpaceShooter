using UnityEngine;

[System.Serializable]
public abstract class DisparoNave
{
    public string tipoDisparo;
    public float poder;
    public float cooldown;
    public string bala;

    public DisparoNave(string tipoDisparo)
    {
        this.tipoDisparo = tipoDisparo;
        poder = 10f;
        cooldown = 1f;
        bala = "normal";
    }

    public DisparoNave(string tipoDisparo, float poder, float cooldown, string bala)
    {
        this.tipoDisparo = tipoDisparo;
        this.poder = poder;
        this.cooldown = cooldown;
        this.bala = bala;
    }

    public abstract string GetDescription();
    public abstract void SetMejora(string mejora, bool activar);
}