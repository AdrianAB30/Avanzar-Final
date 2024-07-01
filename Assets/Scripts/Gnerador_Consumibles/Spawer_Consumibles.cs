using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawer_Consumibles : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject gemPrefab;
    public float tiempoParaSpawnearCoin = 20f;
    public float tiempoParaSpawnearGem = 25f;
    private float tiempoActualCoin = 0f;
    private float tiempoActualGem = 0f;
    public float randomX;
    public float maxX = -10f;
    public float minX = -30f;
    public float spawnY = 12f;
    private int cantidadCoin = 0;
    private int cantidadGem = 0;
    private bool spawnerActivo = true;

    void Start()
    {
        tiempoActualCoin = tiempoParaSpawnearCoin;
        tiempoActualGem = tiempoParaSpawnearGem;
    }

    void CreateConsumible()
    {
        if (!spawnerActivo) return;

        randomX = Random.Range(minX, maxX);
        Vector2 randomPosition = new Vector2(randomX, spawnY);

        if (tiempoActualCoin >= tiempoParaSpawnearCoin && cantidadCoin < 3)
        {
            Instantiate(coinPrefab, randomPosition, transform.rotation);
            tiempoActualCoin = 0f; 
            cantidadCoin++;
        }

        if (tiempoActualGem >= tiempoParaSpawnearGem && cantidadGem < 3)
        {
            Instantiate(gemPrefab, randomPosition, transform.rotation);
            tiempoActualGem = 0f;
            cantidadGem++;
        }
        if (cantidadCoin >= 3 && cantidadGem >= 3)
        {
            spawnerActivo = false;
            Debug.Log("Ya no hay consumibles papi");
        }
    }
    void Update()
    {
        tiempoActualCoin += Time.deltaTime;
        tiempoActualGem += Time.deltaTime;

        if (spawnerActivo)
        {
            CreateConsumible();
        }
    }
}