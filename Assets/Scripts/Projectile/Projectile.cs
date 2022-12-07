using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifetime;

    [HideInInspector]
    public float speed;
    // Update is called once per frame
    void Start()
    {
        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.CompareTag("PlayerProjectile"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {

                collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.instance.lives--;
            }
        }
           
    
    }

}

    
