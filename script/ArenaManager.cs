using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class ArenaManager : Node
{
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



    public Vector2 GetRandomPos(){
        return new Vector2(GD.Randf()*2-1,GD.Randf()*2-1).Normalized() *
        ( GD.Randf() * (arenaRadius - (minItemSpawnRadius + itemSpawnPadding)) + minItemSpawnRadius);
    }

    public void SpawnItem(Vector2 pos){
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
        decorators.Add("Update");


        // create item
        buffer.item=ItemFactory.CreateItem(decorators.ToArray());

        AddChild(buffer);
    }

    public void StartArena(){
        for (int i = 0; i < nItemAtSpawn; i++)
        {
            SpawnItem(GetRandomPos());
        }
        if(playerInstance!=null)playerInstance.Spawn();
    }

    public override void _Ready(){
        StartArena();
    }


    public override void _Process(double delta){
        if(playerInstance==null || playerInstance.isDead)return;

        // spawn timer
        if(_spawnItemTimer>=spawnItemTime && GetChildCount()<maxItemNumber){
            SpawnItem(GetRandomPos());
            _spawnItemTimer=0;
        }
        else{
            _spawnItemTimer+=delta;
        }

        if(GetChildCount()<minItemNumber){
            for (int i = 0; i < minItemNumber-GetChildCount(); i++)
            {
                SpawnItem(GetRandomPos());
            }
        }

        // hazard timer
    }

}
