using Godot;
using System;

public static class MathUtils
{
    public static float approach(float value, float target, float step){
        return value > target ? Mathf.Max(value - step, target) : Mathf.Min(value + step, target);
    }

    public static Vector2 approachVector(Vector2 value, Vector2 target, float step){
        value.X = approach(value.X,target.X,step);
        value.Y = approach(value.Y,target.Y,step);
        return value;
    }
}
