namespace pong;

using System;
using Godot;
public partial class Menu : Control
{
    public event Action OnGameStart;
    [Export] private Button _buttonPlay;
    public override void _Ready()
    {
        _buttonPlay.Pressed += OnButtonPlayPressed;
    }
    private void OnButtonPlayPressed()
    {
        GD.Print("Play button pressed");
        Visible = false;
        OnGameStart?.Invoke();
    }
}