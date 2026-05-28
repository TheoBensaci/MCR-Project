using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float moveSpeed { get; set; } = 500;

	[Export]
	public float moveAcc { get; set; } = 2000;

	[Export]
	public float eatAcc { get; set; } = 2000;




	// state
	[Export]
	public bool onEat { get; set; } = false;

	[Export]
	public double eatModeTime { get; set; } = 0.2;

	private double _eatModeTimer =0;

	private Sprite2D _sprite;



	private bool[] _directionTracker = new bool[4]{false,false,false,false};


	private Vector2 _dirVector = new Vector2(0,0);

	private Vector2 _mousePos = new Vector2(0,0);

	private float _actualVel=0;


	public void initEat(){
		onEat=true;
		this.Velocity = _dirVector * eatAcc;
		this._eatModeTimer=eatModeTime;
	}




	public override void _Input(InputEvent @event)
	{

		_directionTracker[0]=_directionTracker[0]?(!@event.IsActionReleased("up")):@event.IsActionPressed("up");
		_directionTracker[1]=_directionTracker[1]?(!@event.IsActionReleased("down")):@event.IsActionPressed("down");
		_directionTracker[2]=_directionTracker[2]?(!@event.IsActionReleased("right")):@event.IsActionPressed("right");
		_directionTracker[3]=_directionTracker[3]?(!@event.IsActionReleased("left")):@event.IsActionPressed("left");


		if (@event.IsActionPressed("eat"))
		{
			initEat();
		}
	}

	public override void _Ready(){
		GD.Print(moveSpeed);
		_sprite=GetNode<Sprite2D>("Sprite");
		GD.Print(_sprite.Name);
	}


	public override void _Process(double delta){
		if(this.onEat){
			if(this._eatModeTimer>=0){
				this._eatModeTimer-=delta;
			}
			else{
				GD.Print(this._eatModeTimer);
				this.onEat=false;
			}
		}
	}


	public override void _PhysicsProcess(double delta)
	{
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

		_dirVector.X=x;
		_dirVector.Y=y;

		_dirVector=_dirVector.Normalized();

		_sprite.FlipH=(x!=0)?x==-1:_sprite.FlipH;

		Rect2 rect2 = _sprite.RegionRect;
		Vector2 p =  rect2.Position;
		p.X=onEat?32:0;
		rect2.Position=p;
		_sprite.RegionRect=rect2;

		Velocity=MathUtils.approachVector(Velocity,_dirVector * moveSpeed,(float)(delta* moveAcc * 10));



		MoveAndSlide();

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
		}
	}

}
