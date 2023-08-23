using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSO;
#pragma warning disable IDE0051
#pragma warning disable IDE0044
#pragma warning disable IDE0090

public class Player : MonoBehaviour
{
   public static Player Instance { get; private set; }



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

   [SerializeField]private GunSO activeWeapon;
   private float fireTimer;
   private int ammoCount;
   private bool isReloading;


   private Vector2 mousePosition;
   private Vector2 lookDirection;

   private void Awake()
   {
      Instance=this;
   }
   private void Start()
   {
      mainCamera = Camera.main;

      InventoryManager.Instance.OnSelectedSlotChanged += InventoryManager_OnSelectedSlotChanged;
   }

   private void InventoryManager_OnSelectedSlotChanged(object sender, EventArgs e)
   {
      GunSO lastActiveWeapon = activeWeapon;
      activeWeapon = InventoryManager.Instance.GetActiveGunSO();

      if (lastActiveWeapon != activeWeapon) {
         ammoCount = activeWeapon.ammoCount;
      }
   }

    private void Update()
   {
      mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      lookDirection = (mousePosition - (Vector2)transform.position).normalized;

      HandleMovement();

      if (Input.GetKeyDown(KeyCode.R) && !isReloading)
      {
         StartReload(activeWeapon.reloadSpeed);
      }

      if (fireTimer <=0f && ammoCount>0 && !isReloading)
      {
         switch(activeWeapon.fireType)
         {
            case FireType.Single:
               if (Input.GetKeyDown(KeyCode.Mouse0))
               {
                  Shoot();
                  fireTimer = activeWeapon.fireSpeed;
               }  
               break;

            case FireType.Burst:
               if (Input.GetKeyDown(KeyCode.Mouse0))
                  {
                     Shoot();
                     fireTimer = activeWeapon.fireSpeed;
                  }  
               break;

            case FireType.Automatic:
               if (Input.GetKey(KeyCode.Mouse0))
                  {
                     Shoot();
                     fireTimer = activeWeapon.fireSpeed;
                  }  
               break;

         }
      }

      fireTimer -= Time.deltaTime;
   }

   private void StartReload(float reloadTime)
   {
      isReloading = true;
      Invoke(nameof(Reload),reloadTime);
   }

   private void Reload()
   {
      isReloading = false;
      ammoCount = activeWeapon.ammoCount;
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

      ammoCount--; 
   }

   public int GetAmmo()
   {
      return ammoCount;
   }

}

