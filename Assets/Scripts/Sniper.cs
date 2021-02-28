using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public GameObject target;

    public GameObject bulletEmitter;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    public float bulletLifeTime = 5.0f;

    public float shootingDelay = 1.0f;
    public float shootingRange = 15.0f;
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
        shooting = IsPlayerClose();

        if (shooting)
        {
            if (shootingTimer >= shootingDelay)
            {
                ShootBullet();
                shootingTimer = 0.0f;
            }
        }
        shootingTimer += Time.deltaTime;

        bool player_on_right = (target.transform.position - transform.position)[0] >= 0;
        if (!looks_right && player_on_right)
        {
            Flip();
        } else if (looks_right && !player_on_right)
        {
            Flip();
        }
    }

    void Flip()
    {
        looks_right = !looks_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
