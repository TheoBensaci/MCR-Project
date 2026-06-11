using Godot;
using System;
using System.Collections.Generic;

public partial class ItemWrapper : Node2D
{
    public Item item = null;

    [Export]
    public Node2D spiteContainer { get; set; }

    [Export]
    public Node2D iconContainer;

    [Export]
    public PackedScene iconLib;

    [Export]
    public float iconSpace=10f;

    [Export]
    public float iconScale=1f;


    public void init(Item item){
        this.item=item;

        ItemRenderInfo iri = item.GetRenderInfo();

        // get model
        for (int i = 0; i < spiteContainer.GetChildCount(); i++)
        {
            spiteContainer.GetChild<Node2D>(i).Visible=i==iri.GetJunkModel();
        }

        // gen icon
        List<string> icons = iri.GetIcons();

        float w = (icons.Count-1) * iconSpace;
        for (int i = 0; i < icons.Count; i++)
        {
            IconsLib icon = (IconsLib)iconLib.Instantiate();
            iconContainer.AddChild(icon);
            icon.Position = new Vector2(i*iconSpace - w /2,0);
            icon.SelectIcon(icons[i]);
        }




        GetNode<AnimationPlayer>("AnimationPlayer").Play("spawn");
    }


    public void _on_area_2d_body_entered(Node2D other){
        if(other is Player player){
            player.Eat(this.item);
            QueueFree();
        }
    }
}
