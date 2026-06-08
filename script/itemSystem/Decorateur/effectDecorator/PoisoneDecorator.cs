using Godot;

public class PoisonDecorator : AbstractItemDecorator
{
    public PoisonDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override void OnEat(Player pl)
    {
        pl.Damage((float)pl.actualTimeHP/2,DamageType.poison);
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 70;
    }
}