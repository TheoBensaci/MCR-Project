public class LightBallDecorator : AbstractItemDecorator
{
    public LightBallDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override int GetPrice()
    {
        return base.GetPrice() + 10;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().SetJunkModel(3);
    }
}