using Godot;
using System;

public partial class paddle : AnimatableBody2D
{
	[Export]
	private SelectedActionListener _selectedActionListener;

	[Export]
	private float _movementSpeed = 5;

	/// <summary>
	/// Called every physics update
	/// </summary>
	/// <param name="delta">The time between physics frames</param>
	public override void _PhysicsProcess(double delta)
	{
		var currentPosition = Position;

		float direction = _selectedActionListener switch
		{
			SelectedActionListener.PlayerOne => Input.GetAxis(InputActionName.GameMoveUpP1, InputActionName.GameMoveDownP1),
			SelectedActionListener.PlayerTwo => Input.GetAxis(InputActionName.GameMoveUpP2, InputActionName.GameMoveDownP2),
			_ => throw new NotImplementedException(),
		};

		Vector2 velocity = new Vector2(0, (float)(direction * _movementSpeed * delta));

		var collission = new KinematicCollision2D();

		if (TestMove(Transform, velocity, collission) && collission.GetCollider() is StaticBody2D otherBody)
		{
			// TODO: solve to get a tight fit with the walls
		}
		else
		{
			Position += velocity;
		}
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
