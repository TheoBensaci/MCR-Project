using Godot;
using System;

public partial class UiActionBet : UiAction
{
    [Export]
    public Label money;
    [Export]
    public Card card;
    [Export]
    public Card nextCard;
    [Export]
    public MainSceen main;
    public override void Action(){
        money.Text="Next round -> get "+((int)(main.arena.targetMoney*main.arena.roundMoneyMultiplyer)).ToString("D4")+"$";
        nextCard.setActive(true);
        nextCard.SetValue("Next");
        nextCard.action=()=>{
            main.arena.NextRound();
        };

        // gen shop
        Bet bet = Bet.bets[GD.Randi()%Bet.bets.Length];
        card.SetValue(bet.GetDescritption(),"",()=>{
            main.arena.actualBet=bet;
            main.arena.NextRound();
        }).setActive(true);
    }

    public override void EndAction(){

    }

}
