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
    private VisibleOnScreenNotifier2D _visibleNotifier;
    private float _speedFactor;
    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _colorRect = GetNode<ColorRect>("ColorRect");
        _visibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        _collisionShape.Shape = new CircleShape2D() { Radius = Size / 2 };
        _colorRect.Size = new Vector2(Size, Size);
        _colorRect.Position = new Vector2(-Size / 2, -Size / 2);
        _initialPosition = GlobalPosition;
        _visibleNotifier.Connect("screen_exited", new Callable(this, nameof(ResetBall)));
        ResetBall();
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
    private void ResetBall()
    {
        Velocity = Vector2.Zero;
        GlobalPosition = _initialPosition;
        _speedFactor = 1.0f / Acceleration;
        var flip = GD.Randf() < 0.5f ? -1 : 1;
        if (flip < 0)
            Velocity = new Vector2( -8000, GD.RandRange(-512, 512));
        else
            Velocity = new Vector2( 8000, GD.RandRange(-512, 512));
    }
}