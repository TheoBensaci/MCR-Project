using Godot;

public class SpeedDecorator : AbstractItemDecorator
{
    protected int p_amount;
    public SpeedDecorator(Item baseItem,int amount) : base(baseItem)
    {
        p_amount=amount;
    }

    public override void OnEat(Player pl)
    {
        pl.actualMovementSpeed+=p_amount;
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }
}