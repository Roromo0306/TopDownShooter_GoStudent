using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarJugador : MonoBehaviour
{
    [Tooltip("Arrastra aquí el objeto que tenga el script de DeteccionJugador")]
    public DeteccionJugador deteccion;

    [Tooltip("Arrastra aquí el objeto que tenga el script de NavegacionBot")]
    public NavegacionBot naveBot;

    public enum modoRotacion { Inmediato, Gradual, Curva }
    public modoRotacion modo = modoRotacion.Inmediato;

    [Header("Giro Gradual")]
    public float velocidadGiro = 180f; // grados por segundo

    [Header("Giro Curva")]
    public AnimationCurve curvaVelocidad = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Transform objetivoActual;

    void Update()
    {
        ActualizarObjetivo();
        GirarHaciaObjetivo();
    }

    void ActualizarObjetivo()
    {
        if (deteccion.detectado && deteccion.jugador != null)
        {
            objetivoActual = deteccion.jugador; // Apuntar al jugador
        }
        else if (naveBot != null && naveBot.puntos.Count > 0)
        {
            int indice = naveBot.indiceActual % naveBot.puntos.Count;
            objetivoActual = naveBot.puntos[indice]; // Apuntar al punto de patrulla actual
        }
        else
        {
            objetivoActual = null;
        }
    }

    void GirarHaciaObjetivo()
    {
        if (objetivoActual == null) return;

        Vector3 dir = objetivoActual.position - transform.position;
        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // -90 si tu sprite apunta "arriba"

        switch (modo)
        {
            case modoRotacion.Inmediato:
                transform.rotation = Quaternion.Euler(0f, 0f, angulo);
                break;

            case modoRotacion.Gradual:
                float anguloActual = transform.eulerAngles.z;
                float anguloNuevo = Mathf.MoveTowardsAngle(anguloActual, angulo, velocidadGiro * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, 0f, anguloNuevo);
                break;

            case modoRotacion.Curva:
                anguloActual = transform.eulerAngles.z;
                float diferencia = Mathf.DeltaAngle(anguloActual, angulo);
                float t = Mathf.Clamp01(Mathf.Abs(diferencia) / 180f);
                float velocidad = velocidadGiro * curvaVelocidad.Evaluate(t);
                anguloNuevo = Mathf.MoveTowardsAngle(anguloActual, angulo, velocidad * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, 0f, anguloNuevo);
                break;
        }
    }
}

