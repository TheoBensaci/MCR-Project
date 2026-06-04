using Godot;
using System;

public partial class MainSceen : Node2D
{
    private Node2D _cameraTarget;

    [Export]
    public NodePath ShopkeeperPath;
    private ShopKeeper _shopKeeper;

    [ExportSubgroup("Camera")]
    [Export]
    public NodePath CameraTargetPath;
    [Export]
    public Godot.Collections.Dictionary<string, CameraPlacement> CameraPlacement = new Godot.Collections.Dictionary<string, CameraPlacement>();

    private int _placementIndex=0;


    public void ChangeCamera(string targetName){
        if(CameraPlacement.ContainsKey(targetName)){
            _cameraTarget.Position=CameraPlacement[targetName].cameraPos;
            _shopKeeper.targetPos=CameraPlacement[targetName].shopKeeperTargetPos;
            _shopKeeper.ChangeState(CameraPlacement[targetName].shopKeeperState);
        }
    }

    public override void _Input(InputEvent @event)
    {

        if(@event.IsActionReleased("t1")){
            _placementIndex = (_placementIndex+1)%CameraPlacement.Count;

            int i =0;
            foreach (string item in CameraPlacement.Keys)
            {
                if(i==_placementIndex){
                    ChangeCamera(item);
                    GD.Print(item);
                    break;
                }
                i++;
            }
            //ChangeCamera(CameraPlacement[CameraPlacement.Keys[0]]);
        }

        if(@event.IsActionReleased("t2")){
            int i =0;
            foreach (string item in CameraPlacement.Keys)
            {
                if(i==_placementIndex){
                    ChangeCamera(item);
                    GD.Print(item);
                    break;
                }
                i++;
            }
            //ChangeCamera(CameraPlacement[CameraPlacement.Keys[0]]);
        }
    }



    public override void _Ready(){
        if(GetNode(CameraTargetPath) is Node2D camTarget){
            _cameraTarget=camTarget;
        }
        else{
            GD.PushError("Camera target path is missing or invalide");
        }

        if(GetNode(ShopkeeperPath) is ShopKeeper shopKeeper){
            _shopKeeper=shopKeeper;
        }
        else{
            GD.PushError("Shop keepre path is missing or invalide");
        }
    }

}
