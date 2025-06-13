using Godot;

namespace first2DProject.scenes.characters;

public partial class Player : CharacterBody2D
{
    [Export] public int Speed { get; set; } = 400;
    [Export] public int JumpForce { get; set; } = -500;
    [Export] public int Gravity { get; set; } = ProjectSettings.GetSetting("physics/2d/default_gravity").As<int>();
    [Export] public float Acceleration { get; set; } = 0.25f;
    [Export] public float Friction { get; set; } = 0.15f;

    private Vector2 _velocity;
    private AnimatedSprite2D _animatedSprite;

    [Signal]
    public delegate void HitEventHandler();

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            _velocity.Y += Gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            _velocity.Y = JumpForce;
        }

        var direction = Input.GetAxis("move_left", "move_right");

        var targetSpeed = direction * Speed;


        _velocity.X = direction != 0
            ? Mathf.Lerp(_velocity.X, targetSpeed, Acceleration)
            : Mathf.Lerp(_velocity.X, 0, Friction);

        Velocity = _velocity;
        MoveAndSlide();
        _velocity = Velocity;

        UpdateAnimation(direction);
    }

    private void UpdateAnimation(float direction)
    {
        if (IsOnFloor())
        {
            if (direction != 0)
            {
                _animatedSprite.Play("run");
                _animatedSprite.FlipH = direction < 0;
            }
            else
            {
                _animatedSprite.Play("idle");
            }
        }
        else
        {
            _animatedSprite.Play("jump");
        }
    }
}