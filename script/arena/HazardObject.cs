using Godot;
using System;

public partial class HazardObject : Node2D
{

    [ExportSubgroup("Visual")]
    [Export]
    public Node2D warningIcon;
    [Export]
    public AnimationPlayer player;
    [Export]
    public string animationLibrary;


    [ExportSubgroup("Behavior")]
    [Export]
    public double initTime;
    private double _timer=0;

    [Export]
    public DamageType damageType;
    [Export]
    public float damageAmount;

    private bool _active = false;

    public override void _Ready(){
    }

    public void Start(){
        _timer=0;
        player.Play(animationLibrary+"/init");

        // random rotation
        float amount = GD.Randf()*(float)Math.PI*2f;
        warningIcon.Rotate(-amount);
        Rotate(amount);
    }

    public override void _Process(double delta)
    {
        if(_timer>=0){
            if(_timer<=initTime){
                _timer+=delta;
            }
            else{
                _timer=-1;
                player.Play(animationLibrary+"/damage");
                _active=true;
            }
        }
    }

    public void _on_animation_player_animation_finished(StringName animationName){
        if(animationName==animationLibrary+"/damage"){
            QueueFree();
        }
    }

    private void _on_damage_zone_body_entered(Node2D body)
    {
        if(_active && body is Player player){
            player.Damage(damageAmount,damageType);
            _active=false;
        }
    }
}
