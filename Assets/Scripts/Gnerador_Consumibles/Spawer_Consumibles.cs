using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawer_Consumibles : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject gemPrefab;
    public float tiempoParaCrearConsumible = 0.5f;
    public float tiempoActualParaCrearConsumible = 0f;
    public float randomX;
    public float maxX = -12f;
    public float minX = -30f;
    public float spawnY = -12f;

    void Start()
    {
        CreateConsumible();
    }
    void CreateConsumible()
    {
        randomX = Random.Range(minX, maxX);

        Vector2 randomPosition = new Vector2(randomX, spawnY);

        GameObject coin = Instantiate(coinPrefab, randomPosition, transform.rotation);
        GameObject gem = Instantiate(gemPrefab, randomPosition, transform.rotation);

    }
    void Update()
    {
        tiempoActualParaCrearConsumible = tiempoActualParaCrearConsumible + Time.deltaTime;

        if(tiempoParaCrearConsumible >= tiempoActualParaCrearConsumible)
        {
            CreateConsumible();
            tiempoActualParaCrearConsumible = 0;
        }    
    }
}
