


using Godot;

public class BetNoDamage : Bet
{
    public BetNoDamage(string desc, float bonnus) : base(desc, bonnus)
    {
    }

    public override bool Check(ArenaManager arenaManager)
    {
        GD.Print("test : " + arenaManager.runResume.getHit());
        return arenaManager.runResume.getHit()==0;
    }
}