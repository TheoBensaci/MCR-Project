/**
 *   Autheur: Theo Bensaci
 *   Date: 23:42 10.06.2026
 *   Description: ui action for the shop screen
 */

using Godot;
using System;

public partial class UiActionShop : UiAction
{
    [Export]
    public Label money;
    [Export]
    public Card itemA;
    [Export]
    public Card itemB;
    [Export]
    public Card nextCard;

    private int[] _prices;

    [Export]
    public MainSceen main;
    public override void Action(){
        nextCard.SetValue("Next");
        nextCard.setActive(true);
        nextCard.action=()=>{
            main.ChangeCamera("Bet");
        };

        // gen shop
        _prices=new int[2];
        _prices[0]=SetShopCard(itemA,main.arena.playerInstance.money);
        _prices[1]=SetShopCard(itemB,main.arena.playerInstance.money);

        update();
    }

    /// <summary>
    /// set a shp card
    /// </summary>
    /// <param name="card">card</param>
    /// <param name="money">actual money of the player</param>
    /// <returns></returns>
    private int SetShopCard(Card card, int money){
        PowerUp powerUp = PowerUp.powerups[GD.Randi()%PowerUp.powerups.Length];
        card.SetValue(powerUp.GetDescritption(),powerUp.GetCost().ToString("D4")+"$",()=>{
            powerUp.Exec(main.arena);
        }).setActive(money>powerUp.GetCost());
        return powerUp.GetCost();
    }

    /// <summary>
    /// update card
    /// </summary>
    private void update(){
        money.Text=main.arena.playerInstance.money.ToString("D4")+"$";
        itemA.setActive(main.arena.playerInstance.money>_prices[0]);
        itemB.setActive(main.arena.playerInstance.money>_prices[1]);
    }

    public override void EndAction(){
    }

}
