

using System;
using System.Collections.Generic;
using System.Formats.Asn1;

public abstract class AbstractItemDecorator : Item
{

    protected Item p_baseItem;

    public AbstractItemDecorator(Item baseItem){
        p_baseItem=baseItem;
    }

    public virtual ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo();
    }


    public virtual List<string> GetDecoratorsLists(){
        List<string> r = p_baseItem.GetDecoratorsLists();
        r.Add(GetType().Name);
        return r;
    }

    public virtual int GetPrice()
    {
        return p_baseItem.GetPrice();
    }

    public virtual void OnEat(Player pl)
    {
        p_baseItem.OnEat(pl);
    }

    public virtual bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        return p_baseItem.UpdateOnEat(player,eatedItem,arena);
    }

    public virtual bool Update(Player player, ArenaManager arena, double delta_t){
        return p_baseItem.Update(player,arena,delta_t);
    }
}