using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Generador_Skeleton generadorSkeleton;
    public Generador_Goblin generadorGoblin;

    public Text Contador_Monedas;
    public Text Contador_Gemas;
    private int contadorMonedas = 0;
    private int contadorGemas = 0;
    public Slider barraDeVida;
    public GameObject flechaContinuar;

    private void Awake()
    {
        ActualizarContadorMonedas();
        ActualizarContadorGemas();
        IniciarBarradeVida(100);

    }
    public void IncrementarMoneda()
    {
        contadorMonedas++;
        ActualizarContadorMonedas();
    }
    private void ActualizarContadorMonedas()
    {
        Contador_Monedas.text = contadorMonedas.ToString();
    }

    public void IncrementarGemas()
    {
        contadorGemas++;
        ActualizarContadorGemas();
        if (contadorGemas >= 3)
        {
            //ActivarBoss();
            DesactivarSpawners();
            ActivarFlecha();
        }
    }
    private void ActualizarContadorGemas()
    {
        Contador_Gemas.text = contadorGemas.ToString();
    }
    public void CambiarVidaMaxima(int vidaMaxima)
    {
        barraDeVida.maxValue = vidaMaxima;
    }
    public void CambiarVidaActual(int cantidadVida)
    {
        barraDeVida.value = cantidadVida;
    }
    public void IniciarBarradeVida(int cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
    private void DesactivarSpawners()
    {
        if (generadorSkeleton != null)
        {
            generadorSkeleton.gameObject.SetActive(false);
        }

        if (generadorGoblin != null)
        {
            generadorGoblin.gameObject.SetActive(false);
        }
    }
    private void ActivarFlecha()
    {
        flechaContinuar.gameObject.SetActive(true);
    }
}
