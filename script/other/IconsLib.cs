/**
 *   Autheur: Theo Bensaci
 *   Date: 18:47 10.06.2026
 *   Description: use to manage icon in the icon library
 */

using Godot;
using System;

public partial class IconsLib : Node2D
{
    private string _actualIcon="";

    /// <summary>
    /// Select a icon the icon library
    /// </summary>
    /// <param name="name"></param>
    public void SelectIcon(string name){
        for (int i = 0; i < GetChildCount(); i++)
        {
            Node2D node = (Node2D)GetChild(i);
            node.Visible=(node.Name==name);
            _actualIcon=name;
        }
    }

    /// <summary>
    /// Get actual name of the icons
    /// </summary>
    /// <returns></returns>
    public string GetIconName(){
        return _actualIcon;
    }
}
