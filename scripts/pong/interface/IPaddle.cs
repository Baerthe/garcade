namespace pong;

using Godot;
public interface IPaddle
{
    byte Speed { get; }
    byte Size { get; }
    void ChangeSpeed(byte speed);
    void Move(bool directionUp);
    void ResetPosition();
    void Resize(byte size);
    void InjectData(Node2D startPos);
}