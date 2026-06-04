using Godot;
using System;

public partial class Player : CharacterBody2D
{

    // genral
    [ExportSubgroup("General")]
    [Export]
    public bool isDead { get; set; } = false;

    [ExportSubgroup("HP Systeme")]
    [Export]
    public double actualTimeHP { get; set; } = 30;      // actual time remainging in sec
    [Export]
    public double maxTimeHP { get; set; } = 30;         // max time in sec

    [Export]
    public double lossingHpRate { get; set; } = 1;         // losing hp rate

    [ExportSubgroup("Movement")]
    [Export]
    public float moveSpeed { get; set; } = 500;

    [Export]
    public float moveAcc { get; set; } = 2000;

    [Export]
    public float eatAcc { get; set; } = 2000;

    [Export]
    public double eatModeTime { get; set; } = 0.2;

    private double _eatModeTimer =0;

    [Export]
    public double eatCouldown { get; set; } = 0.2;

    private double _eatCouldownTimer =0;



    [ExportSubgroup("Arena")]
    [Export]
    public NodePath arenaManagerPath { get; set; }

    private ArenaManager _arenaManger=null;

    [ExportSubgroup("UI")]
    // player UI
    [Export]
    public NodePath HpBarPath { get; set; }
    private ProgressBar _hpBar;




    [ExportSubgroup("Stats")]
    // state
    [Export]
    public bool onEat { get; set; } = false;

    private Sprite2D _sprite;



    private bool[] _directionTracker = new bool[4]{false,false,false,false};

    private bool _horizontalFacing=false;

    private Vector2 _dirVector = new Vector2(0,0);

    private float _actualVel=0;


    public void InitEat(){
        if(onEat || _eatCouldownTimer>0)return;
        UpdateDirVector();
        onEat=true;
        this.Velocity = _dirVector * eatAcc;
        this._eatModeTimer=eatModeTime;
    }

    public void EndEat(){
        if(!onEat)return;
        onEat=false;
        this._eatCouldownTimer=eatCouldown;
    }

    public void Eat(Item item){
        if(item==null){
            GD.Print("SAD :[");
            return;
        }
        item.OnEat(this);
    }


    private Vector2 UpdateDirVector(bool avoidZero = false){
        // get target dir
        int x =0;
        int y=0;
        if(_directionTracker[0]){
            ++y;
        }
        if(_directionTracker[1]){
            --y;
        }
        if(_directionTracker[2]){
            ++x;
        }
        if(_directionTracker[3]){
            --x;
        }

        if(avoidZero && x==0 && y==0){
            _dirVector.X=_horizontalFacing?1:-1;
            _dirVector.Y=0;
            return _dirVector;
        }

        _dirVector.X=x;
        _dirVector.Y=y;

        _horizontalFacing=(_dirVector.X==0)?_horizontalFacing:_dirVector.X>0;

        _dirVector=_dirVector.Normalized();


        return _dirVector;
    }


    public void Spawn(){

        isDead=false;

        actualTimeHP=maxTimeHP;

        onEat=false;

        // set animation to idle

        Position = new Vector2(0,0);

    }

    public void Damage(float amount){
        actualTimeHP-=amount;
        UpdateHpBar();
    }

    public void Kill(){
        isDead=true;
    }


    public void UpdateHpBar(){
        _hpBar.Value= actualTimeHP/maxTimeHP * 100;
    }




    public override void _Input(InputEvent @event)
    {

        _directionTracker[0]=_directionTracker[0]?(!@event.IsActionReleased("up")):@event.IsActionPressed("up");
        _directionTracker[1]=_directionTracker[1]?(!@event.IsActionReleased("down")):@event.IsActionPressed("down");
        _directionTracker[2]=_directionTracker[2]?(!@event.IsActionReleased("right")):@event.IsActionPressed("right");
        _directionTracker[3]=_directionTracker[3]?(!@event.IsActionReleased("left")):@event.IsActionPressed("left");


        if (@event.IsActionPressed("eat"))
        {
            InitEat();
        }
    }


    public bool CanMove() {
        return !onEat;
    }

    public override void _Ready(){
        _sprite=GetNode<Sprite2D>("Sprite");

        if(GetNode(arenaManagerPath) is ArenaManager arena){
            _arenaManger=arena;
            _arenaManger.playerInstance=this;
        }
        else{
            GD.PushError("arena manager path is missing or invalide");
        }


        if(GetNode(HpBarPath) is ProgressBar hpbar){
            _hpBar=hpbar;
        }
        else{
            GD.PushError("hp bar path is missing or invalide");
        }


        Spawn();
    }


    public override void _Process(double delta){
        if(isDead)return;

        if(this.onEat){
            if(this._eatModeTimer>=0){
                this._eatModeTimer-=delta;
            }
            else{
                EndEat();
            }
        }
        else if(_eatCouldownTimer>0){
            GD.Print(_eatCouldownTimer);
            _eatCouldownTimer-=delta;
        }


        if(actualTimeHP>=0){
            actualTimeHP-=delta * lossingHpRate;
            UpdateHpBar();
        }
        else{
            Kill();
        }
    }


    public override void _PhysicsProcess(double delta)
    {
        if(isDead)return;

        if(CanMove()){
            UpdateDirVector();
            _sprite.FlipH=!_horizontalFacing;

            // update velocity
            Velocity=MathUtils.approachVector(Velocity,_dirVector * moveSpeed,(float)(delta* moveAcc * 10));
        }

        // TODO : real eat animation
        Rect2 rect2 = _sprite.RegionRect;
        Vector2 p =  rect2.Position;
        p.X=onEat?32:0;
        rect2.Position=p;
        _sprite.RegionRect=rect2;


        MoveAndSlide();

        // respect arena rediuse
        double magn = Position.Length();
        Position = Position.Normalized() * (float)Math.Min(magn,_arenaManger.arenaRadius);
    }

}
