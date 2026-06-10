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

    public List<string> baseDecorators=new List<string>();


    [ExportSubgroup("item spawn")]
    [Export]
    public float itemSpawnPadding { get; set; } = 100;

    [Export]
    public float minItemSpawnRadius{ get; set; } = 50;


    [Export]
    public double spawnItemTime { get; set; } = 2;
    private double _spawnItemTimer = 0;

    [Export]
    public float spawnSafeDistance { get; set; } = 100f;

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
    public Node2D hazardContainer;

    [Export]
    public int actualHazardLevel { get; set; } = 0;

    [Export]
    public Godot.Collections.Array<double> spawnHazardTimeByLevel = new Godot.Collections.Array<double>();

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

    public Vector2 GetSafeRandomPos(){
        Vector2 result=GetRandomPos();
        float d = spawnSafeDistance * spawnSafeDistance;
        bool ok;
        // safe net
        for (int j = 0; j < 1000; j++)
        {
            ok=true;
            if(playerInstance!=null)ok = result.DistanceSquaredTo(playerInstance.Position) > d;
            for (int i = 0; i < GetChildCount(); i++)
            {
                if(!ok){
                    break;
                }
                Node2D n = (Node2D)GetChild(i);
                ok = result.DistanceSquaredTo(n.Position) > d;
            }
            if(ok){
                return result;
            }
            result=GetRandomPos();
        }
        return result;
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

        int nDeco = UtilsRandom.GetRandomResult(weightNumberOfDecorateurItem,GD.Randf(),0);
        // add base tag

        for (int i = 0; i < nDeco; i++)
        {
            decorators.Add(UtilsRandom.GetRandomResult(weightAddDecorateurItem,GD.Randf(),""));
        }

        foreach (string item in baseDecorators)
        {
            decorators.Add(item);
        }

        foreach (string item in banDecorator)
        {
            decorators.Remove(item);
        }


        AddChild(buffer);

        // create item
        buffer.init(ItemFactory.CreateItem(decorators.ToArray()));
    }

    public void SpawnItem(){
        SpawnItem(GetSafeRandomPos(),new string[0]);
    }

    public void SpawnHazard(){
        SpawnHazard(GetRandomPos(Vector2.Zero,arenaRadius/2,0));
    }

    public void SpawnHazard(Vector2 position){
        if(hazards.Count==0)return;
        PackedScene scene = hazards[(int)(GD.Randi()%hazards.Count)];

        HazardObject hazard = (HazardObject)scene.Instantiate();
        hazardContainer.AddChild(hazard);
        hazard.Start();
        hazard.Position=position;
    }

    public void AddHazardLevel(){
        actualHazardLevel=Math.Min(actualHazardLevel+1,spawnHazardTimeByLevel.Count-1);
    }


    public void StartArena(){

        UtilsRandom.ClearAllChild(this);
        UtilsRandom.ClearAllChild(hazardContainer);

        main.ChangeCamera("Arena");
        runResume=new RunResume();
        _spawnHazardTimer=0;
        _spawnItemTimer=0;
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
        if(actualHazardLevel>=0){
            _spawnHazardTimer += delta;
            if(_spawnHazardTimer >= spawnHazardTimeByLevel[actualHazardLevel]){
                SpawnHazard();
                _spawnHazardTimer = 0;
            }
        }

    }

}
