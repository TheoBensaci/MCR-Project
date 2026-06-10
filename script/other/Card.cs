using Godot;
using System;


public partial class Card : Button
{
    [Export]
    public float hoverScale=1.2f;

    [Export]
    public float clickScale=1.2f;

    private float _targetScale=1f;

    [Export]
    public Color enableColor;

    [Export]
    public Color disabelColor;

    [Export]
    public Label desciption;

    [Export]
    public Label price;


    public Action action;


    public Card SetValue(string desciption, string price, Action action){
        this.desciption.Text=desciption;
        this.price.Text=price;
        this.action=action;
        return this;
    }


    public void _on_card_button_up(){

    }


    public override void _Process(double delta)
    {
        if(IsHovered()){
            GD.Print("test");
        }
    }
}
