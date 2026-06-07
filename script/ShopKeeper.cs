using Godot;
using System;

public partial class ShopKeeper : Node2D
{

    public enum State{
        happy,
        eye,
        sad
    }

    private AnimationPlayer _animation;

    [Export]
    public Vector2 targetPos { get; set; }

    [Export]
    public float targetPosOffsetRadiuse {get;set;} = 100;

    private double offsetT=0;


[   Export]
    public double movementSpeed {get;set;} = 100;

    [Export]
    public float destinationRadiuse {get;set;} = 500;


    [Export]
    public Node2D defaultMouth { get; set; }


    [Export]
    public Node2D eyeMouth { get; set; }


    [Export]
    public Node2D eye { get; set; }

    [Export]
    public Vector2 eyeMovementRadiuse {get;set;}

    private State _state=State.happy;
    private State _nextState=State.happy;


    public override void _Ready(){

        _animation=GetNode<AnimationPlayer>("AnimationPlayer");

        _animation.Play("Idle");
    }

    private void ApplyChangeState(State newState){
        _state=newState;
        bool eyeMouthActive=false;
        if(newState==State.sad){
            _animation.Play("GameOver");
        }
        else{
            eyeMouthActive = newState==State.eye;
            _animation.Play("ChangeFace");
        }

        defaultMouth.Visible=!eyeMouthActive;
        eyeMouth.Visible=eyeMouthActive;

    }


    public void ChangeState(State newState){
        _nextState=newState;
    }



    public void _on_animation_player_animation_finished(StringName animationName){
        if(animationName=="ChangeFace"){
            _animation.Play("Idle");
        }
    }


    public override void _Process(double delta){

    }


    public override void _PhysicsProcess(double delta)
    {
        // movement
        offsetT = (offsetT+delta) % (2*Math.PI);
        Vector2 offset = new Vector2((float)Math.Cos(offsetT),(float)Math.Sin(offsetT));


        offset = offset * targetPosOffsetRadiuse;

        Vector2 realTargetPos = targetPos + offset;

        Position = Position.Lerp(realTargetPos,(float)(delta*movementSpeed));

        Vector2 diff = realTargetPos - Position;

        if(_nextState!=_state && diff.LengthSquared()<=(destinationRadiuse*destinationRadiuse)){
            ApplyChangeState(_nextState);
        }

        if(_state==State.eye){
            Vector2 eyeDiff = GetViewport().GetCamera2D().GetGlobalMousePosition()-Position;
            eye.Position = eyeDiff.Normalized()*eyeMovementRadiuse;
        }
    }

}
