using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{

    public float speed = 10f;
    public Rigidbody2D rb;
    public bool canMove;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float xDelta = Input.GetAxis("Horizontal");
            float yDelta = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(xDelta, yDelta);

            transform.Translate(move * speed * Time.deltaTime);
        }
    }

    
}
