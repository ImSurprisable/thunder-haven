using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    [SerializeField] private GunSO gunSO;



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
        return gunSO.weaponType == GunSO.WeaponType.Primary;
    }
    public bool IsSecondary()
    {
        return gunSO.weaponType == GunSO.WeaponType.Secondary;
    }
    public bool IsSpike()
    {
        // Spike not implemented yet
        return false;
    }

}
