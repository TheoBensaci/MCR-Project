/**
 *   Autheur: Theo Bensaci
 *   Date: 10:19 04.06.2026
 *   Description: Use to make a item simpler
 */



using System;
using System.Collections.Generic;
using Godot;

public static class ItemFactory{

    // big list for function use to randomise decorators put on the item
    public static Dictionary<string,Func<Item,Item>> decorators = new Dictionary<string,Func<Item,Item>>{
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

    /// <summary>
    /// Create a item with a set of decorators
    /// </summary>
    /// <param name="type">type of the item</param>
    /// <param name="decoratorNames">list of decorators (names)</param>
    /// <returns>item created</returns>
    public static Item CreateItem(ItemType type,string[] decoratorNames){
        Item item = new BaseItem(type);
        foreach (string name in decoratorNames)
        {
            if(decorators.ContainsKey(name)){
                item = decorators[name](item);
            }
        }
        return item;
    }
}