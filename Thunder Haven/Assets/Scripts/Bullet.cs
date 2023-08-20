using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private float bulletSpeed = 30f;
    private float maxLifetime = 1f;

    private float lifetimeTimer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }


    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Other collider isn't also a bullet
        if (other.gameObject.GetComponent<Bullet>() == null)
        {
            Destroy(gameObject);
        }
    }

}
