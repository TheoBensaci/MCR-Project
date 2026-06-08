public class TrashBagDecorator : AbstractItemDecorator
{
    public TrashBagDecorator(Item baseItem) : base(baseItem)
    {
    }

    public override int GetPrice()
    {
        return base.GetPrice() + 10;
    }
}