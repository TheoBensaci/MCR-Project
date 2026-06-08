public class PaperBallDecorator : AbstractItemDecorator
{
    public PaperBallDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override int GetPrice()
    {
        return base.GetPrice() + 10;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().SetJunkModel(4);
    }
}