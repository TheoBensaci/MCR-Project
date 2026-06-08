

using Godot;

public class UpdatedItemDeco : AbstractItemDecorator
{
    private double _timer = 0;
    public UpdatedItemDeco(Item baseItem) : base(baseItem)
    {
        _timer = 2;
    }

    public override void OnEat(Player pl){
        base.OnEat(pl);
    }

    public override bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        return base.UpdateOnEat(player,eatedItem,arena) || true;
    }

    public override bool Update(Player player, ArenaManager arena, double delta_t){
        bool next = base.Update(player,arena,delta_t);
        if(_timer>0){
            _timer-=delta_t;
            return true;
        }
        return next;
    }
}