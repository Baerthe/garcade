namespace pong;

using Godot;
public sealed partial class Paddle : CharacterBody2D
{
    public byte Speed { get; private set; } = 200;
    public byte Size { get; private set; } = 3;
    private CollisionShape2D _collisionShape;
    private Vector2 _initialPosition;
    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    }
    public override void _PhysicsProcess(double delta)
    {
        Velocity = Velocity * 0.8f;
        MoveAndSlide();
    }
    public void ChangeSpeed(byte speed)
    {
        if (speed < 10)
            return;
        Speed = speed;
    }
    public void Move(Direction direction)
    {
        Velocity = direction == Direction.Up ? new Vector2(0, -Speed) : new Vector2(0, Speed);
    }
    public void Resize(byte size)
    {
        if (size < 1 || size > 100)
            return;
        Size = size;
    }
    public void ResetPosition()
    {
        GlobalPosition = _initialPosition;
    }
    public void InjectData(Node2D startPos)
    {
        GlobalPosition = startPos.GlobalPosition;
        _initialPosition = GlobalPosition;
    }
}