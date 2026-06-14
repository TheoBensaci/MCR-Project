/**
 *   Autheur: Theo Bensaci
 *   Date: 20:40 10.06.2026
 *   Description: ui screen
 */

using Godot;
using System;

public partial class UiScreen : Control
{
    [Export]
    public AnimationPlayer player;


    public void ShowScreen(){
        player.Play("show");
        for (int i = 0; i < GetChildCount(); i++)
        {
            if(GetChild(i) is UiAction action){
                action.Action();
            }
        }
    }

    public void _on_animation_player_animation_finished(StringName animationName){
        if(animationName=="show"){
            for (int i = 0; i < GetChildCount(); i++)
            {
                if(GetChild(i) is UiAction action){
                    action.EndAction();
                }
            }
        }
    }

    public void HideScreen(){
        player.Play("hide");
    }
}
