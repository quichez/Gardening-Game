using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GardeningGame.Items;

public class Inventory : Singleton<Inventory>
{
    private List<Item> items = new List<Item>();
    public IReadOnlyCollection<Item> Items => items.AsReadOnly();

    private InventoryPanel invPanel;

    public void SubscribeInventoryPanel(InventoryPanel invPanel) => this.invPanel = invPanel;

    public bool AddItem(Item item)
    {
        switch (item)
        {
            case IStackable stack:
                List<Item> sameItems = items.FindAll(x => x.GetType() == stack.GetType());
                if (sameItems.Count == 0)
                {
                    items.Add(item);
                    invPanel?.UpdateText(InventoryToString());
                    return true;
                }
                foreach (IStackable stackable in sameItems)
                {
                    stackable.AddToStack(stack.quantity);
                    invPanel?.UpdateText(InventoryToString());
                    return true;
                }
                items.Add(item);
                invPanel?.UpdateText(InventoryToString());
                return true;
            default:
                items.Add(item);
                return true;
                
        }
    }

    public bool RemoveItem(Item item)
    {

        return false;
    }

    public string InventoryToString()
    {
        var output = new System.Text.StringBuilder();
        foreach (Item item in items)
        {
            string itemName = item.name;
            if (item is IStackable stack)
                itemName = stack.quantity.ToString() + "   " + itemName;
            
            output.AppendLine(itemName);
        }

        return output.ToString();
    }
}
