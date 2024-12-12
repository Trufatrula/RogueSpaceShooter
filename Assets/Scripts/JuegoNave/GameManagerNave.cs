using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GameManagerNave : MonoBehaviour
{
    //public static GameManager Instance { get; private set; }
    private JugadorNaveController playerController;
    private JugadorNave playerData;
    public SeleccionManager seleccionPartida;

    [SerializeField] private MenuPausa pausa;
    [SerializeField] private Vida vida;
    [SerializeField] private CameraController camara;
    [SerializeField] private List<CameraPoint> puntosCamara = new List<CameraPoint>();
     
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void LoadGame()
    {
        playerData = Guardado.LoadGame();

        if (playerData == null)
        {
            Debug.Log("No hay save.");
            playerData = new JugadorNave("Pepe", 100, new DisparoPreciso(10, 15, "normal", false, false, false), new MovimientoNave(3, 75));
        }
    }

    public void SaveGame()
    {
        Guardado.SaveGame(playerData);
    }

    public void CreateGame()
    {
        List<int> seleccionFinal = seleccionPartida.GetSeleccionFinal();
        DisparoNave disparo = null;
        MovimientoNave movimiento = null;

        switch (seleccionFinal[0])
        {
            case 0:
                disparo = new DisparoPreciso(1, 1.5f, "precisa", false, false, false);
                break;
            case 1:
                disparo = new DisparoCargado();
                break;
        }

        switch (seleccionFinal[1])
        {
            case 0:
                movimiento = new MovimientoNave(0, 5);
                break;
            case 1:
                //disparo = new DisparoCargado();
                break;
        }

        playerData = new JugadorNave("Nave", 100, disparo, movimiento);
        //GameObject playerPrefab = Resources.Load<GameObject>("Player");
        //GameObject playerInstance = Instantiate(playerPrefab);
        //playerController = playerInstance.GetComponent<JugadorNaveController>();
        //playerController.Initialize(playerData);
        //Debug.Log(playerController.GetCurrentGunData().GetDescription());
        //Debug.Log(playerData.ToString());

        CurarVida(5);
        SaveGame();
    }

    public void InstantiatePlayer(Vector3 pos)
    {

        GameObject playerPrefab = Resources.Load<GameObject>("Player");
        GameObject playerInstance = Instantiate(playerPrefab, pos, Quaternion.identity);

        playerController = playerInstance.GetComponent<JugadorNaveController>();
        playerController.Initialize(playerData);

        Debug.Log(playerController.ToString());
        Debug.Log(playerController.GetDataDisparo().GetDescription());
    }

    public void MejoraPlayer(string stat, float cantidad)
    {
        switch(stat)
        {
            case "vida":
                CurarVida((int) Mathf.Round(cantidad));
                break;
            case "poder":
                playerData.disparo.poder = playerData.disparo.poder + cantidad;
                break;
            case "velocidad":
                playerData.movimiento.velocidad = playerData.movimiento.velocidad + cantidad;
                break;
            case "cooldown":
                playerData.disparo.cooldown = playerData.disparo.cooldown + cantidad;
                break;
            case "cadenaCombo":
                playerData.disparo.SetMejora(stat, true);
                playerController.GetComponent<DisparoPrecisoComponent>().cadenaCombo = true;
                break;
            case "dispersion":
                playerData.disparo.SetMejora(stat, true);
                playerController.GetComponent<DisparoPrecisoComponent>().dispersion = true;
                break;
            case "penetracion":
                playerData.disparo.SetMejora(stat, true);
                playerController.GetComponent<DisparoPrecisoComponent>().penetracion = true;
                break;
            default:
                break;
        }
    }

    public void MoverCamara(string punto)
    {
        foreach(CameraPoint p in puntosCamara)
        {
            if (p.gameObject.name.Equals(punto))
            {
                camara.GetCurrentCameraPoint().gameObject.SetActive(false);
                p.gameObject.SetActive(true);
                camara.MoveToPointForzar(p);
            }
        }
    }

    public void MoverCamaraConRetorno(string punto)
    {
        foreach (CameraPoint p in puntosCamara)
        {
            if (p.gameObject.name.Equals(punto))
            {
                camara.MoveToPointForzar(p);
            }
        }
    }

    public void PlayerControllerDestroy()
    {
        playerData.SetDisparoNave(playerData.disparo);
        Destroy(playerController.gameObject);
        playerController = null;
    }

    public string GetTipoDisparo()
    {
        return playerData.tipoDisparo;
    }

    public void CurarVida(int cantidad)
    {
        vida.Curar(cantidad);
    }

    public void Derrotado()
    {
        pausa.Derrota();
    }

    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}