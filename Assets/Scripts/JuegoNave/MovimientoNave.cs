using UnityEngine;

[System.Serializable]
public class MovimientoNave
{
    public int movimiento;
    public float velocidad;

    public MovimientoNave(int movimiento, float velocidad)
    {
        this.movimiento = movimiento;
        this.velocidad = velocidad;
    }
}