using UnityEngine;

[System.Serializable]
public class JugadorNave
{
    public string name;
    public float health;
    public DisparoNave disparo;
    public MovimientoNave movimiento;
    public string tipoDisparo;
    public string datosDisparo;

    public JugadorNave(string name, int health, DisparoNave disparo, MovimientoNave movimiento)
    {
        this.name = name;
        this.health = health;
        this.disparo = disparo;
        this.movimiento = movimiento;


        SetDisparoNave(disparo);
    }

    public JugadorNave() { }

    public void SetDisparoNave(DisparoNave disparo)
    {

        if (disparo == null)
        {
            tipoDisparo = null;
            datosDisparo = null;
        }

        this.tipoDisparo = disparo?.GetType().Name;
        this.datosDisparo = JsonUtility.ToJson(disparo);
    }

    public DisparoNave GetDisparoNave()
    {
        if (string.IsNullOrEmpty(tipoDisparo) || string.IsNullOrEmpty(datosDisparo))
            return null;

        switch (tipoDisparo)
        {
            case "DisparoPreciso":
                return JsonUtility.FromJson<DisparoPreciso>(datosDisparo);
            case "DisparoCargado":
                return JsonUtility.FromJson<DisparoCargado>(datosDisparo);
            default:
                Debug.LogWarning("No hay mas");
                return null;
        }
    }
}