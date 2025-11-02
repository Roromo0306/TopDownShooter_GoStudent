using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="ClasePersonaje", fileName = "NuevaClase")] 
public class ClasePersonajeSO : ScriptableObject //Contenedor de datos
{
    public TipoPersonaje tipoPersonaje;
    public float RitmoDisparo = 5f;
    public float DanioDisparo = 5f;
    public float vidaPersonaje = 5f;
    public float velocidad = 5f;
}
