using Godot;
using System;

public partial class UiAction : Node
{
    public virtual void Action(){
        GD.Print("a");
    }

    public virtual void EndAction(){
        GD.Print("b");
    }
}
