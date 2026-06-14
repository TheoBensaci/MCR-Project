/**
 *   Autheur: Theo Bensaci
 *   Date: 23:23 08.06.2026
 *   Description: use to set camera placement, shopkeeper placement and shopkeeper state
 */

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