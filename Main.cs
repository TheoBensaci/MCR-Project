using Godot;
using System;

public partial class Main : Node
{
    [Export]
    public PackedScene mainSceen;

    private static Main _instance=null;



    public static void RequestStart(){
        if(_instance==null)return;
        _instance.Start();
    }

    private void Start(){
        AddChild(mainSceen.Instantiate());
    }

    public override void _Ready(){
        _instance=this;
        Start();
    }
}
