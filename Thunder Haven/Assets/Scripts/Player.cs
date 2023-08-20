using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   private Camera mainCamera;

   private float movementX;
   private float movementY;
   
   [Header("Movement")]
   [SerializeField] private float movementSpeed;

   [Space]

   [Header("Shooting")]
   [SerializeField] private Transform barrelTransform;
   [SerializeField] private Transform bulletPrefab;
   private float maxBulletDistance = 250f;
   [SerializeField] private LayerMask bulletLayerMask;

   private Vector2 mousePosition;
   private Vector2 lookDirection;


   private void Start()
   {
      mainCamera = Camera.main;
   }

   private void Update()
   {
      mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      lookDirection = (mousePosition - (Vector2)transform.position).normalized;

      HandleMovement();
      
      if (Input.GetKey(KeyCode.Mouse0))
      {
         Shoot();
      }
   }

   private void HandleMovement()
   {
      movementX = Input.GetAxisRaw("Horizontal");
      movementY = Input.GetAxisRaw("Vertical");

      Vector2 inputDirection = new Vector2(movementX, movementY);
      Vector3 movementDirection = new Vector3(inputDirection.x, inputDirection.y, 0).normalized;

      transform.position += movementDirection * (Time.deltaTime * movementSpeed);

      // Rotation
      transform.up = lookDirection;
      
   }

   private void Shoot()
   {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, maxBulletDistance, bulletLayerMask);

      Bullet bullet = Instantiate(bulletPrefab.gameObject, barrelTransform.position, Quaternion.identity).GetComponent<Bullet>();

      bullet.SetDirection(lookDirection);

      if (hit) {
         Debug.Log("Hit: " + hit.transform.gameObject.name);
      }
   }


}

