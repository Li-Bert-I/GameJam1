using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private float speed = 3;
    private float fill;
    public float jump_force;
    public static Rigidbody2D rb;
    private bool look_right = true;
    private bool is_grounded;
    public Transform ground_check;
    public float check_radius;
    public LayerMask what_is_groud;
    public Image Bar;
    public float baseSpeed = 2.0f;
    public float adrenalinSpeed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        fill = Bar.fillAmount;
        //speed = adrenalinSpeed * fill + baseSpeed;
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
            rb.gravityScale = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            }
        }
        else
        {
            rb.gravityScale = 1;
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
