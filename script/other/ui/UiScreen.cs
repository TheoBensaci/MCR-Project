using Godot;
using System;

public partial class UiScreen : Control
{
    [Export]
    public AnimationPlayer player;


    public void ShowScreen(){
        GD.Print("AA");
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
