using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float tiempoParaSpawnearFireball = 2f; 
    private float tiempoActualFireball = 0f;
    public float randomY;
    public float maxY = 5f;
    public float minY = -5f;
    public float spawnX = -60f; 
    private bool spawnerActivo = true;

    void Start()
    {
        tiempoActualFireball = tiempoParaSpawnearFireball;
    }

    void CreateFireball()
    {
        if (!spawnerActivo) return;

        randomY = Random.Range(minY, maxY);
        Vector2 randomPosition = new Vector2(spawnX, randomY);

        if (tiempoActualFireball >= tiempoParaSpawnearFireball)
        {
            Instantiate(fireballPrefab, randomPosition, transform.rotation);
            tiempoActualFireball = 0f;
        }
    }

    void Update()
    {
        tiempoActualFireball += Time.deltaTime;

        if (spawnerActivo)
        {
            CreateFireball();
        }
    }
}