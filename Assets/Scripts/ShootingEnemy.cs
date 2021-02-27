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

    private bool shooting;
    private bool walking;
    private bool walks_right = true;
    private float shootingTimer = 0.0f;

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
        Vector3 shootDirection = (target.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        // rotation = Quaternion.identity;
        GameObject b = Instantiate(bulletPrefab, bulletEmitter.transform.position, rotation) as GameObject;
        b.transform.Rotate(0, 0, -90);
        b.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        Destroy(b, bulletLifeTime);
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
        shootingTimer += Time.deltaTime;

        if (walking)
        {
            float mov_x = 1.0f;
            if (!walks_right)
                 mov_x = -1.0f;
            transform.Translate(mov_x * speed * Time.deltaTime, 0, 0);
            walks_right = (TooLeft() || TooRight()) ? (TooLeft() && !TooRight()) : (walks_right);
        }
    }
}
