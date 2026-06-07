using Godot;
using System;

public partial class ItemWrapper : Node2D
{
    public Item item = null;

    [Export]
    public Node2D spiteContainer { get; set; }


    public void init(Item item){
        this.item=item;

    }


    public void _on_area_2d_body_entered(Node2D other){
        if(other is Player player){
            player.Eat(this.item);
            QueueFree();
        }
    }
}
