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
    private GpuParticles2D _trailParticles;
    private Vector2 _initialPosition;
    private VisibleOnScreenNotifier2D _visibleNotifier;
    private float _speedFactor;
    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _colorRect = GetNode<ColorRect>("ColorRect");
        _trailParticles = GetNode<GpuParticles2D>("GPUParticles2D");
        _visibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        _collisionShape.Shape = new CircleShape2D() { Radius = Size / 2 };
        _colorRect.Size = new Vector2(Size, Size);
        _colorRect.Position = new Vector2(-Size / 2, -Size / 2);
        _trailParticles.ProcessMaterial.Set("scale_max", Size);
        _trailParticles.ProcessMaterial.Set("scale_min", Size);
        _initialPosition = GlobalPosition;
        _visibleNotifier.Connect("screen_exited", new Callable(this, nameof(ResetBall)));
        AddToGroup("ball");
        ResetBall();
    }
    public override void _PhysicsProcess(double delta)
    {
        Velocity = Velocity.Clamp(new Vector2(-12000,-12000), new Vector2(12000, 12000));
        var collision = MoveAndCollide(Velocity * (float)delta * _speedFactor);
        if (collision != null)
        {
            var normal = collision.GetNormal();
            Velocity = Velocity.Bounce(normal);
            _speedFactor += _speedFactor * (Acceleration / 200.0f);
            _speedFactor = Mathf.Clamp(_speedFactor, 0f, 1.2f);
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
        _speedFactor = 0.05f;
        var flip = GD.Randf() < 0.5f ? -1 : 1;
        if (flip < 0)
            Velocity = new Vector2( -8000, GD.RandRange(-512, 512));
        else
            Velocity = new Vector2( 8000, GD.RandRange(-512, 512));
    }
}