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
    [Export] public Paddle PaddleP1 { get; private set; }
    [Export] public Paddle PaddleP2 { get; private set; }
    [Export] public Ball Ball { get; private set; }
    [ExportGroup("HUD Properties")]
    [Export] public Label ScoreP1Label { get; private set; }
    [Export] public Label ScoreP2Label { get; private set; }
    private IController _controller1;
    private IController _controller2;
    private Score _scoreP1;
    private Score _scoreP2;

    public override void _Ready()
    {
        _scoreP1 = new Score(ScoreP1Label);
        _scoreP2 = new Score(ScoreP2Label);
        if (DebugMode)
        {
            GD.PrintS("Pong Main Ready - Debug Mode Enabled");
            _controller1 = new PaddleAI(PaddleP1, _scoreP1, true);
            _controller2 = new PaddleAI(PaddleP2, _scoreP2, false);
        }
    }
    public override void _Process(double delta)
    {
        _controller1.Update();
        _controller2.Update();
    }

    // -> Game State Functions
    private void GameOver()
    {

    }
    private void GameReset()
    {
        PaddleP1.ResetPosition();
        PaddleP2.ResetPosition();
    }
    private void GameStart()
    {
        GameReset();
    }
}
