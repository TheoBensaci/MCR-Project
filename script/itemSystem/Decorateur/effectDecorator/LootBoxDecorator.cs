using Godot;

public class LootBoxDecorator : AbstractItemDecorator
{
    private int _amount;
    public LootBoxDecorator(Item baseItem, int amount) : base(baseItem)
    {
        _amount=amount;
    }

    public override void OnEat(Player pl)
    {
        ArenaManager arenaManager = pl.arenaManager;
        for (int i = 0; i < _amount; i++)
        {
            // we use CallDeferred to prevent flushing queries due to OnEat function call timing (during collision)
            arenaManager.CallDeferred(ArenaManager.MethodName.SpawnItem,
                arenaManager.GetRandomPos(
                        pl.Position,
                        300,
                        300
                    ),
                    new string[1]{"LootBox"}    // ban lootbox decorator from being added to all item create with a loot box to prevent loop hole
            );
        }
        base.OnEat(pl);
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }
}