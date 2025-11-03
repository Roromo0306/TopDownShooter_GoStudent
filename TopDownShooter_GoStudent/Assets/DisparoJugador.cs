using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public enum ModosGiros { Inmediato, Gradual, GradualCurva}
    public ModosGiros modos = ModosGiros.Inmediato;

    public float velocidadGiro = 5f;
    public Transform puntoDisparo;
    public GameObject bala;
    public float cooldown = 0.2f;
    public Camera camera;
    public float rangoVariacionFrecuencia;
    public AnimationCurve curvaVelocidad = AnimationCurve.EaseInOut(0f, 0f, 1f,1f);


    private AudioSource disparo;
    private float frecuenciaInicial;
    private bool puedeDisparar = true;

    // Start is called before the first frame update
    void Start()
    {
        BuscarComponentes();
    }

    // Update is called once per frame
    void Update()
    {
        RotacionJugador();
        if (Input.GetMouseButton(0))
        {
            Disparo();
        }
    }

    void BuscarComponentes()
    {
        disparo = GetComponent<AudioSource>();
        frecuenciaInicial = disparo.pitch;

        if(disparo == null)
        {
            Debug.LogError("No se encuentra la componente AudioSource");
        }

        if(camera == null)
        {
            camera = Camera.main;
            if(camera == null)
            {
                Debug.LogError("No se encuentra la componente  Camera");
            }
        }
     
       
       


    }

    void RotacionJugador()
    {
        Vector3 cursorEnPantalla = Input.mousePosition;
        Vector3 cursorEnMundo = camera.ScreenToWorldPoint(cursorEnPantalla);
        cursorEnMundo.z = 0;

        Vector3 direccionApuntar = cursorEnMundo - transform.position;  
        if(direccionApuntar.magnitude < 0.01f)
        {
            return;
        }
        direccionApuntar.Normalize();

        switch (modos)
        {
            case ModosGiros.Inmediato:
                transform.up = direccionApuntar;
                break;
            case ModosGiros.Gradual:
                GiroGradual(direccionApuntar);
                break;
            case ModosGiros.GradualCurva:
                GiroCurva(direccionApuntar);
                break;
        }
    }

    void GiroGradual(Vector3 direccion)
    {
        
        Vector3 nuevaDireccion = Vector3.RotateTowards(transform.up, direccion, velocidadGiro * Time.deltaTime, 0); //RotateTowards gira un vector a otro al ritmo que le pidamos
        transform.up = nuevaDireccion;
    }

    void GiroCurva(Vector3 direccion)
    {
       
        float angulo = Vector3.Angle(transform.up, direccion); //La funcion de Vector3.Angle devuelve el ángulo entre vectores siempre entre 0 y 180 grados
        float p = Mathf.InverseLerp(0, 180, angulo);
        Vector3 nuevaDireccion = Vector3.RotateTowards(transform.up, direccion, velocidadGiro * curvaVelocidad.Evaluate(p) * Time.deltaTime, 0);
        transform.up = nuevaDireccion;
    }

    void Disparo()
    {
        if(puedeDisparar == true)
        {
            Instantiate(bala, puntoDisparo.position, transform.rotation);
            puedeDisparar = false;
            Invoke("puedeDispararTrue", cooldown);
        }
        

        float variacionPitch = Random.Range(-rangoVariacionFrecuencia, rangoVariacionFrecuencia);
        disparo.pitch = frecuenciaInicial + variacionPitch;
        disparo.Play();
    }

    void puedeDispararTrue()
    {
        puedeDisparar = true;
    }
}
