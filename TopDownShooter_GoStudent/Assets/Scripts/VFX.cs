using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

public class VFX : MonoBehaviour
{
    [Range(0, 1)] public float intensidadMaxVineteo = 0.5f;
    [Range(0, 1)] public float smoothnessMaxVineteo = 0.5f;

    [UnityEngine.Min(0f)] public float duracionAumento = 0.1f;
    [UnityEngine.Min(0f)] public float duracionDisminucion = 2f;

    public bool iniciarEfecto = false;

    private Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        BuscarVignette();
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciarEfecto)
        {
            StartCoroutine(VignetteLerp());
            iniciarEfecto=false;
        }
    }

    private IEnumerator VignetteLerp()
    {
        float cronometro = 0;

        while (cronometro/duracionAumento < 1)
        {
            cronometro += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(0, intensidadMaxVineteo, cronometro/duracionAumento);
            vignette.smoothness.value = Mathf.Lerp(0, intensidadMaxVineteo, cronometro / duracionAumento);

            yield return null; //Espera al siguiente frame
        }

        cronometro = 0;

        while (cronometro < 1)
        {
            cronometro += Time.deltaTime/duracionDisminucion;
            vignette.intensity.value = Mathf.Lerp(intensidadMaxVineteo, 0 , cronometro );
            vignette.smoothness.value = Mathf.Lerp(intensidadMaxVineteo, 0, cronometro );

            yield return null; //Espera al siguiente frame
        }
    }

    void BuscarVignette()
    {
        GameObject globalVolume = GameObject.Find("GlobalVolume");
        if(globalVolume != null)
        {
            PostProcessVolume volume = globalVolume.GetComponent<PostProcessVolume>();

            if(volume != null && volume.profile != null)
            {
                

                if(volume.profile.TryGetSettings(out vignette))
                {
                    vignette.intensity.overrideState = true; //Permite sobreescribir el valor de la intensidad mediante código
                    vignette.smoothness.overrideState = true;
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
