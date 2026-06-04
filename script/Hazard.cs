using Godot;
using System;

public partial class Hazard : Node2D
{
    [ExportSubgroup("Timing")]
    [Export]
    public double warningTime = 2.0;
    [Export]
    public double delayTime = 0.5;
    [Export]
    public double activeTime = 2.0;

    [ExportSubgroup("Visual")]
    [Export]
    public Color warningColor = new Color(0, 0.8f, 1, 0.7f);
    [Export]
    public Color activeColor = new Color(1, 0, 0, 1);

    [ExportSubgroup("References")]
    [Export]
    public NodePath warningShapePath;
    [Export]
    public NodePath activeShapePath;
    [Export]
    public NodePath damageZonePath;

    private double _timer = 0;
    private HazardState _state = HazardState.Warning;

    private enum HazardState { Warning, Delay, Active, Done }

    private CanvasItem _warningShape;
    private CanvasItem _activeShape;
    private Area2D _damageZone;

    public override void _Ready()
    {
        _warningShape = GetNode<CanvasItem>(warningShapePath);
        _activeShape = GetNode<CanvasItem>(activeShapePath);
        _damageZone = GetNode<Area2D>(damageZonePath);

        _warningShape.Modulate = warningColor;
        _activeShape.Modulate = activeColor;
        _warningShape.Visible = true;
        _activeShape.Visible = false;
        _damageZone.Monitoring = false;
        _damageZone.Monitorable = false;

        _timer = warningTime;
        
        GD.Print("[Hazard] Spawned! type=", Name, " pos=", Position, " rot=", Rotation);
    }

    public override void _Process(double delta)
    {
        switch (_state)
        {
            case HazardState.Warning:
                _timer -= delta;
                float flash = (float)(Mathf.Abs(Mathf.Sin(Time.GetTicksMsec() / 150.0)) * 0.5 + 0.5);
                _warningShape.Modulate = new Color(warningColor.R, warningColor.G, warningColor.B, warningColor.A * flash);

                if (_timer <= 0)
                {
                    GD.Print("[Hazard] WARNING -> DELAY");
                    _warningShape.Visible = false;
                    _state = HazardState.Delay;
                    _timer = delayTime;
                }
                break;

            case HazardState.Delay:
                _timer -= delta;
                if (_timer <= 0)
                {
                    GD.Print("[Hazard] DELAY -> ACTIVE");
                    _activeShape.Visible = true;
                    _damageZone.Monitoring = true;
                    _damageZone.Monitorable = true;
                    
                    foreach (Node2D body in _damageZone.GetOverlappingBodies())
                    {
                        _on_damage_zone_body_entered(body);
                    }
                    
                    _state = HazardState.Active;
                    _timer = activeTime;
                }
                break;

            case HazardState.Active:
                _timer -= delta;
                if (_timer <= 0)
                {
                    GD.Print("[Hazard] ACTIVE -> DONE (expiring)");
                    _damageZone.Monitoring = false;
                    _damageZone.Monitorable = false;
                    _activeShape.Visible = false;
                    _state = HazardState.Done;
                }
                break;

            case HazardState.Done:
                GD.Print("[Hazard] QueueFree() name=", Name);
                QueueFree();
                break;
        }
    }

    private void _on_damage_zone_body_entered(Node2D body)
    {
        GD.Print("[Hazard] Body entered! body=", body.Name, " bodyType=", body.GetType().Name);
        if (body is Player player)
        {
            GD.Print("[Hazard] ITS A PLAYER! Applying damage=5");
            player.Damage(5.0f);
        }
        else
        {
            GD.Print("[Hazard] Not a player, ignoring.");
        }
    }
}
