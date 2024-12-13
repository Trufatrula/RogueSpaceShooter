using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosNormales : MonoBehaviour
{
    [SerializeField] private List<GameObject> listEnemies;
    [SerializeField] private List<GameObject> listInstanciasEnemies;
    [SerializeField] private bool bossFinal = false;

    private List<float> probabilidades;
    [SerializeField] private float number = 0.1f;
    [SerializeField] private float duracion = 10f;
    [SerializeField] private float intervaloEnemigoMin = 4f;
    [SerializeField] private float intervaloEnemigoMax = 7f;

    [SerializeField] private Transform spawnNave;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;

    private GameManagerNave gmNave;
    private MenuPausa pausa;
    private MejoraManager mejoraManager;

    void Start()
    {
        gmNave = FindAnyObjectByType<GameManagerNave>();
        gmNave.InstantiatePlayer(spawnNave.position);
        pausa = FindAnyObjectByType<MenuPausa>();
        pausa.SetPausable(true);

        mejoraManager = FindAnyObjectByType<MejoraManager>();
        AsignarPosibilidades();
    }

    public void ComenzarOleadas(float dificultad)
    {
        number = 0.1f * dificultad;
        duracion = duracion + (2.5f * dificultad);
        intervaloEnemigoMin = intervaloEnemigoMin - (0.25f * dificultad);
        if(intervaloEnemigoMin < 0.5f) { intervaloEnemigoMin = 0.5f; }
        intervaloEnemigoMax = intervaloEnemigoMax - (0.2f * dificultad);
        if (intervaloEnemigoMax < 1f) { intervaloEnemigoMax = 1f; }
        StartCoroutine(SpawnEnemies());
    }

    private void AsignarPosibilidades()
    {
        int count = listEnemies.Count;
        probabilidades = new List<float>(count);

        for (int i = 0; i < count; i++)
        {
            float weight;
            if (number >= 1f)
            {
                weight = Mathf.Pow(i + 1, number);
            }
            else
            {
                weight = Mathf.Pow(count - i, 1 / number);
            }

            probabilidades.Add(weight);
        }

        float totalWeight = 0f;
        foreach (float weight in probabilidades)
        {
            totalWeight += weight;
        }

        for (int i = 0; i < probabilidades.Count; i++)
        {
            probabilidades[i] /= totalWeight;
        }

        for (int i = 0; i < probabilidades.Count; i++)
        {
            Debug.Log($"Enemigo {listEnemies[i].name} Probabilidad {probabilidades[i]}");
        }
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(5f);

        float elapsedTime = 0f;

        while (elapsedTime < duracion)
        {
            GameObject enemyToSpawn = SelectEnemigo();

            float randomFactor = Random.Range(0f, 1f);

            Vector2 randomPosition = Vector2.Lerp(spawn1.position, spawn2.position, randomFactor);

            GameObject instanciaEnemy = Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
            listInstanciasEnemies.Add(instanciaEnemy);
            float spawnInterval = Random.Range(intervaloEnemigoMin, intervaloEnemigoMax);
            yield return new WaitForSeconds(spawnInterval);

            elapsedTime += spawnInterval;
        }

        while(listInstanciasEnemies.Count > 0)
        {
            yield return null;
        }
        DarRecompensa();
    }

    public void QuitarEnemigo(GameObject enemigo)
    {
        listInstanciasEnemies.Remove(enemigo);
    }

    public void MeterEnemigo(GameObject enemigo)
    {
        listInstanciasEnemies.Add(enemigo);
    }

    private GameObject SelectEnemigo()
    {
        float randomValue = Random.value; 
        float cumulativeProbability = 0f;

        for (int i = 0; i < probabilidades.Count; i++)
        {
            cumulativeProbability += probabilidades[i];
            if (randomValue <= cumulativeProbability)
            {
                return listEnemies[i];
            }
        }
        return listEnemies[0];
    }

    private void DarRecompensa()
    {
        if (!bossFinal)
        {
            mejoraManager.SetNivelActual(this);
            mejoraManager.SelectRandomObjects(3);
            mejoraManager.GetComponent<Animator>().SetTrigger("Bajar");
        } else
        {
            gmNave.VictoriaMagistral();
        }
    }

    public void RegresarAlMapa()
    {
        pausa.SetPausable(false);
        gmNave.MoverCamara("PantallaMapa");
        gmNave.PlayerControllerDestroy();
        Destroy(gameObject);
    }
}
