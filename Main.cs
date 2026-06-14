/**
 *   Autheur: Theo Bensaci
 *   Date: 17:03 10.06.2026
 *   Description: Root
 */

using Godot;
using System;

public partial class Main : Node
{
    [Export]
    public PackedScene mainSceen;

    private static Main _instance=null;

    private MainSceen _main=null;


    public static void RequestStart(){
        if(_instance==null)return;
        _instance.Start();
    }

    private void Start(){
        if(_main!=null)RemoveChild(_main);
        _main =(MainSceen) mainSceen.Instantiate();
        AddChild(_main);
    }

    public override void _Ready(){
        _instance=this;
        Start();
    }
}
