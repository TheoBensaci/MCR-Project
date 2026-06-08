using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class ArenaManager : Node
{
    [Export]
    public MainSceen main;

    [ExportSubgroup("arenna")]
    [Export]
    public float arenaRadius { get; set; } = 2000;

    [ExportSubgroup("decorateur spawn")]
    [Export]
    public Godot.Collections.Dictionary<string, float> weightBaseItem = new Godot.Collections.Dictionary<string, float>();
    [Export]
    public Godot.Collections.Dictionary<string, float> weightAddDecorateurItem = new Godot.Collections.Dictionary<string, float>();
    [Export]
    public Godot.Collections.Dictionary<int, float> weightNumberOfDecorateurItem = new Godot.Collections.Dictionary<int, float>();


    [ExportSubgroup("item spawn")]
    [Export]
    public float itemSpawnPadding { get; set; } = 100;

    [Export]
    public float minItemSpawnRadius{ get; set; } = 50;


    [Export]
    public double spawnItemTime { get; set; } = 2;
    private double _spawnItemTimer = 0;

    [Export]
    public int nItemAtSpawn { get; set; } = 20;

    [Export]
    public int minItemNumber { get; set; } = 3;

    [Export]
    public int maxItemNumber { get; set; } = 3;


    [ExportSubgroup("item wrapper")]
    [Export]
    public PackedScene itemWrapperSceen { get; set; }

    public Player playerInstance;

    [ExportSubgroup("stats")]
    [Export]
    public int targetMoney { get; set; } = 100;

    [ExportSubgroup("Hazard")]

    [Export]
    public int actualHazardLevel { get; set; } = 0;

    [Export]
    public double spawnHazardTime { get; set; } = 10;
    private double _spawnHazardTimer = 0;

    [Export]
    public Godot.Collections.Array<PackedScene> hazards {get;set;}


    public RunResume runResume=null;


    public Vector2 GetRandomPos(Vector2 center, float radiuse, float deadZone){
        Vector2 result = center + new Vector2(GD.Randf()*2-1,GD.Randf()*2-1).Normalized() *
        ( GD.Randf() * (radiuse - deadZone) + deadZone);

        // check if result isn't outside of the arena, if else, push back
        if(result.LengthSquared()>(arenaRadius*arenaRadius)){
            result=result.Normalized()*arenaRadius;
        }

        return result;
    }

    public Vector2 GetRandomPos(){
        return GetRandomPos(Vector2.Zero,arenaRadius-itemSpawnPadding,minItemSpawnRadius);
    }

    public void SpawnItem(Vector2 pos,string[] banDecorator){
        // make a buffer with a ItemWrapper class
        ItemWrapper buffer = (ItemWrapper)itemWrapperSceen.Instantiate();

        // set position
        buffer.Position = pos;

        // generate decorateurs
        List<string> decorators = new List<string>(){
            UtilsRandom.GetRandomResult(weightBaseItem,GD.Randf(),"")
        };

        //int nDeco = UtilsRandom.GetRandomResult(weightNumberOfDecorateurItem,GD.Randf(),0);
        // add base tag

        decorators.Add("Test");


        decorators.Add("Speedy");

        decorators.Add("Random");


        foreach (string item in banDecorator)
        {
            decorators.Remove(item);
        }


        AddChild(buffer);

        // create item
        buffer.init(ItemFactory.CreateItem(decorators.ToArray()));
    }

    public void SpawnItem(){
        SpawnItem(GetRandomPos(),new string[0]);
    }

    public void SpawnHazard(){
        SpawnHazard(GetRandomPos());
    }

    public void SpawnHazard(Vector2 position){
        if(hazards.Count==0)return;

        PackedScene scene = hazards[(int)GD.Randi()%hazards.Count];

        Node2D hazard = (Node2D)scene.Instantiate();
        hazard.Position = position;

        AddChild(hazard);
    }

    public void SpawnRandomHazard(){
        float angle = GD.Randf() * Mathf.Tau;
        float dist = GD.Randf() * (arenaRadius - 200) + 100;
        Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dist;
        SpawnHazard(pos);
    }

    public void StartArena(){

        for (int i = 0; i < GetChildCount(); i++)
        {
            RemoveChild(GetChild(i));
        }

        main.ChangeCamera("Arena");
        runResume=new RunResume();
        for (int i = 0; i < nItemAtSpawn; i++)
        {
            SpawnItem();
        }
        if(playerInstance!=null)playerInstance.Spawn();
    }

    public void EndArena(){
        main.ChangeCamera("Score");
    }

    public override void _Ready(){
        StartArena();
    }

    public override void _Input(InputEvent @event)
    {

        if(@event.IsActionReleased("t2")){
            StartArena();
        }
    }


    public override void _Process(double delta){
        if(playerInstance==null || playerInstance.isDead)return;

        // spawn timer
        if(_spawnItemTimer>=spawnItemTime && GetChildCount()<maxItemNumber){
            SpawnItem();
            _spawnItemTimer=0;
        }
        else{
            _spawnItemTimer+=delta;
        }

        if(GetChildCount()<minItemNumber){
            for (int i = 0; i < minItemNumber-GetChildCount(); i++)
            {
                SpawnItem();
            }
        }

        // hazard timer
        _spawnHazardTimer += delta;
        if(_spawnHazardTimer >= spawnHazardTime){
            SpawnHazard();
            _spawnHazardTimer = 0;
        }

    }

}
