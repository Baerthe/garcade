namespace pong;

using Godot;
public partial class PaddlePlayer : IController
{
    public Paddle Paddle
    {
        get => _paddle;
        set
        {
            if (value == null || _paddle != null)
                return;
            _paddle = value;
        }
    }
    private Paddle _paddle;
    public PaddlePlayer(Paddle paddle)
    {
        Paddle = paddle;
    }
    public float GetInputDirection()
    {
        float direction = 0f;
        if (Input.IsActionPressed("p1_move_up") || Input.IsActionPressed("p2_move_up"))
            direction -= 1f;
        if (Input.IsActionPressed("p1_move_down") || Input.IsActionPressed("p2_move_down"))
            direction += 1f;
        return direction;
    }
}