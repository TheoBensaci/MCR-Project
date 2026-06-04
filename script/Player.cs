using Godot;
using System;
using System.Collections.Generic;


public partial class Player : CharacterBody2D
{

    // genral
    [ExportSubgroup("General")]
    [Export]
    public bool isDead { get; set; } = false;

    [Export]
    public int money{get;set;}=0;

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
    public float dashAcc { get; set; } = 1750;

    [Export]
    public double dashTime { get; set; } = 0.25;

    private double _dashTimer =0;

    [Export]
    public double dashCouldown { get; set; } = 0.3;

    private double _dashCouldownTimer =0;



    [ExportSubgroup("Arena")]
    [Export]
    public NodePath arenaManagerPath { get; set; }

    private ArenaManager _arenaManger=null;

    [ExportSubgroup("UI")]
    // player UI
    [Export]
    public NodePath hpBarPath { get; set; }
    private ProgressBar _hpBar;

    [Export]
    public NodePath moneyDisplayPath { get; set; }
    private Label _moneyDisplay;




    [ExportSubgroup("Stats")]
    // state
    [Export]
    public bool onDash { get; set; } = false;

    private Sprite2D _sprite;



    private bool[] _directionTracker = new bool[4]{false,false,false,false};

    private bool _horizontalFacing=false;

    private Vector2 _dirVector = new Vector2(0,0);

    private float _actualVel=0;

    private List<Item> _updatedItems=new List<Item>();


    #region Dash
    public void InitDash(){
        if(onDash || _dashCouldownTimer>0)return;
        UpdateDirVector();
        onDash=true;
        this.Velocity = _dirVector * dashAcc;
        this._dashTimer=dashTime;
    }

    public void EndDash(){
        if(!onDash)return;
        onDash=false;
        this._dashCouldownTimer=dashCouldown;
    }

    #endregion

    #region Eat / Item

    private void UpdateItem(Func<Item,bool> check){
        int count = _updatedItems.Count;
        for (int i = 0; i < count;)
        {
            if(!check(_updatedItems[i])){//_updatedItems[i].UpdateOnEat(this,item,_arenaManger)){
                _updatedItems[i]=_updatedItems[count-1];
                _updatedItems.RemoveAt(count-1);
                count--;
                GD.Print(count);
                continue;
            }
            i++;
        }
    }

    public void Eat(Item item){
        if(item==null){
            GD.Print("SAD :[");
            return;
        }

        item.OnEat(this);

        // update items
        UpdateItem(it=>it.UpdateOnEat(this,item,_arenaManger));

        // add money
        money+=item.GetPrice();

        UpdateMoneyUI();

        // add new items
        _updatedItems.Add(item);
    }

    #endregion



    #region Movement

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

    public bool CanMove() {
        return !onDash;
    }

    #endregion

    #region Death and Spawn

    public void Spawn(){

        isDead=false;

        actualTimeHP=maxTimeHP;

        onDash=false;

        // set animation to idle

        Position = new Vector2(0,0);

        // update UI
        UpdateHpBarUi();
        UpdateMoneyUI();

    }

    public void Damage(float amount){
        actualTimeHP-=amount;
        UpdateHpBarUi();
    }

    public void Kill(){
        isDead=true;
    }

    #endregion

    #region Ui

    public void UpdateHpBarUi(){
        _hpBar.Value= actualTimeHP/maxTimeHP * 100;
    }

    public void UpdateMoneyUI(){
        _moneyDisplay.Text=this.money+" / "+this._arenaManger.targetMoney;
    }


    #endregion



    #region Godot
    public override void _Input(InputEvent @event)
    {

        _directionTracker[0]=_directionTracker[0]?(!@event.IsActionReleased("up")):@event.IsActionPressed("up");
        _directionTracker[1]=_directionTracker[1]?(!@event.IsActionReleased("down")):@event.IsActionPressed("down");
        _directionTracker[2]=_directionTracker[2]?(!@event.IsActionReleased("right")):@event.IsActionPressed("right");
        _directionTracker[3]=_directionTracker[3]?(!@event.IsActionReleased("left")):@event.IsActionPressed("left");


        if (@event.IsActionPressed("eat"))
        {
            InitDash();
        }

        if (@event.IsActionPressed("spawn_hazard"))
        {
            if(_arenaManger!=null){
                _arenaManger.SpawnRandomHazard();
            }
        }
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


        if(GetNode(hpBarPath) is ProgressBar hpbar){
            _hpBar=hpbar;
        }
        else{
            GD.PushError("hp bar path is missing or invalide");
        }

        if(GetNode(moneyDisplayPath) is Label moneyLabel){
            _moneyDisplay=moneyLabel;
        }
        else{
            GD.PushError("money display path is missing or invalide");
        }


        Spawn();
    }


    public override void _Process(double delta){
        if(isDead)return;

        if(this.onDash){
            if(this._dashTimer>=0){
                this._dashTimer-=delta;
            }
            else{
                EndDash();
            }
        }
        else if(_dashCouldownTimer>0){
            _dashCouldownTimer-=delta;
        }


        UpdateItem(it=>it.Update(this,_arenaManger,delta));


        if(actualTimeHP>=0){
            actualTimeHP-=delta * lossingHpRate;
            UpdateHpBarUi();
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
        p.X=onDash?32:0;
        rect2.Position=p;
        _sprite.RegionRect=rect2;


        MoveAndSlide();

        // respect arena rediuse
        double magn = Position.Length();
        Position = Position.Normalized() * (float)Math.Min(magn,_arenaManger.arenaRadius);
    }

    #endregion

}
