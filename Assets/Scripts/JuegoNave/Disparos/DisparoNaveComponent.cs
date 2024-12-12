using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisparoNaveComponent : MonoBehaviour
{
    public float poder;
    public float cooldown;
    public string bala;

    public abstract void Disparo();
}
