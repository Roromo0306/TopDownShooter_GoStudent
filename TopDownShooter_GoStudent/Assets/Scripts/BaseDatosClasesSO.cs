using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BaseDatos", fileName = "NuevaBaseDatos" )]
public class BaseDatosClasesSO : ScriptableObject
{
    public List<ClasePersonajeSO> clases = new();

    public ClasePersonajeSO GetByType(TipoPersonaje Tipo)
    {
        for(int i = 0; i < clases.Count; i++)
        {
            if( clases[i].tipoPersonaje == Tipo)
            {
                return clases[i];
            }
        }

        return null; 
    }
}
