namespace pong;

using Godot;
public class PaddleAI : IController
{
    public Paddle Paddle { get; private set; }
    public PaddleAI(Paddle paddle)
    {
        Paddle = paddle;
    }
    public Direction GetInputDirection()
    {
        return Direction.None;
    }
    public void Update()
    {
        // AI logic to move the paddle would go here
    }
}