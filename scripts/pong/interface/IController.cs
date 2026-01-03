namespace pong;

using Godot;
public interface IController
{
    Paddle Paddle { get; }
    Score Score { get; }
    Direction GetInputDirection();
    public void Update()
    {
        if (GetInputDirection() == Direction.None)
            return;
        Paddle.Move(GetInputDirection());
    }
}