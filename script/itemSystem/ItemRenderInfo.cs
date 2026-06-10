using System;
using System.Collections.Generic;

public class ItemRenderInfo{
    private int _junkModel=0;
    private List<string> _icons = new List<string>();

    public ItemRenderInfo SetJunkModel(int junkModel){
        _junkModel=junkModel;
        return this;
    }

    public ItemRenderInfo AddIcon(string icon){
        _icons.Add(icon);
        return this;
    }
    public ItemRenderInfo RemoveIcon(string icon){
        _icons.Remove(icon);
        return this;
    }

    public ItemRenderInfo ReplaceIcon(int index,string icon){
        if(index<0 || index>_icons.Count)return this;
        _icons[(int)index]=icon;
        return this;
    }

    public List<string> GetIcons(){
        return _icons;
    }

    public int GetJunkModel(){
        return _junkModel;
    }
}