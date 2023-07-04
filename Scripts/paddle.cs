using Godot;
using System;

public partial class paddle : AnimatableBody2D
{
	[Export]
	private SelectedActionListener _selectedActionListener;

	[Export]
	private float _movementSpeed = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{

	//}

	/// <summary>
	/// Called every physics update
	/// </summary>
	/// <param name="delta">The time between physics frames</param>
	public override void _PhysicsProcess(double delta)
	{
		//var currentPosition = Position;

		float direction = _selectedActionListener switch
		{
			SelectedActionListener.PlayerOne => Input.GetAxis(InputActionName.GameMoveUpP1, InputActionName.GameMoveDownP1),
			SelectedActionListener.PlayerTwo => Input.GetAxis(InputActionName.GameMoveUpP2, InputActionName.GameMoveDownP2),
			_ => throw new NotImplementedException(),
		};

		Vector2 velocity = new Vector2(0, (float)(direction * _movementSpeed * delta));

		MoveAndCollide(velocity);
	}
}

public enum SelectedActionListener
{
	PlayerOne,
	PlayerTwo
}

public static class InputActionName
{
	public const string GameMoveUpP1 = "game_move_up_p1";
	public const string GameMoveDownP1 = "game_move_down_p1";
	public const string GameMoveUpP2 = "game_move_up_p2";
	public const string GameMoveDownP2 = "game_move_down_p2";
}
