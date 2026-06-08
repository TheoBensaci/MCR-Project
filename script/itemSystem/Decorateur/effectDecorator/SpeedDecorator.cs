using Godot;

public class SpeedDecorator : SpeedyDecorator
{
    private double _lifeTime=0;
    private bool _ended=false;
    public SpeedDecorator(Item baseItem,int amount, double duration) : base(baseItem,amount)
    {
        _lifeTime=duration;
    }


    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }

    public override bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        return true || base.UpdateOnEat(player,eatedItem,arena);
    }

    public override bool Update(Player player, ArenaManager arena, double delta_t){

        bool next = base.Update(player,arena,delta_t);
        if(!_ended){
            if(_lifeTime>0){
                _lifeTime-=delta_t;
                return true;
            }
            else{
                _ended=true;
                player.actualMovementSpeed-=p_amount;
            }
        }

        return next;
    }
}