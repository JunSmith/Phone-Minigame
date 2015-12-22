using UnityEngine;
using System;

[CLSCompliant(false)]
public class GameTargetSquare : MonoBehaviour
{    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Vector2.Distance(gameObject.transform.position, mousePosition) < 1)            
                Destroy(gameObject);
            
        }
    }
}
