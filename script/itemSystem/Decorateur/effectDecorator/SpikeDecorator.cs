using Godot;

public class SpikeDecorator : AbstractItemDecorator
{
    private float _amount;
    public SpikeDecorator(Item baseItem,float amount) : base(baseItem)
    {
        _amount=amount;
    }

    public override void OnEat(Player pl)
    {
        pl.Damage(_amount,DamageType.spike);
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 40;
    }
}