

using System;
using System.Collections.Generic;

public static class ItemFactory{
    private static Dictionary<string,Func<Item,Item>> _decorators = new Dictionary<string,Func<Item,Item>>{
        {"Test",n=>new TestItemDeco(n)}
    };

    public static Item CreateItem(string[] decorators){
        Item item = new BaseItem();
        foreach (string name in decorators)
        {
            item = _decorators[name](item);
        }
        return item;
    }
}