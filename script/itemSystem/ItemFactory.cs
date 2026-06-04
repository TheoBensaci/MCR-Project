

using System;
using System.Collections.Generic;

public static class ItemFactory{
    public static Dictionary<string,Func<Item,Item>> decorators = new Dictionary<string,Func<Item,Item>>{
        {"Test",n=>new TestItemDeco(n)},
        {"Update",n=>new UpdatedItemDeco(n)}
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