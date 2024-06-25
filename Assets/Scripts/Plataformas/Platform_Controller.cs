using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Controller : MonoBehaviour
{
    public float yMinPosition;             
    public float yMaxPosition;              
    public float currentTime;               
    public float timeToWaitForMovement;     
    public int direction = -1;              
    public float speed = 4;                 
    public bool canWait;                   
    private Rigidbody2D rbd;               

    void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();  
    }
    void FixedUpdate()
    {
        if (!canWait)
        {
            rbd.velocity = new Vector2(0, speed * direction);
        }
        else
        {
            rbd.velocity = Vector2.zero;
            currentTime = currentTime + Time.deltaTime;

            if (currentTime >= timeToWaitForMovement)
            {
                currentTime = 0;
                direction = direction * -1;
                canWait = false;
            }
        }
        if (transform.position.y >= yMaxPosition && direction == 1)
        {
            canWait = true;
        }
        else if (transform.position.y <= yMinPosition && direction == -1)
        {
            canWait = true;
        }
    }
}