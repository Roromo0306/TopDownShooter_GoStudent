using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineImpulseSource))] 

public class ActivarRuido : MonoBehaviour
{
    [System.Serializable]
    public struct configuracionesShake
    {
        [Min(0f)] public float Amplitude;
        [Min(0f)] public float Frecuencia;
        [Min(0f)] public float Decay;
        [Min(0f)] public float Sustain;
    }

    public List<configuracionesShake> listaConfiguraciones = new List<configuracionesShake>();
    [Min(0)]public int configuracionElegida = 0;    
    public bool activar = false;

    private CinemachineImpulseSource impulsesource;

    // Start is called before the first frame update
    void Start()
    {
        impulsesource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activar)
        {
            Shake();
            activar = false;
        }

    }

    public void Shake()
    {
        if(listaConfiguraciones.Count == 0)
        {
            return;
        }
        var configuracion = listaConfiguraciones[configuracionElegida];
        var definicionImpulso = impulsesource.m_ImpulseDefinition; //apartado de la componente que contiene la amplitud, frecuencia, decay y sustain
        definicionImpulso.m_AmplitudeGain = configuracion.Amplitude;
        definicionImpulso.m_FrequencyGain = configuracion.Frecuencia;
        definicionImpulso.m_TimeEnvelope.m_DecayTime = configuracion.Decay;
        definicionImpulso.m_TimeEnvelope.m_SustainTime = configuracion.Sustain;

        impulsesource.GenerateImpulse();
    }


}
