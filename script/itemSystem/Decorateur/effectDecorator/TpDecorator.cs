using Godot;

public class TpDecorator : AbstractItemDecorator
{
    public TpDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override void OnEat(Player pl)
    {
        pl.Position=pl.arenaManager.GetRandomPos();
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }
}