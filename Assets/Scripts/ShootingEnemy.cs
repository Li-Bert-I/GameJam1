using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject target;
    public Transform pathRight;
    public Transform pathLeft;

    public GameObject bulletEmitter;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    public float bulletLifeTime = 5.0f;

    public float shootingDelay = 1.0f;
    public float shootingRange = 10.0f;
    public float speed = 1.0f;

    public AudioClip[] shotSounds;

    private bool shooting;
    private bool walking;
    private AudioSource audioSrc;
    private bool walks_right = true;
    private bool looks_right = false;
    private float shootingTimer = 0.0f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    bool IsPlayerClose()
    {
        return (target.transform.position - transform.position).magnitude <= shootingRange;
    }

    bool SeePlayer()
    {
        return (target.transform.position - transform.position)[0] >= 0 == walks_right;
    }

    bool TooRight()
    {
        if (pathRight.position[0] <= transform.position[0])
            return true;
        return false;
    }

    bool TooLeft()
    {
        if (pathLeft.position[0] >= transform.position[0])
            return true;
        return false;
    }

    void ShootBullet()
    {
        Vector3 shootDirection = (target.transform.position - bulletEmitter.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        // rotation = Quaternion.identity;
        GameObject b = Instantiate(bulletPrefab, bulletEmitter.transform.position, rotation) as GameObject;
        b.transform.Rotate(0, 0, -90);
        b.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        Destroy(b, bulletLifeTime);
        audioSrc.pitch = 1 + Random.Range(-0.2f, 0.2f);
        audioSrc.PlayOneShot(shotSounds[Random.Range(0, shotSounds.Length)], 0.8f);
    }

    void FixedUpdate()
    {
        shooting = IsPlayerClose() && SeePlayer();
        walking = !shooting;

        if (shooting)
        {
            if (shootingTimer >= shootingDelay)
            {
                ShootBullet();
                shootingTimer = 0.0f;
            }
        }
        shootingTimer += Time.fixedDeltaTime;

        if (!looks_right && walks_right)
        {
            Flip();
        } else if (looks_right && !walks_right)
        {
            Flip();
        }

        if (walking)
        {
            float mov_x = 1.0f;
            if (!walks_right)
                 mov_x = -1.0f;
            transform.Translate(mov_x * speed * Time.fixedDeltaTime, 0, 0);
            walks_right = (TooLeft() || TooRight()) ? (TooLeft() && !TooRight()) : (walks_right);
        }
    }

    void Flip()
    {
        Debug.Log("Flipped");
        looks_right = !looks_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
