

using System.Formats.Asn1;

public abstract class AbstractItemDecorator : Item
{

    protected Item p_baseItem;

    public AbstractItemDecorator(Item baseItem){
        p_baseItem=baseItem;
    }

    public virtual ItemGroup GetGroup()
    {
        return p_baseItem.GetGroup();
    }

    public virtual int GetPrice()
    {
        return p_baseItem.GetPrice();
    }

    public virtual bool OnEat(Player pl)
    {
        return p_baseItem.OnEat(pl);
    }
}