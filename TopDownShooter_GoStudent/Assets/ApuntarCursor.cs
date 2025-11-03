using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarCursor : ApuntarJugador
{
    public Camera camera;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BuscarComponentes();
    }

    protected override void ActualizarObjetivo()
    {
        Vector3 cursorEnPantalla = Input.mousePosition;
        Vector3 cursorEnMundo = camera.ScreenToWorldPoint(cursorEnPantalla);      
        cursorEnMundo.z = 0;

        objetivoActual = cursorEnMundo;
    }
    // Update is called once per frame
    protected override void Update()
    {
        ActualizarObjetivo();
        GirarHaciaObjetivo();
    }

    void BuscarComponentes()
    {

        if (camera == null)
        {
            camera = Camera.main;
            if (camera == null)
            {
                Debug.LogError("No se encuentra la componente  Camera");
            }
        }


    }
}
