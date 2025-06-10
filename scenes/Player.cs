using Godot;

namespace first2DProject.scenes;

public partial class Player : Area2D
{
    [Export] public int Speed { get; set; } = 400;
    private Vector2 ScreenSize { get; set; }
    
    [Signal]
    public delegate void HitEventHandler();

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(double delta)
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
        var velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }
        
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed; 
            
            Position += velocity * (float)delta;
            Position = Position.Clamp(Vector2.Zero, ScreenSize);

            if (velocity.X == 0) return;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "run";
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = velocity.X < 0;
        }
        else
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "idle";
        }
    }
}