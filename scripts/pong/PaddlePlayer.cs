namespace pong;

using Godot;
public class PaddlePlayer : IController
{
    private bool _isPlayer1;
    private string _inputPrefix => _isPlayer1 ? "p1_" : "p2_";
    public Paddle Paddle { get; private set; }
    public PaddlePlayer(Paddle paddle, bool isPlayer1 = true)
    {
        _isPlayer1 = isPlayer1;
        Paddle = paddle;
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
    public void Update()
    {
        if (GetInputDirection() == Direction.None)
            return;
        Paddle.Move(GetInputDirection());
    }
}