using GardeningGame.Items;
using System;
using System.Collections.Generic;
using System.Text;

public class Inventory : Singleton<Inventory>
{
    private List<Item> items = new List<Item>();
    public IReadOnlyCollection<Item> Items => items.AsReadOnly();

    private InventoryPanel invPanel;

    public void SubscribeInventoryPanel(InventoryPanel invPanel)
    {
        this.invPanel = invPanel;
        this.invPanel?.UpdateText(InventoryToString());
    }

    public bool AddItem(Item item)
    {
        switch (item)
        {
            case IStackable stackable:
                List<Item> invItems = items.FindAll(x => x.GetType() == stackable.GetType());
                foreach (IStackable invItem in invItems)
                {
                    if (invItem.IsFull) continue;

                    int overflow = stackable.quantity - invItem.RemainingValue;
                    
                    if (overflow <= 0)
                    {
                        invItem.AddToStack(stackable.quantity);
                        invPanel.UpdateText(InventoryToString());
                        return true;
                    }
                    else
                    {
                        invItem.Fill();
                        stackable.RemoveFromStack(invItem.RemainingValue);
                        AddItem(item);
                        invPanel.UpdateText(InventoryToString());

                        return true;
                    }
                }
                items.Add(item);
                invPanel.UpdateText(InventoryToString());

                return true;
            default:
                return true;
        }
    }

    public bool RemoveItem(Item item)
    {
        switch (item)
        {
            case IStackable stackable:
                List<Item> invItems = items.FindAll(x => x.GetType() == stackable.GetType());
                int invTotal = 0;

                foreach (IStackable invItem in invItems)
                {
                    invTotal += invItem.quantity;
                }

                int remaining = invTotal - stackable.quantity;

                if (remaining < 0) return false;

                else if(remaining == 0)
                {
                    foreach (IStackable invItem in invItems)
                    {
                        items.RemoveAll(x => x.GetType() == stackable.GetType());
                    }
                    invPanel.UpdateText(InventoryToString());
                    return true;
                }
                else if (remaining > 0)
                {
                    invItems.Reverse();

                    foreach (IStackable invItem in invItems)
                    {
                        int tempVal = invItem.quantity - stackable.quantity;

                        if (tempVal <= 0)
                        {
                            items.Remove(invItem as Item);

                            if(tempVal < 0)
                            {
                                stackable.RemoveFromStack(-tempVal);
                                RemoveItem(stackable as Item);
                            }
                            return true;
                        }
                        else if (invItem.quantity - stackable.quantity > 0)
                        {
                            invItem.RemoveFromStack(stackable.quantity);                            
                        }

                    }
                    invPanel.UpdateText(InventoryToString());
                    return true;
                }
                return false;
            default:
                return false;
        }

    }

    public IReadOnlyCollection<Item> FindAllItems(Type type) => items.FindAll(x => x.GetType() == type).AsReadOnly();

    public string InventoryToString()
    {
        StringBuilder sb = new();
        foreach (Item item in items)
        {
            switch (item)
            {
                case IStackable stackable:
                    sb.AppendLine(stackable.quantity + " " + item.ToString());
                    break;
                default:
                    sb.AppendLine(item.ToString());
                    break;
            }            
        }
        return sb.ToString();
    }
}
