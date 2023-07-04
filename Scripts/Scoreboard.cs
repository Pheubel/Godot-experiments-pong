using Godot;
using System;

public partial class Scoreboard : Control
{
	[Export]
	private Label _playerOneScoreLabel;

	[Export]
	private Label _playerTwoScoreLabel;

	int _playerOneScore = 0;
	int _playerTwoScore = 0;

	public void OnGoalEntered(Node _, long player)
	{
		if (player == 0)
		{
			_playerTwoScore++;
			_playerTwoScoreLabel.Text = _playerTwoScore.ToString();
		}
		else
		{
			_playerOneScore++;
			_playerOneScoreLabel.Text = _playerOneScore.ToString();
		}
	}
}
