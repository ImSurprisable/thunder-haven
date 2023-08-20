using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private float movementX;
   private float movementY;
   private Camera mainCamera;

   [SerializeField] private float movementSpeed;
   [SerializeField] private Transform barrelTransform;
   [SerializeField] private Transform bulletPrefab;


   private void Start()
   {
      mainCamera = Camera.main;
   }

   private void Update()
   {
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
      Vector3 movementDirection = new Vector3(inputDirection.x, inputDirection.y, 0);
      transform.position += movementDirection * Time.deltaTime * movementSpeed;



      Vector3 mousePosition = Input.mousePosition;
      Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
      transform.up = mouseWorldPosition - new Vector2(transform.position.x, transform.position.y);
      
   }

   private void Shoot()
   {
      Bullet Bullet = Instantiate(bulletPrefab.gameObject, barrelTransform.position, Quaternion.identity).GetComponent<Bullet>();
      Bullet.SetTarget(mainCamera.ScreenToWorldPoint(Input.mousePosition));
   }


}

