using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CofreTesoro : MonoBehaviour
{
    [SerializeField] private GameManagerNave gmNave;
    [SerializeField] private GestionCurrencias currencia;
    [SerializeField] private Transform spawnNave;
    [SerializeField] private List<GameObject> mejorasPrecisas;
    [SerializeField] private List<GameObject> mejorasCargadas;
    [SerializeField] private MecanicaTexto texto;
    [SerializeField] private CofreHitbox hitbox;

    void OnEnable()
    {
        GetComponent<Button>().interactable = true;
        gmNave.InstantiatePlayer(spawnNave.position);
        hitbox.gameObject.SetActive(true);
    }

    public void AbrirCofre()
    {
        GetComponent<Button>().interactable = false;
        switch(gmNave.GetTipoDisparo())
        {
            case "DisparoPreciso":
                RemoveRandomGameObject(mejorasPrecisas);
                break;
            case "DisparoCargado":
                break;
        }
    }
    public void RemoveRandomGameObject(List<GameObject> gameObjects)
    {
        if (gameObjects.Count == 0)
        {
            Debug.Log("Sale bien");
            currencia.GanarBola(100);
            texto.texto = "100x Dinero Conseguido";
            texto.gameObject.SetActive(true);
            StartCoroutine(VolverAlMenu());
            return;
        }
        int randomIndex = Random.Range(0, gameObjects.Count);
        Instantiate(gameObjects[randomIndex]);
        gmNave.MejoraPlayer(gameObjects[randomIndex].name, 1);
        Debug.Log($"Quitar: {gameObjects[randomIndex].name}");
        texto.texto = gameObjects[randomIndex].name + " adquirida";
        texto.gameObject.SetActive(true);
        gameObjects.RemoveAt(randomIndex);
        StartCoroutine(VolverAlMenu());
    }

    private IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(5f);
        gmNave.MoverCamara("PantallaMapa");
        gmNave.PlayerControllerDestroy();
        hitbox.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
