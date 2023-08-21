using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunInfoUI : MonoBehaviour
{
   [SerializeField]private TextMeshProUGUI ammoText;
   
   private void LateUpdate()
   {
    ammoText.text = "Ammo: " + Player.Instance.GetAmmo().ToString();
   }
}
