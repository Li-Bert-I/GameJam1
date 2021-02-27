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
    public float spikesAdrenalin = 0.3f;
    public float wireAdrenalin = 0.3f;
    public float fallFreezeDelay = 1.0f;
    public float wireSpeedRation = 0.6f;
    public float adrenalinPower = 1.0f;

    public Image Bar;

    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rb;
    private BoxCollider2D cc;
    private bool look_right = true;
    private float adrenalinLevel;
    public bool is_grounded;
    private float speed = 3.0f;
    private float freezedDelay = 0.0f;
    private bool inWire = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        adrenalinLevel = Bar.fillAmount;
        speed = adrenalinSpeed * Mathf.Pow(adrenalinLevel, adrenalinPower) + baseSpeed;
        if (inWire) {
            speed *= wireSpeedRation;
        }

        float mov_x = Input.GetAxis("Horizontal");

        if (freezedDelay > 0)
        {
            freezedDelay -= Time.fixedDeltaTime;
        } else
        {
            if (!look_right && (mov_x > 0))
            {
                Flip();
            } else if (look_right && (mov_x < 0))
            {
                Flip();
            }

            float mov_speed = mov_x * speed;
            transform.Translate(mov_speed * Time.fixedDeltaTime, 0, 0);
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (mov_speed == 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }

    }

    void Update()
    {
        if (inWire) {
            Bar.fillAmount = Bar.fillAmount + wireAdrenalin * Time.deltaTime;
        }

        bool new_grounded_state = IsGrounded();
        if (new_grounded_state && !is_grounded)
        {
            if (rb.velocity.y < -critical_speed)
            {
                Debug.Log("Damaged");
                Bar.fillAmount = Bar.fillAmount + fallingAdrenalin;
                freezedDelay = fallFreezeDelay;
            }
        }
        is_grounded = new_grounded_state;

        if (is_grounded && Input.GetKeyDown(KeyCode.Space) && freezedDelay <= 0)
        {
            rb.velocity += Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(cc.bounds.center, Vector2.down, cc.bounds.extents.y * 1.5f + extraHeight, platformLayerMask);
        Color rayColor = Color.red;
        if (raycastHit.collider != null)
            rayColor = Color.green;
        Debug.DrawRay(cc.bounds.center, Vector2.down, rayColor, cc.bounds.extents.y * 1.5f + extraHeight);
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

        if(other.tag == "Barbwire") {
            Debug.Log("Wire");
            inWire = true;
        }

        if(other.tag == "Spikes") {
            Debug.Log("Spikes");
            Bar.fillAmount = Bar.fillAmount + bulletAdrenalin;
            freezedDelay = 1.0f;
            int direction = look_right ? 1 : -1;
            rb.velocity = new Vector3(direction * 10.0f, 20.0f, 0);
        }
    }

    // Detect collision exit with floor
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Barbwire") {
            inWire = false;
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
