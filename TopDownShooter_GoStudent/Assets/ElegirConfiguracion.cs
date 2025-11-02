using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElegirConfiguracion : MonoBehaviour
{
    [Header("Selección de Personaje")]
    public TipoPersonaje tipoPersonaje;
    public BaseDatosClasesSO baseDatos;
    [Header("Stats")]
    public float ritmo;
    public float danio;
    public float vida;
    public float velocidad;


    private ClasePersonajeSO claseElegida;

    
    void Awake()
    {
        CargarDatos(tipoPersonaje);
    }

    void CargarDatos(TipoPersonaje tipo)
    {
        if(baseDatos == null)
        {
            Debug.LogError("Falta la base de datos");
            return;
        }

        claseElegida = baseDatos.GetByType(tipo);

        if(claseElegida == null)
        {
            Debug.LogError("La clase elegida no es valida");
            return;
        }

        ritmo = claseElegida.RitmoDisparo;
        danio = claseElegida.DanioDisparo;
        vida = claseElegida.vidaPersonaje;
        velocidad = claseElegida.velocidad; 
    }
}
