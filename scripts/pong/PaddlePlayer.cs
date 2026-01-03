namespace pong;

using Godot;
public class PaddlePlayer : IController
{
    private string _inputPrefix;
    public Paddle Paddle { get; private set; }
    public Score Score { get; private set; }
    private Ball _ball;
    private bool _isLeftSide;
    public PaddlePlayer(Paddle paddle, Score score, bool isLeftSide, bool isPlayer1)
    {
        _isLeftSide = isLeftSide;
        _inputPrefix = isPlayer1 ? "p1_" : "p2_";
        Paddle = paddle;
        Score = score;
        _ball = Paddle.GetTree().GetNodesInGroup("ball")[0] as Ball;
        _ball.OnOutOfBounds += (bool isLeftSide) =>
        {
            if ((isLeftSide && _isLeftSide) || (!isLeftSide && !_isLeftSide))
                Score.AddPoint();
        };
        GD.Print($"PaddlePlayer created for {(_isLeftSide ? "Player 1" : "Player 2")}");
    }
    public Direction GetInputDirection()
    {
        Direction direction = Direction.None;
        if (Input.IsActionPressed($"{_inputPrefix}move_up"))
            direction = Direction.Up;
        if (Input.IsActionPressed($"{_inputPrefix}move_down"))
            direction = Direction.Down;
        return direction;
    }
}