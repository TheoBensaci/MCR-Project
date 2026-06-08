using Godot;

public class SpeedyDecorator : AbstractItemDecorator
{
    protected int p_amount;
    public SpeedyDecorator(Item baseItem,int amount) : base(baseItem)
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