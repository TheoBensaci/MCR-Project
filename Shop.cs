using Godot;
using Godot.Collections;

public partial class Shop : Node2D
{
	[Export] private HBoxContainer _cardsContainer;

	private PackedScene _cardScene;
	private CardDatabase _cardDatabase = new CardDatabase();

	public override void _Ready()
	{
		_cardScene = GD.Load<PackedScene>("res://scenes/ShopCard.tscn");
		GenerateCards();
	}

	private void GenerateCards()
	{
		foreach (Node child in _cardsContainer.GetChildren())
			child.QueueFree();

		Array<CardData> selected = _cardDatabase.GetRandomCards(2);

		foreach (CardData data in selected)
		{
			ShopCard card = _cardScene.Instantiate<ShopCard>();
			_cardsContainer.AddChild(card);
			card.Setup(data);
		}
	}
}
