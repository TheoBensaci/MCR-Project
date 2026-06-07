
using System;
using System.Collections.Generic;
using Godot;

public class RunResume{

    public Dictionary<string,int> itemTracker = new Dictionary<string, int>();

    public RunResume(){
        foreach (KeyValuePair<string,Func<Item, Item>> item in ItemFactory.decorators)
        {
            itemTracker.Add(item.Key,0);
        }
    }


    public void addItem(Item item){

    }



}