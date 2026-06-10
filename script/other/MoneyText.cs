using Godot;
using System;

public partial class MoneyText : Label
{
    [Export]
    public int minNumberLength=4;


    public void SetMoney(int money){
        Text=money.ToString("D"+minNumberLength)+"$";
    }

}
