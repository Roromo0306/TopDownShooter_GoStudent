using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VFX_FilmGrain : MonoBehaviour
{
    [Range(0, 1)] public float intensidadMax = 0.5f;
    [Min(0)] public float duracionAumento = 0.1f;
    [Min(0)] public float duracionDisminucion = 2f;

    public bool iniciarEfecto = false;

    private FilmGrain filmGrain;

    void Start()
    {
        BuscarFilmGrain();
    }

    void Update()
    {
        if (iniciarEfecto)
        {
            StartCoroutine(FilmGrainLerp());
            iniciarEfecto = false;
        }
    }

    private IEnumerator FilmGrainLerp()
    {
        float cronometro = 0;

        // Fase de aumento
        while (cronometro / duracionAumento < 1)
        {
            cronometro += Time.deltaTime;
            filmGrain.intensity.value = Mathf.Lerp(0, intensidadMax, cronometro / duracionAumento);

            yield return null;
        }

        cronometro = 0;

        // Fase de disminución
        while (cronometro < 1)
        {
            cronometro += Time.deltaTime / duracionDisminucion;
            filmGrain.intensity.value = Mathf.Lerp(intensidadMax, 0, cronometro);

            yield return null;
        }
    }

    void BuscarFilmGrain()
    {
        GameObject globalVolume = GameObject.Find("Global Volume");
        if (globalVolume != null)
        {
            Volume volume = globalVolume.GetComponent<Volume>();

            if (volume != null)
            {
                volume.profile.TryGet(out filmGrain);

                if (filmGrain != null)
                {
                    filmGrain.intensity.overrideState = true; // permitimos que se controle desde código
                }
                else
                {
                    Debug.LogError("No se encontró FilmGrain en el Volume Profile.");
                }
            }
            else
            {
                Debug.LogError("No se ha encontrado la componente Volume");
            }
        }
        else
        {
            Debug.LogError("No se encuentra el objeto Global Volume");
        }
    }
}
