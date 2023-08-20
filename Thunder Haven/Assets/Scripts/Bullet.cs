using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Vector3 target;
    private float bulletSpeed = 30f;
    private float maxDistance = 100f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        rb.velocity = (target - transform.position) * bulletSpeed;



    }

    





}
