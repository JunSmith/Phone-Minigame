using UnityEngine;
using System;

[CLSCompliant(false)]
public class GameKeepUpBar : MonoBehaviour
{
    private float defaultVertPos = -4.5f;
    private int horiLimit = 8;

	public void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float horiPos = Mathf.Clamp(mousePosition.x, -horiLimit, horiLimit);

            gameObject.transform.position = new Vector3(horiPos, defaultVertPos, -1); 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
