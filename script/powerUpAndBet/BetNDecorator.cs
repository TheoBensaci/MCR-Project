


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
        GD.Print("Check : "+arenaManager.runResume.getItemCount(_name));
        return arenaManager.runResume.getItemCount(_name)>=_amount;
    }
}