using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMenu : MonoBehaviour
{
    public RawImage _img;  
    public float _x;  
    private void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, 0) * Time.deltaTime, _img.uvRect.size);
    }
}

