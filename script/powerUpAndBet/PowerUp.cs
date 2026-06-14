/**
 *   Autheur: Theo Bensaci
 *   Date: 15:11 10.06.2026
 *   Description: power up
 */

using System;

public abstract class PowerUp{

    // list of precreated power up
    public static PowerUp[] powerups = new PowerUp[]{
        new PowerUpHpUp("+ 5% hp",600,1.05f),
        new PowerBlockHit("block +1 damage",1000,1),
        new PowerBaseDecorator("make every item have the Heal modifer",5000,"Speed"),
        new PowerBaseDecorator("make every item have the Acceleration modifer",1000,"Acceleration"),
        new PowerBaseDecorator("make every item have the LootBox modifer",10000,"LootBox")
    };
    private string _descirption;
    private int _cost;

    public PowerUp(string desc, int cost){
        _descirption=desc;
        _cost=cost;
    }


    public string GetDescritption(){
        return _descirption;
    }

    public int GetCost(){
        return _cost;
    }

    /// <summary>
    /// Execute power up and apply the cost to the player money
    /// </summary>
    /// <param name="arena"></param>
    public virtual void Exec(ArenaManager arena){
        arena.playerInstance.money-=_cost;
    }
}