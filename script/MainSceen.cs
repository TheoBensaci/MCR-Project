using Godot;
using System;


public partial class MainSceen : Node2D
{
    private Node2D _cameraTarget;
    [ExportSubgroup("Camera")]
    [Export]
    public NodePath CameraTargetPath;
    [Export]
    public Godot.Collections.Dictionary<string, Vector2> CameraPositions = new Godot.Collections.Dictionary<string, Vector2>();


    public void ChangeCamera(string targetName){
        if(CameraPositions.ContainsKey(targetName)){
            _cameraTarget.Position=CameraPositions[targetName];
        }
    }

    public override void _Input(InputEvent @event)
    {

        if(@event.IsActionReleased("t1")){
            ChangeCamera("Arena");
        }

        if(@event.IsActionReleased("t2")){
            ChangeCamera("Shop");
        }
    }



    public override void _Ready(){
        if(GetNode(CameraTargetPath) is Node2D camTarget){
            _cameraTarget=camTarget;
        }
        else{
            GD.PushError("Camera target path is missing or invalide");
        }
    }

}
