

using Godot;

public class TestItemDeco : AbstractItemDecorator
{
    public TestItemDeco(Item baseItem) : base(baseItem)
    {
    }

    public override bool OnEat(Player pl){
        GD.Print("We test");
        return p_baseItem.OnEat(pl);
    }
}