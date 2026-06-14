

using System;
using System.Collections.Generic;
using Godot;

public class BaseItem : Item
{

    private ItemType _type;

    public BaseItem(ItemType type){
        _type=type;
    }

    public ItemRenderInfo GetRenderInfo(){
        return new ItemRenderInfo();
    }


    public List<string> GetDecoratorsLists(){
        return new List<string>(){this.GetType().Name};
    }


    public int GetPrice()
    {
        return 10;
    }

    public void OnEat(Player pl)
    {

    }

    public bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        return false;
    }

    public bool Update(Player player, ArenaManager arena, double delta_t){
        return false;
    }

    public ItemType GetItemType()
    {
        return _type;
    }
}