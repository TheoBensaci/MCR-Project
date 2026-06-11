using Godot;

public class GreedyDecorator : AbstractItemDecorator
{
    private double _lifeTime=0;
    private double _duration=0;
    private float _bonnusAmount=0;
    private bool _ended=false;
    public GreedyDecorator(Item baseItem,double duration,float bonnusAmount) : base(baseItem)
    {
        _lifeTime=_duration=duration;
        _bonnusAmount=bonnusAmount;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().AddIcon("Greedy");
    }
    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }

    public override bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        if(!_ended){
            player.money+=(int)(eatedItem.GetPrice()*_bonnusAmount);
            _lifeTime=_duration;
        }
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
                GD.Print("END greedy");
                _ended=true;
            }
        }

        return next;
    }
}