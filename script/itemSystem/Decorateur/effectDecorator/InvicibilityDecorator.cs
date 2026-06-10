using Godot;

public class InvicibilityDecorator : AbstractItemDecorator
{
    protected double p_amount;
    public InvicibilityDecorator(Item baseItem,double amount) : base(baseItem)
    {
        p_amount=amount;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().AddIcon("Invicibility");
    }

    public override void OnEat(Player pl)
    {
        pl.SetInvicibility(p_amount);
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }
}