using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySingleUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform selectedSlotVisual;

    public void UpdateVisual(Sprite iconSprite, string itemName)
    {
        itemNameText.text = itemName;
        itemIcon.sprite = iconSprite;

        if (iconSprite == null) {
            itemIcon.enabled = false;
        } else {
            itemIcon.enabled = true;
        }
    }

}
