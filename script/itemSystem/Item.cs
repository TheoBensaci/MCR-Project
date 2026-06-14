/**
 *   Autheur: Theo Bensaci
 *   Date: 22:05 01.06.2026
 *   Description: base item
 */


using System;
using System.Collections.Generic;

public interface Item{

    /// <summary>
    /// Get the item type
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType();

    /// <summary>
    /// Get render info the item
    /// </summary>
    /// <returns></returns>
    public ItemRenderInfo GetRenderInfo();

    /// <summary>
    /// Get the list of decorators name of the items
    /// </summary>
    /// <returns></returns>
    public List<string> GetDecoratorsLists();

    /// <summary>
    /// function call by the player when this item is eat
    /// </summary>
    /// <param name="pl">actual player</param>
    public void OnEat(Player pl);

    /// <summary>
    /// Get price of the item
    /// </summary>
    /// <returns></returns>
    public int GetPrice();
    /*
        The update function familie are callbacks use to implement effect when a item was eaten
        They are call on event during certain even and return a boolean, if one of thoses function return a false, the item is earse and
        will not longer be updated
    */


    /// <summary>
    /// call when ever the player eat a item
    /// </summary>
    /// <param name="player">player</param>
    /// <param name="eatedItem">item eated</param>
    /// <param name="arena">arena</param>
    /// <returns>true if this item need to be keep, false if this item can be eares from the update lists</returns>
    public bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena);


    /// <summary>
    /// call every tick of process update of player
    /// </summary>
    /// <param name="player">player</param>
    /// <param name="delta_t">dekta t between update</param>
    /// <param name="arena">arena</param>
    /// <returns>true if this item need to be keep, false if this item can be eares from the update lists</returns>
    public bool Update(Player player, ArenaManager arena, double delta_t);

}