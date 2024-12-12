using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BalaPrecisa : MonoBehaviour
{
    private float velocidad = 6f;
    private ObjectPool<BalaPrecisa> pool;
    public float poder;
    public bool dispersion = false;
    public bool penetracion = false;

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * velocidad * Time.deltaTime);
    }

    public void SetPool(ObjectPool<BalaPrecisa> pool)
    {
        this.pool = pool;
    }

    public void Choque()
    {
        if(dispersion)
        {
            var bala = pool.Get();
            bala.transform.position = transform.position;
            bala.transform.rotation = bala.transform.rotation = Random.value > 0.5f ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
            bala.dispersion = false;
            bala.penetracion = true;
        }
        if (!penetracion)
        {
            pool.Release(this);
        }
    }

    public void ChoquePared()
    {
        pool.Release(this);
    }
}
