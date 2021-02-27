using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public float baseSpeed = 2.0f;
    public float adrenalinSpeed = 1.0f;
    public float jumpVelocity = 70.0f;
    public float critical_speed = 10.0f;
    public float fallingAdrenalin = 0.3f;
    public float bulletAdrenalin = 0.3f;

    public Image Bar;

    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private bool look_right = true;
    private float adrenalinLevel;
    public bool is_grounded;
    private float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        adrenalinLevel = Bar.fillAmount;
        speed = adrenalinSpeed * adrenalinLevel + baseSpeed;

        float mov_x = Input.GetAxis("Horizontal");

        if (!look_right && (mov_x > 0))
        {
            Flip();
        } else if (look_right && (mov_x < 0))
        {
            Flip();
        }

        float mov_speed = mov_x * speed;
        transform.Translate(mov_speed * Time.deltaTime, 0, 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (mov_speed == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        bool new_grounded_state = IsGrounded();
        if (new_grounded_state && !is_grounded)
        {
            if (rb.velocity.y < -critical_speed)
            {
                Debug.Log("Damaged");
                Bar.fillAmount = Bar.fillAmount + fallingAdrenalin;
            }
        }
        is_grounded = new_grounded_state;

        if (is_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(cc.bounds.center, Vector2.down, cc.size.y + extraHeight, platformLayerMask);
        Color rayColor = Color.red;
        if (raycastHit.collider != null)
            rayColor = Color.green;
        Debug.DrawRay(cc.bounds.center, Vector2.down, rayColor, cc.size.y + extraHeight);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "bullet"){
            // GameObject e = Instantiate(explosion) as GameObject;
            // e.transform.position = transform.position;
            Debug.Log("Bullet");
            Destroy(other.gameObject);
            Bar.fillAmount = Bar.fillAmount + bulletAdrenalin;
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
