using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public float fireSpeed;
    public int ammoCount;
    public float reloadSpeed;
    public float damage;
    public new string name;
    public enum FireType{Automatic, Burst, Single}
    public FireType fireType;
}
