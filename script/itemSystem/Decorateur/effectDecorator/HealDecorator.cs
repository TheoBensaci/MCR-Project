using Godot;

public class HealDecorator : AbstractItemDecorator
{
    protected float p_amount;
    public HealDecorator(Item baseItem,float amount) : base(baseItem)
    {
        p_amount=amount;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().AddIcon("Heal");
    }

    public override void OnEat(Player pl)
    {
        pl.Heal(p_amount);
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }
}