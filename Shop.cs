using Godot;

public partial class Shop : Control
{
	public override void _Ready()
	{
		GD.Print("Boutique ouverte !");
	}

	public void OpenShop()
	{
		Visible = true;
	}

	public void CloseShop()
	{
		Visible = false;
	}
}
