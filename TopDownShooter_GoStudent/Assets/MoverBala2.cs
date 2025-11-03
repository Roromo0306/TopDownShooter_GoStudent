
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBala2 : MoverBala
{
    public float tiempoVida = 10f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Invoke("AutoDestruccion", tiempoVida);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
            base.OnTriggerEnter2D (col);

        if (col.CompareTag("Pared"))
        {
            velocidad *= -1;
        }
    }

    void AutoDestruccion()
    {
        Destroy(gameObject);
    }
}
