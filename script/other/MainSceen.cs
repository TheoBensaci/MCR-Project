using Godot;
using System;

public partial class MainSceen : Node2D
{

    [Export]
    public ShopKeeper shopKeeper;

    [Export]
    public ArenaManager arena;

    [Export]
    public UiManager uiManager;

    [ExportSubgroup("Camera")]
    [Export]
    public Node2D cameraTarget;
    [Export]
    public Godot.Collections.Dictionary<string, CameraPlacement> CameraPlacement = new Godot.Collections.Dictionary<string, CameraPlacement>();

    private int _placementIndex=0;


    public void ChangeCamera(string targetName){
        if(CameraPlacement.ContainsKey(targetName)){
            cameraTarget.Position=CameraPlacement[targetName].cameraPos;
            shopKeeper.SetPlacement(CameraPlacement[targetName].shopKeeperTargetPos,targetName);
            shopKeeper.ChangeState(CameraPlacement[targetName].shopKeeperState);
        }
    }

    public override void _Input(InputEvent @event)
    {

        if(@event.IsActionReleased("t1")){
            Main.RequestStart();
        }
    }



    public override void _Ready(){
        ChangeCamera("Arena");
        arena.main=this;
        shopKeeper.uiManager=uiManager;

        arena.StartArena();
    }

}
