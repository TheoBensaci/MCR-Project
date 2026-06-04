

using Godot;

public class TestItemDeco : AbstractItemDecorator
{
    public TestItemDeco(Item baseItem) : base(baseItem)
    {
    }

    public override void OnEat(Player pl){
        GD.Print("We test");
        base.OnEat(pl);
    }
}