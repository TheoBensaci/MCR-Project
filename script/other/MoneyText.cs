/**
 *   Autheur: Theo Bensaci
 *   Date: 20:07 10.06.2026
 *   Description: money text
 */

using Godot;
using System;

public partial class MoneyText : Label
{
    [Export]
    public int minNumberLength=4;

    /// <summary>
    /// Set money
    /// </summary>
    /// <param name="money"></param>
    public void SetMoney(int money){
        Text=money.ToString("D"+minNumberLength)+"$";
    }

}
