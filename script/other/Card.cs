/**
 *   Autheur: Theo Bensaci
 *   Date: 20:23 10.06.2026
 *   Description: simple card buttons
 */

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
    public float scaleSpeed=1000f;

    [Export]
    public Color enableColor;

    [Export]
    public Color disabelColor;

    [Export]
    public Label desciption;

    [Export]
    public Label price;

    [Export]
    public TextureRect background;

    public Action action;


    /// <summary>
    /// Set card value
    /// </summary>
    /// <param name="desciption">descritpion of the card</param>
    /// <param name="price">price of the card</param>
    /// <param name="action">onclick action of the card</param>
    /// <returns></returns>
    public Card SetValue(string desciption="", string price="", Action action=null){
        this.desciption.Text=desciption;
        this.price.Text=price;
        this.action=action;
        return this;
    }


    public void _on_card_button_up(){
        if(Disabled)return;
        if(action!=null)action.Invoke();
        setActive(false);
        this.desciption.Text="";
        this.price.Text="";
        Scale=new Vector2(clickScale,clickScale);
    }

    public void _on_card_button_down(){

    }


    /// <summary>
    /// Set card color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private Card setColor(Color color){
        price.LabelSettings.FontColor=color;
        desciption.LabelSettings.FontColor=color;
        background.Modulate = color;


        return this;
    }


    /// <summary>
    /// Set if the card is disabeled or not
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public Card setActive(bool state){
        Disabled=!state;
        return setColor(state?enableColor:disabelColor);
    }


    public override void _Process(double delta)
    {
        if(Disabled){
            _targetScale=1;
        }
        else{
            if(IsHovered()){
                _targetScale=hoverScale;
            }
            else{
                _targetScale=1;
            }
        }

        Scale=MathUtils.approachVector(Scale,new Vector2(_targetScale,_targetScale),(float)delta * scaleSpeed);
    }
}
