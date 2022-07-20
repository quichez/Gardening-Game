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
}
