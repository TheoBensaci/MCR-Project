/**
 *   Autheur: Theo Bensaci
 *   Date: 22:25 14.06.2026
 *   Description: ui action for the game over screen
 */

using Godot;
using System;

public partial class UiActionGameOver : UiAction
{
    [Export]
    public Card nextCard;

    public override void Action(){
        GD.Print("next");
        nextCard.SetValue("Next");
        nextCard.setActive(true);
        nextCard.action=()=>{
            Main.RequestStart();
        };
    }

    public override void EndAction(){
    }

}
