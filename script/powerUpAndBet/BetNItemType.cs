/**
 *   Autheur: Theo Bensaci
 *   Date: 20:34 14.06.2026
 *   Description: Bet on the number of type item T eated by run
 */




using Godot;

public class BetNItemType : Bet
{
    private ItemType _type;
    private int _amount;
    public BetNItemType(string desc, float bonnus,ItemType type,int amount) : base(desc, bonnus)
    {
        _type=type;
        _amount=amount;
    }

    public override bool Check(ArenaManager arenaManager)
    {
        return arenaManager.runResume.GetItemCount(_type)>=_amount;
    }
}