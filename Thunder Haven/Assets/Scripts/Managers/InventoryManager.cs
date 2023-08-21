using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSO;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance { get; private set; }

    public event EventHandler OnSelectedSlotChanged;
    public event EventHandler OnPrimaryChanged;
    public event EventHandler OnSecondaryChanged;

    public event EventHandler<OnSelectedSlotChangedUIEventArgs> OnSelectedSlotChangedUI;
    public class OnSelectedSlotChangedUIEventArgs : EventArgs {
        public Slot selectedSlot;
    }

    
    private GunSO primaryGunSO;
    private GunSO secondaryGunSO;

    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayerMask;

    public enum Slot { Primary, Secondary, Knife, Spike }
    private Slot selectedSlot;

    private bool playerHasSpike;




    private void Awake()
    {
        Instance = this;

        selectedSlot = Slot.Knife;
    }

    private void Update()
    {

        UpdateSlot();


        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.CircleCast(Player.Instance.transform.position, pickupRange, Vector2.down, 10f, pickupLayerMask);
            if (hit)
            {
                DroppedItem droppedItem = hit.transform.GetComponent<DroppedItem>();

                if (droppedItem.IsPrimary()) {
                    PickupPrimaryItem(droppedItem);
                } else if (droppedItem.IsSecondary()) {
                    PickupSecondaryItem(droppedItem);
                } else {
                    //PickupSpike();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (selectedSlot == Slot.Primary && primaryGunSO != null) {
                DropPrimaryItem();
            } else if (selectedSlot == Slot.Secondary && secondaryGunSO != null) {
                DropSecondaryItem();
            } else if (selectedSlot == Slot.Spike && playerHasSpike) {
                //DropSpike();
            }
        }
    }



    public void PickupPrimaryItem(DroppedItem droppedItem)
    {
        if (primaryGunSO != null) {
            // Already has a primary
            DropPrimaryItem();
        }

        primaryGunSO = droppedItem.GetGunSO();

        droppedItem.DestroySelf();
        OnPrimaryChanged?.Invoke(this, EventArgs.Empty);
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }
    public void PickupSecondaryItem(DroppedItem droppedItem)
    {
        if (secondaryGunSO != null) {
            // Already has a secondary
            DropSecondaryItem();
        }

        primaryGunSO = droppedItem.GetGunSO();

        droppedItem.DestroySelf();
        OnSecondaryChanged?.Invoke(this, EventArgs.Empty);
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }



    public void DropPrimaryItem()
    {
        Transform droppedPrefab = primaryGunSO.droppedPrefab;

        RemoveItem(WeaponType.Primary);
        
        Instantiate(droppedPrefab, Player.Instance.transform.position, Quaternion.identity);
        OnPrimaryChanged?.Invoke(this, EventArgs.Empty);
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }
    public void DropSecondaryItem()
    {
        Transform droppedPrefab = primaryGunSO.droppedPrefab;

        RemoveItem(WeaponType.Secondary);
        
        Instantiate(droppedPrefab, Player.Instance.transform.position, Quaternion.identity);
        OnSecondaryChanged?.Invoke(this, EventArgs.Empty);
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }


    private void RemoveItem(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Primary) {
            primaryGunSO = null;
        } else {
            secondaryGunSO = null;
        }
    }



    private void UpdateSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && primaryGunSO != null) {
            selectedSlot = Slot.Primary;
            OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
            OnSelectedSlotChangedUI?.Invoke(this, new OnSelectedSlotChangedUIEventArgs {
                selectedSlot = selectedSlot
            });
            
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && secondaryGunSO != null) {
            selectedSlot = Slot.Secondary;
            OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
            OnSelectedSlotChangedUI?.Invoke(this, new OnSelectedSlotChangedUIEventArgs {
                selectedSlot = selectedSlot
            });

        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedSlot = Slot.Knife;
            OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
            OnSelectedSlotChangedUI?.Invoke(this, new OnSelectedSlotChangedUIEventArgs {
                selectedSlot = selectedSlot
            });

        } else if (Input.GetKeyDown(KeyCode.Alpha4) && playerHasSpike) {
            selectedSlot = Slot.Spike;
            OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
            OnSelectedSlotChangedUI?.Invoke(this, new OnSelectedSlotChangedUIEventArgs {
                selectedSlot = selectedSlot
            });

        }
    }

    public GunSO GetActiveGunSO()
    {
        if (selectedSlot == Slot.Primary) {
            return primaryGunSO;
        } else if (selectedSlot == Slot.Secondary) {
            return secondaryGunSO;
        }

        Debug.LogWarning("Returning GunSO as null");
        return null;
    }

    public GunSO GetPrimaryGunSO()
    {
        return primaryGunSO;
    }
    public GunSO GetSecondaryGunSO()
    {
        return secondaryGunSO;
    }
}
