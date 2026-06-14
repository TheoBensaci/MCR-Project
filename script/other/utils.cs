/**
 *   Autheur: Theo Bensaci
 *   Date: 23:23 08.06.2026
 *   Description: utils random
 */


using Godot;

public static class UtilsRandom{

    /// <summary>
    /// Get a random value but use weight to do that
    /// </summary>
    /// <param name="weights">dico of value and weigth</param>
    /// <param name="t">value bewteen 0 and 1</param>
    /// <param name="defaultValue">default value in case of no result</param>
    /// <typeparam name="T">type of result</typeparam>
    /// <returns></returns>
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
    /// <summary>
    /// Approache a value by step
    /// </summary>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static float approach(float value, float target, float step){
        return value > target ? Mathf.Max(value - step, target) : Mathf.Min(value + step, target);
    }

    /// <summary>
    /// Approach a vector by step
    /// </summary>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static Vector2 approachVector(Vector2 value, Vector2 target, float step){
        value.X = approach(value.X,target.X,step);
        value.Y = approach(value.Y,target.Y,step);
        return value;
    }
}


public static class OtherUtils
{
    /// <summary>
    /// Clear all child of a node
    /// </summary>
    /// <param name="parent"></param>
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