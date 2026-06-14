/**
 *   Autheur: Theo Bensaci
 *   Date: 23:23 08.06.2026
 *   Description: render info of a item
 */

using System;
using System.Collections.Generic;

public class ItemRenderInfo{
    private List<string> _icons = new List<string>();

    /// <summary>
    /// Add a icon to the info
    /// </summary>
    /// <param name="icon">name of the icon</param>
    /// <returns></returns>
    public ItemRenderInfo AddIcon(string icon){
        _icons.Add(icon);
        return this;
    }

    /// <summary>
    /// Remove a icon from the info
    /// </summary>
    /// <param name="icon">name of the icon</param>
    /// <returns></returns>
    public ItemRenderInfo RemoveIcon(string icon){
        _icons.Remove(icon);
        return this;
    }

    /// <summary>
    /// Replace a specific icon in the info
    /// </summary>
    /// <param name="index">index of the icon in the lists of icons</param>
    /// <param name="icon">new icon name</param>
    /// <returns></returns>
    public ItemRenderInfo ReplaceIcon(int index,string icon){
        if(index<0 || index>_icons.Count)return this;
        _icons[(int)index]=icon;
        return this;
    }

    /// <summary>
    /// Get the list of icon
    /// </summary>
    /// <returns></returns>
    public List<string> GetIcons(){
        return _icons;
    }
}