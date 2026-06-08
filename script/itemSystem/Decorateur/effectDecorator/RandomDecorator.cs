using System.Collections.Generic;
using Godot;

public class RandomDecorator : AbstractItemDecorator
{
    public RandomDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override List<string> GetDecoratorsLists(){
        List<string> r = p_baseItem.GetDecoratorsLists();
        r[^1]=GetType().Name;
        return r;
    }
}