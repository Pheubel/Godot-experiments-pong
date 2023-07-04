using Godot;
using System;

public partial class Puck : RigidBody2D
{
	[Export]
	private float _movementSpeed = 5;

	[Export(PropertyHint.Range, "0,180,0.5,radian")]
	private float _spreadAngle = 0;

	private Timer _timer;

	public ShootDirection StartDirection { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = this.GetNode<Timer>("Timer");

		// randomly sellects to shoot to either player 1 or player 2
		StartDirection = (ShootDirection)(GD.Randi() % 2);

		_timer.Timeout += () =>
		{
			Shoot(StartDirection);
		};
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

	public enum ShootDirection
	{
		PlayerOne,
		PlayerTwo
	}
}
