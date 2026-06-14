/**
 *   Autheur: Theo Bensaci
 *   Date: 00:09 11.06.2026
 *   Description: Bet on the no hit run
 */




using Godot;

public class BetNoDamage : Bet
{
    public BetNoDamage(string desc, float bonnus) : base(desc, bonnus)
    {
    }

    public override bool Check(ArenaManager arenaManager)
    {
        GD.Print("test : " + arenaManager.runResume.GetHit());
        return arenaManager.runResume.GetHit()==0;
    }
}