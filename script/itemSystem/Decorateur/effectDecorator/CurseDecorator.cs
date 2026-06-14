using Godot;

public class CurseDecorator : AbstractItemDecorator
{
    private double _lifeTime=0;
    private double _duration=0;
    private float _amount=0;
    private bool _ended=false;
    public CurseDecorator(Item baseItem,double duration,float amount) : base(baseItem)
    {
        _lifeTime=_duration=duration;
        _amount=amount;
    }
    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().AddIcon("Curse");
    }

    public override void OnEat(Player pl)
    {
        pl.AddEffectIcon("Curse");
        base.OnEat(pl);
    }

    public override int GetPrice()
    {
        return p_baseItem.GetPrice() + 10;
    }

    public override bool UpdateOnEat(Player player, Item eatedItem, ArenaManager arena){
        if(!_ended){
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
                player.RemoveEffectIcon("Curse");
                player.Damage(_amount,DamageType.curse,true);
                _ended=true;
            }
        }

        return next;
    }
}