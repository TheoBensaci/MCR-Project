/**
 *   Autheur: Theo Bensaci
 *   Date: 00:04 11.06.2026
 *   Description: Bet on the Number of decorator T eated by run
 */




using Godot;

public class BetNDecorator : Bet
{
    private string _name;
    private int _amount;
    public BetNDecorator(string desc, float bonnus,string name,int amount) : base(desc, bonnus)
    {
        _name=name;
        _amount=amount;
    }

    public override bool Check(ArenaManager arenaManager)
    {
        GD.Print("Check : "+arenaManager.runResume.GetDecoratorCount(_name));
        return arenaManager.runResume.GetDecoratorCount(_name)>=_amount;
    }
}