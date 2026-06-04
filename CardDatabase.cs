using Godot;
using Godot.Collections;

[GlobalClass]
public partial class CardDatabase : Resource
{
	private static readonly Array<CardData> AllCards = new()
	{
		new CardData { CardName = "Résistance élémentaire", Description = "Empêche un type de dégât de blesser le joueur", Price = 60 },
		new CardData { CardName = "Butin céleste", Description = "Être touché a une chance de faire apparaître un objet divin à côté du joueur", Price = 30 },
		new CardData { CardName = "Sablier", Description = "Chaque objet ramassé ajoute un peu de temps", Price = 50 },
		new CardData { CardName = "Tout améliorer", Description = "Chaque objet reçoit un décorateur supplémentaire", Price = 120 },
		new CardData { CardName = "Vitalité", Description = "Augmente les HP du joueur de 15%", Price = 55 },
		new CardData { CardName = "Bouclier passif", Description = "Bloque 1 dégât par manche", Price = 25 },
		new CardData { CardName = "Invulnérabilité", Description = "Courte invulnérabilité lors du ramassage d'un objet", Price = 100 },
		new CardData { CardName = "Chance dorée", Description = "Augmente la probabilité d'obtenir un décorateur rare", Price = 30 },
	};

	public Array<CardData> GetRandomCards(int count)
	{
		var available = new Array<CardData>(AllCards);
		var result = new Array<CardData>();

		for (int i = 0; i < count && available.Count > 0; i++)
		{
			int index = GD.RandRange(0, available.Count - 1);
			result.Add(available[index]);
			available.RemoveAt(index);
		}

		return result;
	}
}
