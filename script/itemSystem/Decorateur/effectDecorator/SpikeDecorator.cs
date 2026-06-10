using Godot;

public class SpikeDecorator : AbstractItemDecorator
{
    private float _amount;
    public SpikeDecorator(Item baseItem,float amount) : base(baseItem)
    {
        _amount=amount;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().AddIcon("Spike");
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