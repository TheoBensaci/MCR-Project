using Godot;

[GlobalClass]
public partial class CameraPlacement : Resource {
    [Export]
    public Vector2 cameraPos {get;set;}

    [Export]
    public Vector2 shopKeeperTargetPos {get;set;}

    [Export]
    public ShopKeeper.State shopKeeperState {get;set;}
}