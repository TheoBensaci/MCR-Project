using Godot;
using System;

public partial class ShopCard : PanelContainer
{
		
	[Export] private Label _cardName;
	[Export] private Label _description;
	[Export] private Label _price;
	[Export] private Button _buyButton;

	private CardData _cardData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_buyButton.Pressed += OnBuyPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
		public void Setup(CardData data)
	{
		_cardData = data;
		_cardName.Text = data.CardName;
		_description.Text = data.Description;
		_price.Text = $"{data.Price} gold";
	}

	private void OnBuyPressed()
	{
		GD.Print($"Acheté : {_cardData.CardName}");
	}
}
