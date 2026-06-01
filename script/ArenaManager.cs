using Godot;
using System;
using System.IO;


public partial class ArenaManager : Node
{
    [Export]
    public float arenaRadius { get; set; } = 2000;

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


    [Export]
    public PackedScene itemWrapperSceen { get; set; }

    public Player playerInstance;


    public void SpawnItem(){
        // make a buffer with a ItemWrapper class
        ItemWrapper buffer = (ItemWrapper)itemWrapperSceen.Instantiate();

        // set position
        // TODO check if the place is free befor to have a better repartition
        buffer.Position = new Vector2(GD.Randf()*2-1,GD.Randf()*2-1).Normalized() *
        ( GD.Randf() * (arenaRadius - (minItemSpawnRadius + itemSpawnPadding)) + minItemSpawnRadius);


        // init item
        Item item = new BaseItem();

        item = new TestItemDeco(item);

        // add decorator

        buffer.item=item;

        AddChild(buffer);
    }

    public void StartArena(){
        for (int i = 0; i < nItemAtSpawn; i++)
        {
            SpawnItem();
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
    }

}
