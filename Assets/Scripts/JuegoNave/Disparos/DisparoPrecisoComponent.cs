using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class DisparoPrecisoComponent : DisparoNaveComponent
{
    public bool dispersion;
    public bool cadenaCombo;
    public bool penetracion;
    private bool puedeDisparar = true;

    public BalaPrecisa balaPrefab;
    private ObjectPool<BalaPrecisa> balaPool;

    public void Initialize(DisparoPreciso disparoData)
    { 
        poder = disparoData.poder;
        cooldown = disparoData.cooldown;
        bala = disparoData.bala;
        dispersion = disparoData.dispersion;
        cadenaCombo = disparoData.cadenaCombo;
        penetracion = disparoData.penetracion;

        balaPrefab = Resources.Load<BalaPrecisa>(bala);
    }

    private void Awake()
    {
        balaPool = new ObjectPool<BalaPrecisa>(CrearBala, GetBala, ReleaseBala, DestroyBala);
    }

    private BalaPrecisa CrearBala()
    {
        BalaPrecisa balaCopia = Instantiate(balaPrefab, transform.position, Quaternion.identity);
        balaCopia.SetPool(balaPool);
        balaCopia.poder = poder;
        balaCopia.dispersion = dispersion;
        balaCopia.penetracion = penetracion;
        return balaCopia;
    }

    private void GetBala(BalaPrecisa bala)
    {
        bala.transform.position = transform.position;
        bala.transform.rotation = Quaternion.identity;
        bala.gameObject.SetActive(true);
        bala.dispersion = dispersion;
        bala.penetracion = penetracion;
    }

    private void ReleaseBala(BalaPrecisa bala)
    {
        bala.gameObject.SetActive(false);
    }

    private void DestroyBala(BalaPrecisa bala)
    {
        Destroy(bala.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && puedeDisparar)
        {
            Disparo();
            StartCoroutine(CooldownDisparo());
        }
    }

    public override void Disparo()
    {
        if(cadenaCombo)
        {
            SpawnBala(Vector3.zero);
            SpawnBala(new Vector3(-Mathf.Sin(Mathf.Deg2Rad * 10), Mathf.Cos(Mathf.Deg2Rad * 10), 0));
            SpawnBala(new Vector3(Mathf.Sin(Mathf.Deg2Rad * 10), Mathf.Cos(Mathf.Deg2Rad * 10), 0));
        }
        else
        {
            var bala = balaPool.Get();
        }
    }

    private void SpawnBala(Vector3 directionOffset)
    {
        var bullet = balaPool.Get();
        bullet.transform.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.up, directionOffset, Vector3.forward));
    }

    private IEnumerator CooldownDisparo()
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(cooldown);
        puedeDisparar = true;
    }

}
