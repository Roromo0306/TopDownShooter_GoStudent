using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavegacionBot : MonoBehaviour
{
    public List<Transform> puntos = new List<Transform> ();
    public float distanciaCambio = 0.3f;


    private DeteccionJugador deteccionJugador;
    private int indiceActual = 0;
    private NavMeshAgent agente;

    // Start is called before the first frame update
    void Start()
    {
        BuscarComponentes();
        ConfiguracionInicialAgente();
    }

    // Update is called once per frame
    void Update()
    {
        if (!deteccionJugador.detectado)
        {
            Patrulla();
        }
        else
        {
            agente.SetDestination(deteccionJugador.jugador.position);
        }
            
    }

    void BuscarComponentes()
    {
        
        deteccionJugador = GetComponent<DeteccionJugador>();
        if(deteccionJugador == null )
        {
            Debug.LogError("La componente DeteccionJugador no fue encontrada");
            return;
        }

        agente = GetComponent<NavMeshAgent>();
        if(agente == null)
        {
            Debug.LogError("La componente NavMeshAgent no fue encontrada");
            return;
        }

    }

    void ConfiguracionInicialAgente()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;    
        agente.SetDestination(puntos[indiceActual].position);
    }

    void Patrulla()
    {
        
        Vector3 objetivo = puntos[indiceActual].position;
        objetivo.z = 0;
        float distancia = Vector3.Distance(objetivo, transform.position);

        if (distancia <= distanciaCambio)
        {
            indiceActual++;
            if(indiceActual >= puntos.Count)
            {
                indiceActual = 0;
               
            }

            agente.SetDestination(puntos[indiceActual].position);
        }
    }
}
