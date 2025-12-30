namespace pong;

using Godot;
public sealed partial class Paddle : Node2D, IPaddle
{
    public byte Size { get; private set; } = 3;
    private Vector2 _initialPosition;
    public void Move(float delta, bool directionUp)
    {
        
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