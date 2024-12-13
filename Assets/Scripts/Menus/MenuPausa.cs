using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject menuDerrota;
    [SerializeField] GameObject menuVictoria;

    private bool pausable = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausable)
        {
            Pausar();
        }
    }

    private void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Despausar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetPausable(bool pausable)
    {
        this.pausable = pausable;
    }

    public void Derrota()
    {
        menuPausa.SetActive(false);
        menuDerrota.SetActive(true);
        pausable = false;
        Time.timeScale = 0f;
    }

    public void Victoria()
    {
        menuPausa.SetActive(false);
        menuVictoria.SetActive(true);
        pausable = false;
        Time.timeScale = 0f;
    }
}