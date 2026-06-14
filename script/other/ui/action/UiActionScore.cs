/**
 *   Autheur: Theo Bensaci
 *   Date: 21:03 10.06.2026
 *   Description: ui action for the score screen
 */

using Godot;
using System;

public partial class UiActionScore : UiAction
{
    [Export]
    public Label endMoney;

    [Export]
    public MainSceen main;

    public override void Action(){
        if(main.arena.actualBet!=null){
            bool result = main.arena.actualBet.Exec(main.arena);
            GD.Print("Bet : "+result);
        }
        endMoney.Text=main.arena.playerInstance.money + "$ / "+main.arena.targetMoney+"$";
    }

    public override void EndAction(){
        if(main.arena.playerInstance.money>main.arena.targetMoney){
            main.arena.playerInstance.money-=main.arena.targetMoney;
            main.ChangeCamera("Shop");
        }
        else{
            main.ChangeCamera("GameOver");
        }
    }

}
