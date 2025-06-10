using Godot;

namespace first2DProject.scenes;

public partial class Player : Area2D
{
	[Export] public int Speed { get; set; } = 400;
	private int ScreenSize { get; set; }
}