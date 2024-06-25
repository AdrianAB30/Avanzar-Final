using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Contador_Monedas;
    public Text Contador_Gemas;
    private int contadorMonedas = 0;
    private int contadorGemas = 0;
    public Slider barraDeVida;

    private void Awake()
    {
        ActualizarContadorMonedas();
        ActualizarContadorGemas();
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
    }
    private void ActualizarContadorGemas()
    {
        Contador_Gemas.text = contadorGemas.ToString();
    }
    public void ActualizarBarraDeVida(int vidaPlayer)
    {
        barraDeVida.value = vidaPlayer;
    }
    public void SetMaxValue(int maxVida)
    {
        barraDeVida.maxValue = maxVida;
        barraDeVida.value = maxVida;
    }
}
