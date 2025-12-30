namespace pong;

using Godot;
public interface IController
{
    Paddle Paddle {get; set;}
    float GetInputDirection();
}