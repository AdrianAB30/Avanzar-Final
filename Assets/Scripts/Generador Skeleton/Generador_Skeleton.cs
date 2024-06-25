using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Skeleton : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 15f;
    public GameObject player;
    public UIManager uiManager;

    void Start()
    {
        InvokeRepeating("SpawnSkeleton", 0f, spawnInterval);
    }
    public void SpawnSkeleton()
    {
        GameObject skeleton = Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);
        Skeleton_Controller skeleton_Controller = skeleton.GetComponent<Skeleton_Controller>();
        if(skeleton_Controller != null)
        {
            skeleton_Controller.Target = player;
            skeleton_Controller.uiManager = uiManager;
            skeleton_Controller.Walking();
        }
    }
}
