using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    TextMeshProUGUI text => GetComponentInChildren<TextMeshProUGUI>();

    private void OnEnable()
    {
        Inventory.Instance.SubscribeInventoryPanel(this);
    }

    private void OnDisable()
    {
        Inventory.Instance.SubscribeInventoryPanel(null);
    }

    public void UpdateText(string inv) => text.text = inv;

}
