

using System;
using System.Collections.Generic;
using Godot;

public class BaseItem : Item
{
    public List<ItemTag> GetTags()
    {
        return new List<ItemTag>();
    }

    public int GetPrice()
    {
        return 0;
    }

    public void OnEat(Player pl)
    {

    }

    public List<string> getIcons(){
        return new List<string>();
    }

    public bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        return false;
    }

    public bool Update(Player player, ArenaManager arena, double delta_t){
        return false;
    }
}