using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventoMenu : MonoBehaviour
{
    public UnityEvent evento;
    public void InvocarEvento()
    {
        evento.Invoke();
    }
}
