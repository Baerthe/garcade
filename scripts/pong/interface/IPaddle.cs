namespace pong;

using Godot;
public interface IPaddle
{
    byte Size { get; }
    void Move(float delta, bool directionUp);
    void ResetPosition();
    void Resize(byte size);
    void InjectData(Node2D startPos);
}