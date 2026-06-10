using Godot;
using System;

public partial class IconsLib : Node2D
{
    public void SelectIcon(string name){
        for (int i = 0; i < GetChildCount(); i++)
        {
            Node2D node = (Node2D)GetChild(i);
            node.Visible=(node.Name==name);
        }
    }
}
