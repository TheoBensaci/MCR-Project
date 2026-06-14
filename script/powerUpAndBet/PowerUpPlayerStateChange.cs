/**
 *   Autheur: Theo Bensaci
 *   Date: 00:11 11.06.2026
 */

using System;

public class PowerUpHpUp : PowerUp
{
    private float _amount;
    public PowerUpHpUp(string desc, int cost, float amount) : base(desc, cost)
    {
        _amount=amount;
    }

    public override void Exec(ArenaManager arena){
        base.Exec(arena);
        arena.playerInstance.maxTimeHP*=_amount;
    }
}
public class PowerBlockHit : PowerUp
{
    private int _amount;
    public PowerBlockHit(string desc, int cost, int amount) : base(desc, cost)
    {
        this._amount=amount;
    }

    public override void Exec(ArenaManager arena){
        base.Exec(arena);
        arena.playerInstance.armor+=_amount;
    }
}