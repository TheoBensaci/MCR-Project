
using System;
using System.Collections.Generic;
using Godot;

public class RunResume{

    private Dictionary<string,int> _itemTracker = new Dictionary<string, int>();
    private int _hit = 0;

    public RunResume(){
    }


    public void addItem(Item item){
        List<string> decorators = item.GetDecoratorsLists();
        foreach (string itemName in decorators)
        {
            if(!_itemTracker.ContainsKey(itemName)){
                _itemTracker.Add(itemName,1);
                continue;
            }
            _itemTracker[itemName]++;
        }
    }

    public void addHit(){
        _hit++;
    }

    public int getItemCount(string name){
        if(!_itemTracker.ContainsKey(name))return 0;
        return _itemTracker[name];
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