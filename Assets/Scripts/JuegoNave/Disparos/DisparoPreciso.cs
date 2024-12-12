using UnityEngine;

[System.Serializable]
public class DisparoPreciso : DisparoNave
{
    public bool dispersion;
    public bool cadenaCombo;
    public bool penetracion;

    public DisparoPreciso() : base("DisparoPreciso")
    {
        dispersion = false;
        cadenaCombo = false;
        penetracion = false;
    }

    public DisparoPreciso(float poder, float cooldown, string bala, bool dispersion, bool cadenaCombo, bool penetracion) : base("DisparoPreciso", poder, cooldown, bala)
    {
        this.dispersion = dispersion;
        this.cadenaCombo = cadenaCombo;
        this.penetracion = penetracion;
    }

    public override string GetDescription()
    {
        return $"Preciso: Damage {poder}, Cooldown {cooldown}, Dispersion {dispersion}, Cadena de combo {cadenaCombo}, Penetracion {penetracion}";
    }

    public override void SetMejora(string mejora, bool activar)
    {
        switch(mejora)
        {
            case "cadenaCombo":
                cadenaCombo = activar;
                break;
            case "dispersion":
                dispersion = activar;
                break;
            case "penetracion":
                penetracion = activar;
                break;
            default:
                break;
        }
    }
}