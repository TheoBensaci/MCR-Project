/**
 *   Autheur: Theo Bensaci
 *   Date: 00:56 11.06.2026
 *   Description: manage camera shake
 */

using Godot;
using System;

public partial class CameraShake : Camera2D
{

    [Export]
    public float recoverSpeed;

    private float _actualShake = 0;



    public void SetShake(float strength){
        _actualShake=strength;
    }


    private Vector2 GetRandomOffset(){
        return new Vector2((float)GD.RandRange(-_actualShake,_actualShake),(float)GD.RandRange(-_actualShake,_actualShake));
    }


    public override void _Process(double delta){

        _actualShake = MathUtils.approach(_actualShake,0,(float)(delta*recoverSpeed));
        if(_actualShake>0){
            GD.Print(_actualShake);
        }

        Offset=GetRandomOffset();

    }
}
