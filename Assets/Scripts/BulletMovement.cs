using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Ground"){
            // GameObject e = Instantiate(explosion) as GameObject;
            // e.transform.position = transform.position;
            Destroy(this.gameObject, 0.05f);
        }
    }
}
