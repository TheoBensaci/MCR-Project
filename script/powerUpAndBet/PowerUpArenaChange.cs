/**
 *   Autheur: Theo Bensaci
 *   Date: 00:18 11.06.2026
 *   Description: add base decorator to every item
 */

using System;

public class PowerBaseDecorator : PowerUp
{
    private string _deco;
    public PowerBaseDecorator(string desc, int cost, string deco) : base(desc, cost)
    {
        _deco=deco;
    }

    public override void Exec(ArenaManager arena){
        base.Exec(arena);
        arena.baseDecorators.Add(_deco);
    }
}