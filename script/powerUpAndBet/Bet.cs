/**
 *   Autheur: Theo Bensaci
 *   Date: 00:02 11.06.2026
 *   Description: Manage bets
 */

using System.Collections.Generic;

public abstract class Bet{

    // list of bet precreated
    public static Bet[] bets = new Bet[]{
        new BetNItemType("Get 5 trashbag -> $ x1.25",1.25f,ItemType.trashbag,5),
        new BetNItemType("Get 5 can -> $ x1.25",1.25f,ItemType.can,5),
        new BetNItemType("Get 7 paper ball -> $ x2",2,ItemType.paperBall,7),
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

    /// <summary>
    /// Check if a been repected
    /// </summary>
    /// <param name="arenaManager"></param>
    /// <returns></returns>
    public abstract bool Check(ArenaManager arenaManager);

    /// <summary>
    /// Execute bet, if ok multiply the money by N, if not devide it by N
    /// </summary>
    /// <param name="arenaManager">arena</param>
    /// <returns></returns>
    public bool Exec(ArenaManager arenaManager){
        if(Check(arenaManager)){
            arenaManager.playerInstance.money=(int)(p_bonnus*arenaManager.playerInstance.money);
            return true;
        }
        arenaManager.playerInstance.money=(int)(arenaManager.playerInstance.money/p_bonnus);
        return false;
    }


}