

using Godot;

public class BaseItem : Item
{
    public ItemGroup GetGroup()
    {
        return ItemGroup.none;
    }

    public int GetPrice()
    {
        return 0;
    }

    public bool OnEat(Player pl)
    {
        GD.Print("nom nom "+pl.Name);
        return true;
    }
}