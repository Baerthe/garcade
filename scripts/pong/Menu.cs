namespace pong;

using System;
using Godot;
public partial class Menu : Control
{
    public event Action OnGameStart;
    public event Action<PlayerType, PlayerType> OnControllersSelected;
    [Export] private Button _buttonPlay;
    [Export] private OptionButton _OptionPaddle1;
    [Export] private OptionButton _OptionPaddle2;
    public override void _Ready()
    {
        _buttonPlay.Pressed += OnButtonPlayPressed;
    }
    private void OnButtonPlayPressed()
    {
        GD.Print("Play button pressed");
        Visible = false;
        OnControllersSelected?.Invoke(
            (PlayerType)_OptionPaddle1.GetSelectedId(),
            (PlayerType)_OptionPaddle2.GetSelectedId()
        );
        GD.Print($"Controllers selected: P1 - {(PlayerType)_OptionPaddle1.GetSelectedId()}, P2 - {(PlayerType)_OptionPaddle2.GetSelectedId()}");
        OnGameStart?.Invoke();
    }
}