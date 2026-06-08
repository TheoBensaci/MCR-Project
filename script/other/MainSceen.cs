using Godot;
using System;

public partial class MainSceen : Node2D
{

    [Export]
    public ShopKeeper shopKeeper;

    [ExportSubgroup("Camera")]
    [Export]
    public Node2D cameraTarget;
    [Export]
    public Godot.Collections.Dictionary<string, CameraPlacement> CameraPlacement = new Godot.Collections.Dictionary<string, CameraPlacement>();

    private int _placementIndex=0;


    public void ChangeCamera(string targetName){
        if(CameraPlacement.ContainsKey(targetName)){
            cameraTarget.Position=CameraPlacement[targetName].cameraPos;
            shopKeeper.targetPos=CameraPlacement[targetName].shopKeeperTargetPos;
            shopKeeper.ChangeState(CameraPlacement[targetName].shopKeeperState);
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
    }



    public override void _Ready(){
        ChangeCamera("Arena");
    }

}
