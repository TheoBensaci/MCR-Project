
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