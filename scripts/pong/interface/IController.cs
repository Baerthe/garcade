namespace pong;

using Godot;
public interface IController
{
    Paddle Paddle {get; }
    Direction GetInputDirection();
    void Update();
}