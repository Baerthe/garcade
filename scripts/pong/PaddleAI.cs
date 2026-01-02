namespace pong;

using Godot;
public class PaddleAI : IController
{
    public Paddle Paddle { get; private set; }
    private Ball _ball;
    private Direction _lastDirection = Direction.None;
    public PaddleAI(Paddle paddle)
    {
        Paddle = paddle;
        _ball = Paddle.GetTree().GetNodesInGroup("ball")[0] as Ball;
    }
    public Direction GetInputDirection()
    {
        int flip = GD.RandRange(0, 100);
        var detectionZone = (Paddle.Size + GD.RandRange(-18, 12)) / 4;
        Direction direction = Direction.None;
        if (flip < 20)
        {
            if (_ball.GlobalPosition.Y < Paddle.GlobalPosition.Y - detectionZone)
                direction = Direction.Up;
            else if (_ball.GlobalPosition.Y > Paddle.GlobalPosition.Y + detectionZone)
                direction = Direction.Down;
        }
        switch (_lastDirection)
        {
            case Direction.Up:
                if (flip < 8)
                     break;
                direction = Direction.Up;
                break;
            case Direction.Down:
                if (flip < 8)
                    break;
                direction = Direction.Down;
                break;
            case Direction.None:
                break;
        }
        _lastDirection = direction;
        return direction;
    }
}