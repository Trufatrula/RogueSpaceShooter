using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeleccionManager : MonoBehaviour
{
    public SeleccionMenu[] menus; 
    private int currentMenuIndex = 0;
    public UnityEvent evento;
    private bool finalizado = false;
    List<int> selecciones = new List<int>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !finalizado)
        {
            if (!ConfirmCurrentSelection()) return;
            if (CheckAllMenusConfirmed())
            {
                finalizado = true;
                menus[currentMenuIndex].DeactivateMenu();
                PerformFinalAction();
            }
            else
            {
                MoveToNextMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MoveToPreviousMenu();
        }
    }

    bool ConfirmCurrentSelection()
    {
        if(menus[currentMenuIndex].isActive)
        {
            selecciones.Add(menus[currentMenuIndex].getSelectedIndex());
            menus[currentMenuIndex].ConfirmSelection();
            return true;
        }
        return false;
    }

    bool CheckAllMenusConfirmed()
    {
        foreach (var menu in menus)
        {
            if (!menu.isConfirmed)
                return false;
        }
        return true;
    }

    void MoveToNextMenu()
    {
        menus[currentMenuIndex].DeactivateMenu();

        if (currentMenuIndex < menus.Length - 1)
        {
            currentMenuIndex++;
            ActivateMenu(currentMenuIndex);
        }
    }

    void MoveToPreviousMenu()
    {
        if (currentMenuIndex > 0)
        {
            menus[currentMenuIndex].Deselect();
            menus[currentMenuIndex].DeactivateMenu();
            selecciones.Remove(currentMenuIndex);

            currentMenuIndex--;
            ActivateMenu(currentMenuIndex);
        }
    }

    public void ReturnToStart()
    {
        while (currentMenuIndex > 0)
        {
            MoveToPreviousMenu();
        }
        menus[currentMenuIndex].Deselect();
        menus[currentMenuIndex].DeactivateMenu();
    }

    void ActivateMenu(int index)
    {
        Debug.Log(index);
        menus[index].ActivateMenu();
    }

    void PerformFinalAction()
    {
        Debug.Log("Hecho");
        evento.Invoke();
    }

    public List<int> GetSeleccionFinal()
    {
        return selecciones;
    }
}