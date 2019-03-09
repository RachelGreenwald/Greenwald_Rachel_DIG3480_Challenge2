using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move4 : MonoBehaviour
{
    private bool dirRight = true;
    public float speed = 1.0f;

    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        if (transform.position.x >= -20.5)
        {
            dirRight = false;
        }
        if (transform.position.x <= -30.5)
        {
            dirRight = true;
        }
    }
}
