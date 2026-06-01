
public enum ItemGroup{
    none,
    g1,
    g2
}

public interface Item{

    /// <summary>
    /// Get item group of this item
    /// </summary>
    public ItemGroup GetGroup();

    /// <summary>
    /// function call by the player when this item is eat
    /// </summary>
    /// <param name="pl">actual player</param>
    /// <returns>if the OnEat chain need to continue (return true) else (return false)</returns>
    public bool OnEat(Player pl);

    public int GetPrice();
}