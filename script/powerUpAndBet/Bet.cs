using System.Collections.Generic;

public abstract class Bet{

    public static Bet[] bets = new Bet[]{
        new BetNDecorator("Get 5 trashbag -> $ x1.25",1.25f,"TrashBag",5),
        new BetNDecorator("Get 5 can -> $ x1.25",1.25f,"Can",5),
        new BetNDecorator("Get 7 paper ball -> $ x2",2,"PaperBall",7),
        new BetNDecorator("Get 10 paper -> $ x3",3,"Paper",10),
        new BetNDecorator("Get 20 trashbag -> $ x5",5,"TrashBag",20),
        new BetNoDamage("Take No damage -> $ x5",5)
    };


    private string _descirption;
    protected float p_bonnus;

    public Bet(string desc, float bonnus){
        _descirption=desc;
        p_bonnus=bonnus;
    }

    public string GetDescritption(){
        return _descirption;
    }

    public abstract bool Check(ArenaManager arenaManager);

    public bool Exec(ArenaManager arenaManager){
        if(Check(arenaManager)){
            arenaManager.playerInstance.money=(int)(p_bonnus*arenaManager.playerInstance.money);
            return true;
        }
        arenaManager.playerInstance.money=(int)(arenaManager.playerInstance.money/p_bonnus);
        return false;
    }


}