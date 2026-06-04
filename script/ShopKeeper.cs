using Godot;
using System;

public partial class ShopKeeper : Node2D
{
    private AnimationPlayer _animation;

    [Export]
    public Vector2 targetPos { get; set; }



    [Export]
    public NodePath defaultMouthPath { get; set; }
    private Node _defaultMouth;


    [Export]
    public NodePath eyeMouthPath { get; set; }
    private Node2D _eyeMouth;


    [Export]
    public NodePath eyePath { get; set; }
    private Node2D _moneyDisplay;

    [Export]
    public float eyeMovementRadiuse {get;set;}

    public bool eyeOut=false;


    public override void _Ready(){

        if(GetNode(eyeMouthPath) is Node2D eyeMouth){
            _eyeMouth=eyeMouth;
        }
        else{
            GD.PushError("eye mouth path is missing or invalide");
        }
    }
}
