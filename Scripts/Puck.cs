using Godot;
using System;

public partial class Puck : RigidBody2D
{
	[Export]
	private float _movementSpeed = 5;

	[Export(PropertyHint.Range, "0,180,0.5,radian")]
	private float _spreadAngle = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// randomly sellects to shoot to either player 1 or player 2
		var direction = (ShootDirection)(GD.Randi() % 2);

		Shoot(direction);
	}

	/// <summary>
	/// Called every physics update
	/// </summary>
	/// <param name="delta">The time between physics frames</param>
	//public override void _PhysicsProcess(double delta)
	//{
	//}

	/// <summary>
	/// cool
	/// </summary>
	private void Shoot(ShootDirection direction)
	{
		float halfRange = _spreadAngle * 0.5f;

		var angleOffset = (float)GD.RandRange(-halfRange, halfRange);

		ReadOnlySpan<Vector2> directionTable = stackalloc Vector2[2] {
			Vector2.Left,
			Vector2.Right
		};

		var dir = directionTable[(int)direction];

		LinearVelocity = dir.Rotated(angleOffset) * _movementSpeed;
	}

	private enum ShootDirection
	{
		PlayerOne,
		PlayerTwo
	}
}
