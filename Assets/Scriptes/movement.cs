using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float jump_force;
    private Rigidbody2D rb;
    private bool look_right = true;
    private bool is_grounded;
    public Transform ground_check;
    public float check_radius;
    public LayerMask what_is_groud;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, check_radius, what_is_groud);
        float mov_x = Input.GetAxis("Horizontal");

        if (!look_right && (mov_x > 0))
        {
            Flip();
        } else if (look_right && (mov_x < 0))
        {
            Flip();
        }

        transform.Translate(mov_x * speed * Time.deltaTime, 0, 0);
        mov_x = 0;

        if (is_grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            }
        }

    }

    void Flip()
    {
        look_right = !look_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
