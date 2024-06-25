using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Player player;
    public bool limitsCamera = false;

    void Update()
    { 
        if(player != null)
        {
           if(player.transform.position.x >= -7.524005 || player.transform.position.x <= -36.39204)
            {
                limitsCamera = true;
            }
            else
            {
                limitsCamera = false;
            }
            if (limitsCamera == false)
            {
                Vector2 newCamPosition = new Vector2(player.transform.position.x, 0);
                transform.position = new Vector3(newCamPosition.x, transform.position.y, transform.position.z);
            }
        }  
    }
}
