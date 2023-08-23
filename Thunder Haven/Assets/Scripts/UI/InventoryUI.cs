using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private InventorySingleUI primaryInventorySlot;
    [SerializeField] private InventorySingleUI secondaryInventorySlot;
    [SerializeField] private InventorySingleUI knifeInventorySlot;
    [SerializeField] private InventorySingleUI spikeInventorySlot;

    


    private void Start()
    {
        InventoryManager.Instance.OnPrimaryChanged += InventoryManager_OnPrimaryChanged;
        InventoryManager.Instance.OnSecondaryChanged += InventoryManager_OnSecondaryChanged;
        InventoryManager.Instance.OnSelectedSlotChangedUI += InventoryManager_OnSelectedSlotChangedUI;

        primaryInventorySlot.UpdateVisual(null, "Empty");
        secondaryInventorySlot.UpdateVisual(null, "Empty");
    }

    private void InventoryManager_OnSelectedSlotChangedUI(object sender, InventoryManager.OnSelectedSlotChangedUIEventArgs e)
    {
        switch (e.selectedSlot)
        {
            case Slot.Primary:
                
                break;
            case Slot.Secondary:
            
                break;
            case Slot.Knife:
                break;
            case Slot.Spike:
                break;
        }
    }

    private void InventoryManager_OnSecondaryChanged(object sender, EventArgs e)
    {
        GunSO secondaryGunSO = InventoryManager.Instance.GetSecondaryGunSO();

        if (secondaryGunSO == null) {
            secondaryInventorySlot.UpdateVisual(null, "Empty");
        } else {
            secondaryInventorySlot.UpdateVisual(secondaryGunSO.iconSprite, secondaryGunSO.name);
        }
    }

    private void InventoryManager_OnPrimaryChanged(object sender, EventArgs e)
    {
        GunSO primaryGunSO = InventoryManager.Instance.GetPrimaryGunSO();

        if (primaryGunSO == null) {
            primaryInventorySlot.UpdateVisual(null, "Empty");
        } else {
            primaryInventorySlot.UpdateVisual(primaryGunSO.iconSprite, primaryGunSO.name);
        }
    }

}
