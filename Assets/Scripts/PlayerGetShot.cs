using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetShot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "bullet"){
            // GameObject e = Instantiate(explosion) as GameObject;
            // e.transform.position = transform.position;
            Destroy(other.gameObject);
        }
    }
}
