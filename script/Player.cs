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

    [ExportSubgroup("Animation")]
    [Export]
    public AnimationPlayer animPlayer { get; set; }

    [Export]
    public Sprite2D sprite1 { get; set; }

    [Export]
    public Sprite2D sprite2 { get; set; }

    private int dashAnimationState=0;

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
    public ArenaManager arenaManager { get; set; }

    [ExportSubgroup("UI")]
    // player UI
    [Export]
    public ProgressBar hpBar { get; set; }

    [Export]
    public Label moneyDisplay { get; set; }




    [ExportSubgroup("Stats")]
    // state
    [Export]
    public bool onDash { get; set; } = false;



    private bool[] _directionTracker = new bool[4]{false,false,false,false};

    private bool _horizontalFacing=false;

    private Vector2 _dirVector = new Vector2(0,0);

    private float _actualVel=0;

    private List<Item> _updatedItems=new List<Item>();


    #region Dash
    public void InitDash(){
        if(onDash || _dashCouldownTimer>0 || (_dirVector.X==0 && _dirVector.Y==0))return;
        UpdateDirVector();
        onDash=true;
        this.Velocity = _dirVector * dashAcc;
        this._dashTimer=dashTime;

        animPlayer.Play("initDash");
    }

    public void EndDash(){
        if(!onDash)return;
        dashAnimationState=1;
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
        UpdateItem(it=>it.UpdateOnEat(this,item,arenaManager));

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

        animPlayer.Play("idle");

        isDead=false;

        actualTimeHP=maxTimeHP;

        onDash=false;

        // set animation to idle

        Position = new Vector2(0,0);

        Velocity=new Vector2(0,0);
        money=0;

        // update UI
        UpdateHpBarUi();
        UpdateMoneyUI();

    }

    public void Damage(float amount){
        actualTimeHP-=amount;
        UpdateHpBarUi();
    }

    public void Kill(){
        animPlayer.Play("death");
        isDead=true;
    }

    #endregion

    #region Ui

    public void UpdateHpBarUi(){
        hpBar.Value= actualTimeHP/maxTimeHP * 100;
    }

    public void UpdateMoneyUI(){
        moneyDisplay.Text=this.money+" / "+this.arenaManager.targetMoney;
    }


    #endregion


    public void _on_animation_player_animation_finished(StringName animationName){
        if(animationName=="death"){
            arenaManager.EndArena();
        }
        switch(dashAnimationState){
            case 1:
                if(animationName=="initDash"){
                    dashAnimationState=2;
                    animPlayer.Play("endDash");
                }
            break;
            case 2:
                if(animationName=="endDash"){
                    dashAnimationState=0;
                }
            break;
        }
    }



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
            if(arenaManager!=null){
                arenaManager.SpawnRandomHazard();
            }
        }
    }


    public override void _Ready(){
        arenaManager.playerInstance=this;

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

        if(onDash){

        }
        else if(dashAnimationState==0){
            if(_dirVector.X!=0 || _dirVector.Y!=0){
                animPlayer.Play("move");
            }
            else{
                animPlayer.Play("idle");
            }
        }


        UpdateItem(it=>it.Update(this,arenaManager,delta));


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
            sprite1.FlipH=sprite2.FlipH=!_horizontalFacing;

            // update velocity
            Velocity=MathUtils.approachVector(Velocity,_dirVector * moveSpeed,(float)(delta* moveAcc * 10));
        }

        // TODO : real eat animation


        MoveAndSlide();

        // respect arena rediuse
        double magn = Position.Length();
        Position = Position.Normalized() * (float)Math.Min(magn,arenaManager.arenaRadius);
    }

    #endregion

}
