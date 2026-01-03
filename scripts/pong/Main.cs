namespace pong;

using Godot;
using System;
/// <summary>
/// Main game controller for Pong game. Pong is a pretty simple game, so we will have Main being the controller and orchestrator of the game.
/// It will manage the paddles and the ball, and handle the game logic.
/// </summary>
public partial class Main : Node2D
{
    [ExportGroup("References")]
    [Export] public Menu Menu { get; private set; }
    [Export] public Timer GameTimer {get; private set; }
    [Export] public Paddle PaddleP1 { get; private set; }
    [Export] public Paddle PaddleP2 { get; private set; }
    [Export] public Ball Ball { get; private set; }
    [ExportGroup("HUD Properties")]
    [Export] public Label ScoreP1Label { get; private set; }
    [Export] public Label ScoreP2Label { get; private set; }
    private bool _isGameOver = true;
    private IController _controller1;
    private IController _controller2;
    private Score _scoreP1;
    private Score _scoreP2;

    public override void _Ready()
    {
        _scoreP1 = new Score(ScoreP1Label);
        _scoreP2 = new Score(ScoreP2Label);
        Menu.OnGameStart += GameStart;
        Menu.OnControllersSelected += OnControllersSelected;
    }
    public override void _Process(double delta)
    {
        if (_isGameOver)
            return;
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

        Ball.ToggleEnable();
        _isGameOver = false;
    }
    private void OnControllersSelected(PlayerType player1Type, PlayerType player2Type)
    {
        _controller1 = player1Type switch
        {
            PlayerType.Player1 => new PaddlePlayer(PaddleP1, _scoreP1, true),
            PlayerType.Player2 => new PaddlePlayer(PaddleP1, _scoreP2, false),
            PlayerType.AI => new PaddleAI(PaddleP1, _scoreP1, true),
            _ => throw new ArgumentOutOfRangeException(nameof(player1Type), "Invalid player type")
        };

        _controller2 = player2Type switch
        {
            PlayerType.Player1 => new PaddlePlayer(PaddleP2, _scoreP1, true),
            PlayerType.Player2 => new PaddlePlayer(PaddleP2, _scoreP2, false),
            PlayerType.AI => new PaddleAI(PaddleP2, _scoreP2, false),
            _ => throw new ArgumentOutOfRangeException(nameof(player2Type), "Invalid player type")
        };
    }
}