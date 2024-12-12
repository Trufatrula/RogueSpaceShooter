using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MejoraManager : MonoBehaviour
{
    [SerializeField] private List<MejoraBoton> listaMejoras;
    private Dictionary<MejoraBoton, float> weights = new Dictionary<MejoraBoton, float>();
    private float defaultWeight = 1.0f;
    private float reducedWeight = 0.5f;

    List<MejoraBoton> mejorasDisponibles = new List<MejoraBoton>();
    List<MejoraBoton> mejorasInstancia = new List<MejoraBoton>();

    [SerializeField] private List<RectTransform> mejora;

    private EnemigosNormales nivelActual;
    [SerializeField] private SlotController slots;

    void Start()
    {
        foreach (MejoraBoton obj in listaMejoras)
        {
            weights[obj] = defaultWeight;
        }
    }

    public void SelectRandomObjects(int count)
    {
        mejorasDisponibles.Clear();

        List<MejoraBoton> availableObjects = new List<MejoraBoton>(listaMejoras);
        Dictionary<MejoraBoton, float> tempWeights = new Dictionary<MejoraBoton, float>(weights);
        int c = 0;

        for (int i = 0; i < count; i++)
        {
            if (availableObjects.Count == 0)
                break;

            float totalWeight = 0f;
            foreach (MejoraBoton obj in availableObjects)
            {
                totalWeight += tempWeights[obj];
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0f;
            MejoraBoton selectedObject = null;


            foreach (MejoraBoton obj in availableObjects)
            {
                cumulativeWeight += tempWeights[obj];
                if (randomValue <= cumulativeWeight)
                {
                    selectedObject = obj;
                    MejoraBoton mejor = Instantiate(selectedObject, mejora[c]);
                    mejor.SetManager(this);
                    mejor.GetComponent<Button>().interactable = true;
                    mejorasInstancia.Add(mejor);
                    c++;
                    break;
                }
            }

            if (selectedObject != null)
            {
                mejorasDisponibles.Add(selectedObject);
                availableObjects.Remove(selectedObject);

                weights[selectedObject] = reducedWeight;
                Debug.Log(reducedWeight);
            }
        }

        foreach (MejoraBoton obj in listaMejoras)
        {
            if (!mejorasDisponibles.Contains(obj))
            {
                weights[obj] = defaultWeight;
            }
        }

        //return selectedObjects;
    }

    public void TerminarEleccion()
    {
        foreach (MejoraBoton mejora in mejorasInstancia)
        {
            mejora.GetComponent<Button>().interactable = false;
            Destroy(mejora.gameObject);
        }
        mejorasInstancia.Clear();
        GetComponent<Animator>().SetTrigger("Subir");
        if(nivelActual != null)
        {
            nivelActual.RegresarAlMapa();
            nivelActual = null;
        } else
        {
            slots.derrota.Invoke();
        }

    }

    public void SetNivelActual(EnemigosNormales nivel)
    {
        nivelActual = nivel;
    }
}