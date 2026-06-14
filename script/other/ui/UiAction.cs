/**
 *   Autheur: Theo Bensaci
 *   Date: 20:47 10.06.2026
 *   Description: Ui actions, call when a screen is showned and the shopkeeper arrive
 */

using Godot;
using System;

public partial class UiAction : Node
{
    /// <summary>
    /// Call when the screen is showned and the shopkeeper as arrive
    /// </summary>
    public virtual void Action(){
    }

    /// <summary>
    /// Call when the screen is showned and the animation as eneded
    /// </summary>
    public virtual void EndAction(){
    }
}
