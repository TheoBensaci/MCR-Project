/**
 *   Autheur: Theo Bensaci
 *   Date: 23:23 08.06.2026
 *   Description: resum run perfomace, keep track of useful data for bet
 */


using System;
using System.Collections.Generic;
using Godot;

public class RunResume{


    private Dictionary<string,int> _decoratorTracker = new Dictionary<string, int>();
    private int[] _itemTypeTracker;
    private int _hit = 0;

    public RunResume(){
        int max = 0;
        // get the max value of itemtype enum
        foreach(int i in Enum.GetValues(typeof(ItemType)))
            max = Math.Max(i,max);

        // create a int array to keep track of the number of each type eatent by the player
        _itemTypeTracker = new int[max+1];
        for (int i = 0; i < _itemTypeTracker.Length; i++)
        {
            _itemTypeTracker[i]=0;
        }
    }

    /// <summary>
    /// Add a item to the tracker
    /// </summary>
    /// <param name="item">item to add</param>
    public void AddItem(Item item){
        List<string> decorators = item.GetDecoratorsLists();
        foreach (string itemName in decorators)
        {
            if(!_decoratorTracker.ContainsKey(itemName)){
                _decoratorTracker.Add(itemName,1);
                continue;
            }
            _decoratorTracker[itemName]++;
        }
        _itemTypeTracker[(int)item.GetItemType()]++;
    }

    /// <summary>
    /// add a hit to tracker
    /// </summary>
    public void AddHit(){
        _hit++;
    }

    /// <summary>
    /// Get the number of a specifique type of decorator meet during the run
    /// </summary>
    /// <param name="name">name of decorator</param>
    /// <returns></returns>
    public int GetDecoratorCount(string name){
        if(!_decoratorTracker.ContainsKey(name))return 0;
        return _decoratorTracker[name];
    }

    /// <summary>
    /// Get the number of a specifique type of item meet during the run
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetItemCount(ItemType type){
        return _itemTypeTracker[(int)type];
    }

    /// <summary>
    /// Get number of hit taken during the run
    /// </summary>
    /// <returns></returns>
    public int GetHit(){
        return _hit;
    }


    public void Reset(){
        foreach (KeyValuePair<string,int> i in _decoratorTracker)
        {
            _decoratorTracker[i.Key]=0;
        }
        _hit=0;
        for (int i = 0; i < _itemTypeTracker.Length; i++)
        {
            _itemTypeTracker[i]=0;
        }
    }


}