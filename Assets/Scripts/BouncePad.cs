using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private float _bouncePower = 25f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _bouncePower);
            //play bounce pad sfx 
            FindObjectOfType<AudioManager>().Play("Jump", 0.8f);
        }
    }
}
