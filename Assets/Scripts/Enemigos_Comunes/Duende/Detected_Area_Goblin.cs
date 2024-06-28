using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected_Area : MonoBehaviour
{
    public Goblin_Controller goblin_Controller;

    public void SetGoblin(Goblin_Controller goblin)
    {
        goblin_Controller = goblin;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            goblin_Controller.PlayerDetected();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            goblin_Controller.PlayerLost();
        }
    }
}
