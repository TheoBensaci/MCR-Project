public class CanDecorator : AbstractItemDecorator
{
    public CanDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override int GetPrice()
    {
        return base.GetPrice() + 10;
    }

    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().SetJunkModel(5);
    }
}