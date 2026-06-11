
using Godot;

public static class UtilsRandom{
    public static T GetRandomResult<[MustBeVariant] T>(Godot.Collections.Dictionary<T, float> weights, float t,T defaultValue){

        // get total
        float total = 0;
        foreach (var (value, amount) in weights)
        {
            total+=amount;
        }

        // chance
        t*=total;
        total = 0;
        foreach (var (value, amount) in weights)
        {
            if(t>total && t<=(total+amount)){
                return value;
            }
            total+=amount;
        }
        return defaultValue;
    }

}

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


public static class OtherUtils
{
    public static void ClearAllChild(Node parent){
        int n = parent.GetChildCount();
        for (int i = 0; i < n; i++)
        {
            Node node = parent.GetChild(0);
            parent.RemoveChild(node);
            node.QueueFree();
        }
    }
}