using Godot;
using System;

public partial class UiManager : Node
{
    [Export]
    public Godot.Collections.Dictionary<string, UiScreen> screens = new Godot.Collections.Dictionary<string, UiScreen>();


    public void Show(string name){
        GD.Print(name);
        foreach (string item in screens.Keys)
        {
            if(item==name){
                screens[item].ShowScreen();
            }
            else{
                screens[item].HideScreen();
            }
        }
    }
}
