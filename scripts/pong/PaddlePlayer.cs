namespace pong;

using Godot;
public class PaddlePlayer : IController
{
    private bool _isPlayer1;
    private string _inputPrefix => _isPlayer1 ? "p1_" : "p2_";
    public Paddle Paddle
    {
        get => _paddle;
        set
        {
            if (value == null && _paddle == null)
                return;
            _paddle = value;
        }
    }
    private Paddle _paddle;
    public PaddlePlayer(Paddle paddle, bool isPlayer1 = true)
    {
        _isPlayer1 = isPlayer1;
        Paddle = paddle;
    }
    public int GetInputDirection()
    {
        int direction = 0;
        if (Input.IsActionPressed($"{_inputPrefix}move_up"))
            direction = -1;
        if (Input.IsActionPressed($"{_inputPrefix}move_down"))
            direction = 1;
        return direction;
    }
    public void Update()
    {
        if (GetInputDirection() == 0)
            return;
        Paddle.Move(GetInputDirection() < 0);
    }
}