namespace pong;

using Godot;
[GlobalClass]
public sealed partial class Ball : CharacterBody2D
{
    [ExportGroup("Properties")]
    [Export(PropertyHint.Range, "1,100")] public byte Acceleration { get; private set; } = 25;
    [Export(PropertyHint.Range, "1,100")] public byte Size { get; private set; } = 8;
    private CollisionShape2D _collisionShape;
    private ColorRect _colorRect;
    private Vector2 _initialPosition;
    private float _speedFactor;
    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _colorRect = GetNode<ColorRect>("ColorRect");
        _collisionShape.Shape = new CircleShape2D() { Radius = Size / 2 };
        _colorRect.Size = new Vector2(Size, Size);
        _colorRect.Position = new Vector2(-Size / 2, -Size / 2);
        _initialPosition = GlobalPosition;
        _speedFactor = 1.0f / Acceleration;
        // Launch the ball in a random direction for testing
        Velocity = new Vector2(-8000, 0);
    }
    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(Velocity * (float)delta * _speedFactor);
        if (collision != null)
        {
            var normal = collision.GetNormal();
            Velocity = Velocity.Bounce(normal);
            _speedFactor += _speedFactor * (1.0f / Acceleration);
            if (Velocity.Y > -128 && Velocity.Y < 128)
            {
                Velocity = new Vector2(Velocity.X, GD.RandRange(-512, 512));
            }
        }
    }
}