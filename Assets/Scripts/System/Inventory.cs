using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Items;

public class Inventory : MonoBehaviour
{
    public readonly List<Item> items;

    public bool AddItem(Item item)
    {

        return false;
    }

    public bool RemoveItem(Item item)
    {

        return false;
    }

    public void DegradeQualityOfItems()
    {
        foreach (IQuality item in items)
        {
            if(item is IQualityDegrade qualDegradeItem)
            {
                qualDegradeItem.AddToDegradeTimer();
            }
        }
    }

    public string InventoryToString()
    {
        var output = new System.Text.StringBuilder();
        foreach (Item item in items)
        {
            string itemName = item.name;
            if (item is IStackable stack)
                itemName = stack.quantity.ToString() + "   " + itemName;
            if (item is IQuality quality)
                itemName += "{ " + quality.ToString() + " }";
            
            output.AppendLine(itemName);
        }

        return output.ToString();
    }
}
