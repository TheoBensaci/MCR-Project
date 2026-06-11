using System;

public abstract class PowerUp{

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

    public virtual void Exec(ArenaManager arena){
        arena.playerInstance.money-=_cost;
    }
}