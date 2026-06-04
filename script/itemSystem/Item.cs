
using System;
using System.Collections.Generic;

public interface Item{

    /// <summary>
    /// Get item group of this item
    /// </summary>
    public List<ItemTag> GetTags();

    /// <summary>
    /// function call by the player when this item is eat
    /// </summary>
    /// <param name="pl">actual player</param>
    public void OnEat(Player pl);

    public int GetPrice();

    public List<string> getIcons();

    /*
        The update function familie are callbacks use to implement effect when a item was eaten
        They are call on event during certain even and return a boolean, if one of thoses function return a false, the item is earse and
        will not longer be updated
    */


    public bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena);

    public bool Update(Player player, ArenaManager arena, double delta_t);

}