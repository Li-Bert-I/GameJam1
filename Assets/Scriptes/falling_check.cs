using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_check : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log(movement.rb.velocity.y);
        }
        if (collision.gameObject.tag.Equals("Ground") && movement.rb.velocity.y < -10)
        {
            Debug.Log("Damages");
        }
    }
}
