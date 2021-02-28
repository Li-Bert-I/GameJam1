using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject bulletEmitter;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5.0f;
    public float bulletLifeTime = 10.0f;
    public GameObject target;
    public float shootingDelay = 1.0f;

    private bool shoot;
    private float shootingTimer = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
        if (shoot)
        {
            if (shootingTimer >= shootingDelay)
            {
                ShootBullet();
                shootingTimer = 0.0f;
            }
        }
        shootingTimer += Time.fixedDeltaTime;
    }

    void ShootBullet()
    {
        Vector3 shootDirection = (target.transform.position - bulletEmitter.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        // rotation = Quaternion.identity;
        GameObject b = Instantiate(bulletPrefab, bulletEmitter.transform.position, rotation) as GameObject;
        b.transform.Rotate(0, 0, 90);
        b.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        Destroy(b, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            shoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            shoot = false;
        }
    }
}
