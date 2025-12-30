namespace pong;

using Godot;
using System;
/// <summary>
/// Main game controller for Pong game. Pong is a pretty simple game, so we will have Main being the controller and orchestrator of the game.
/// It will manage the paddles and the ball, and handle the game logic.
/// </summary>
public partial class Main : Node2D
{
    [ExportGroup("Debug")]
    [Export] public bool DebugMode {get; private set; } = false;
    [ExportGroup("References")]
    [Export] public Timer GameTimer {get; private set; }
    [Export] public Paddle PaddleP1
    {
        get => _paddle1 as Paddle;
        private set => _paddle1 = value;
    }
    [Export] public Paddle PaddleP2
    {
        get => _paddle2 as Paddle;
        private set => _paddle2 = value;
    }
    [Export] public Node2D StartPosP1 {get; private set; }
    [Export] public Node2D StartPosP2 {get; private set; }
    private IController _controller1;
    private IController _controller2;
    private IPaddle _paddle1;
    private IPaddle _paddle2;

    public override void _Ready()
    {
        GameStart();
        if (DebugMode)
        {
            GD.PrintS("Pong Main Ready - Debug Mode Enabled");
            _controller1 = new PaddlePlayer(PaddleP1);
            //_controller2 = new PaddlePlayer(PaddleP2);
        }
    }

    // -> Game State Functions
    private void GameOver()
    {

    }
    private void GameReset()
    {
        _paddle1.ResetPosition();
        _paddle2.ResetPosition();
    }
    private void GameStart()
    {
        _paddle1.InjectData(StartPosP1);
        _paddle2.InjectData(StartPosP2);
        GameReset();
    }
}
