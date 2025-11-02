using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculadorParticulas : MonoBehaviour
{
    [Header("Curva de emisión")]
    public AnimationCurve[] emissionCurves;
    // public AnimationCurve emission = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float minEmission = 0f;
    public float maxEmission = 5f;
    public float cicloEmission = 5f;

    [Header("Curva de tiempo de vida")]
    public AnimationCurve tiempoVida = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float minTiempoVida = 0f;
    public float maxTiempoVida = 5f;


    [Header("Color de partículas")]
    public Gradient colores;

    private int indiceCurva = 0; // Controla que curva se usa
    private float cronometro = 0f;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AumentarCronometro(); //Aumenta el tiempo del cronómetro
        CambioEmision(); //Controla la emisión de los rayos
        CambioTiempoVida(); //Controla el tiempo de vida de los rayos
        CambioColor();


    }

    void AumentarCronometro()
    {
        cronometro += Time.deltaTime;
        if(cronometro >= cicloEmission)
        {
            cronometro = 0;

            indiceCurva++;
            int maxIndex = Mathf.Max(emissionCurves.Length);
            if (indiceCurva >= maxIndex)
            {
                indiceCurva = 0; // vuelve a empezar
            }
        }
    }

    void CambioEmision()
    {
        if (emissionCurves.Length == 0) return;

        AnimationCurve curvaActual = emissionCurves[indiceCurva % emissionCurves.Length];
        float totalEmission = minEmission + curvaActual.Evaluate(cronometro / cicloEmission) * (maxEmission - minEmission);

        var canalEmision = particles.emission;
        canalEmision.rateOverTime = new ParticleSystem.MinMaxCurve(totalEmission);
    }

    void CambioTiempoVida()
    {
        float totalVida = minTiempoVida + tiempoVida.Evaluate(cronometro / maxTiempoVida) * (maxTiempoVida - minTiempoVida);
        var canalMain = particles.main;
        canalMain.startLifetime = new ParticleSystem.MinMaxCurve(totalVida);
    }


    void CambioColor()
    {
        float t = cronometro / cicloEmission; 
        var canalMain = particles.main;
        canalMain.startColor = colores.Evaluate(t); 
    }
}
