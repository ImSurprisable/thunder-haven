using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : ScriptableObject
{

    public enum FireType{ Automatic, Burst, Single }
    public enum WeaponType{ Primary, Secondary }

    
    public Transform droppedPrefab;

    [Space]

    public new string name;
    public Sprite heldSprite;
    public Sprite iconSprite;

    [Space]
    
    public float damage;
    public int ammoCount;
    public float reloadSpeed;

    [Space]

    public float fireSpeed;
    public FireType fireType;

    [Space]

    public WeaponType weaponType;
}
