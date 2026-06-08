
using System;
using System.Collections.Generic;
using Godot;

public class RunResume{

    private Dictionary<string,int> _itemTracker = new Dictionary<string, int>();
    private int _hit = 0;

    public RunResume(){
        foreach (KeyValuePair<string,Func<Item, Item>> item in ItemFactory.decorators)
        {
            _itemTracker.Add(item.Key,0);
        }
    }


    public void addItem(Item item){
        List<string> decorators = item.GetDecoratorsLists();
        foreach (string itemName in decorators)
        {
            if(!_itemTracker.ContainsKey(itemName))return;
            GD.Print(itemName);
            _itemTracker[itemName]++;
        }
    }

    public void addHit(){
        _hit++;
    }

    public Dictionary<string,int> getItemTracker(){
        return _itemTracker;
    }

    public int getHit(){
        return _hit;
    }


    public void reset(){
        foreach (KeyValuePair<string,int> i in _itemTracker)
        {
            _itemTracker[i.Key]=0;
        }
        _hit=0;
    }


}