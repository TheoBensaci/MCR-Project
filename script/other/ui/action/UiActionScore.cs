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
            main.arena.actualBet.Exec(main.arena);
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
