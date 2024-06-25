using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private BoxCollider2D attackArea;
    private void Awake()
    {
        attackArea = GetComponent<BoxCollider2D>();
    }
}
