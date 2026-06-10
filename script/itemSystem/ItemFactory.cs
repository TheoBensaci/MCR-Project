

using System;
using System.Collections.Generic;
using Godot;

public static class ItemFactory{
    public static Dictionary<string,Func<Item,Item>> decorators = new Dictionary<string,Func<Item,Item>>{
        {"TrashBag",n=>new ModelDecorator(n,0,"TrashBag")},
        {"Paper",n=>new ModelDecorator(n,2,"Paper")},
        {"LightBall",n=>new ModelDecorator(n,3,"LightBall")},
        {"PaperBall",n=>new ModelDecorator(n,4,"PaperBall")},
        {"Cigarette",n=>new ModelDecorator(n,1,"Cigarette")},
        {"Can",n=>new ModelDecorator(n,5,"Can")},
        {"Acceleration",n=>new AccelerationDecorator(n,500,0.75)},
        {"Tp",n=>new TpDecorator(n)},
        {"LootBox",n=>new LootBoxDecorator(n,3)},
        {"Speed",n=>new SpeedDecorator(n,20)},
        {"Heal",n=>new HealDecorator(n,5)},
        {"Invicibility",n=>new InvicibilityDecorator(n,0.2)},
        {"Greedy",n=>new GreedyDecorator(n,1,1)},
        {"Spike",n=>new SpikeDecorator(n,5f)},
        {"Poison",n=>new PoisonDecorator(n)},
        {"Curse",n=>new CurseDecorator(n,1.5,7f)},
        {"Random",n=>{
            // possible decorator the random decorator can be
            string[] possibleDec={
                "Speed",
                "Tp",
                "LootBox",
                "Acceleration",
                "Heal",
                "Invicibility",
                "Greedy",
                "Spike",
                "Poison",
                "Curse"
            };

            // return a random decorator
            return new RandomDecorator(decorators[possibleDec[GD.Randi()%possibleDec.Length]](n));
        }}
    };

    public static Item CreateItem(string[] decoratorNames){
        Item item = new BaseItem();
        foreach (string name in decoratorNames)
        {
            if(decorators.ContainsKey(name)){
                item = decorators[name](item);
            }
        }
        return item;
    }
}