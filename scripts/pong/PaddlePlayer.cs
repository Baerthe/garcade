namespace pong;

using Godot;
public class PaddlePlayer : IController
{
    private string _inputPrefix => _isPlayer1 ? "p1_" : "p2_";
    public Paddle Paddle { get; private set; }
    public Score Score { get; private set; }
    private Ball _ball;
    private bool _isPlayer1;
    public PaddlePlayer(Paddle paddle, Score score, bool isPlayer1)
    {
        _isPlayer1 = isPlayer1;
        Paddle = paddle;
        Score = score;
        _ball = Paddle.GetTree().GetNodesInGroup("ball")[0] as Ball;
        _ball.OnOutOfBounds += (bool isLeftSide) =>
        {
            if ((isLeftSide && _isPlayer1) || (!isLeftSide && !_isPlayer1))
                Score.AddPoint();
        };
        GD.Print($"PaddlePlayer created for {(_isPlayer1 ? "Player 1" : "Player 2")}");
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