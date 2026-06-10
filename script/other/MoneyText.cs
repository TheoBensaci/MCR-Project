using Godot;
using System;

public partial class MoneyText : Label
{
    [Export]
    public int minNumberLength=4;


    public void SetMoney(int money){
        string str = money.ToString();
        int n = minNumberLength-str.Length;
        for (int i = 0; i < n; i++)
        {
            str="0"+str;
        }
        Text=str+"$";
    }

}
