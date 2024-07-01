using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Goblin : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject goblinPrefab;
    public Transform spawnpoint;
    public float spawnInterval = 10f;
    public GameObject player;
    public SFXManager SFXManager;
    //public UIManager uIManager;

    void Start()
    {
        InvokeRepeating("SpawnGoblin", 0f, spawnInterval);
    }
    public void SpawnGoblin()
    {
        GameObject goblin = Instantiate(goblinPrefab,spawnpoint.position,spawnpoint.rotation);
        Goblin_Controller goblin_Controller = goblin.GetComponent<Goblin_Controller>();
        if (goblin_Controller != null)
        {
            goblin_Controller.Target = player;
            goblin_Controller.SFXManager = SFXManager;
            //skeleton_Controller.uiManager = uiManager;
            goblin_Controller.WalkingGoblin();
        }
    }
}
