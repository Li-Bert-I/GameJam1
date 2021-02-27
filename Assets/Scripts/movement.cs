using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float jump_force;
    private Rigidbody2D rb;
    private bool look_right = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float mov_x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        }
        //rb.MovePosition (rb.position + Vector2.right * f * speed * Time.deltaTime);
        transform.Translate(mov_x * speed * Time.deltaTime, 0, 0);
        mov_x = 0;
    }

    void Flip()
    {

    }
}
