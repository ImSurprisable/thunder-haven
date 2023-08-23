using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSO;
#pragma warning disable IDE0052
#pragma warning disable IDE0044

public class DroppedItem : MonoBehaviour
{

    [SerializeField] private GunSO gunSO;
    private int ammoCount;



    public GunSO GetGunSO()
    {
        return gunSO;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public bool IsPrimary()
    {
        return gunSO.weaponType == WeaponType.Primary;
    }
    public bool IsSecondary()
    {
        return gunSO.weaponType == WeaponType.Secondary;
    }
    public bool IsSpike()
    {
        // Spike not implemented yet
        return false;
    }

    public void SetAmmoCount(int ammoCount)
    {
        this.ammoCount = ammoCount;
    }

}
