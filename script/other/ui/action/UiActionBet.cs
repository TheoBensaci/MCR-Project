/**
 *   Autheur: Theo Bensaci
 *   Date: 00:38 11.06.2026
 *   Description: ui action for the bet screen
 */

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
            main.arena.PrepareNextRun();
        };

        // gen shop
        Bet bet = Bet.bets[GD.Randi()%Bet.bets.Length];
        card.SetValue(bet.GetDescritption(),"",()=>{
            main.arena.actualBet=bet;
            main.arena.PrepareNextRun();
        }).setActive(true);
    }

    public override void EndAction(){

    }

}
