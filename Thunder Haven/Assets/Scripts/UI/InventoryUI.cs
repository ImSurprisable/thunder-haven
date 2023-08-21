using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        primaryInventorySlot.UpdateVisual(null, "Empty");
        secondaryInventorySlot.UpdateVisual(null, "Empty");
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
