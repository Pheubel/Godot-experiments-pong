using Godot;
using System;

public partial class Puck : RigidBody2D
{
	[Export]
	private float _movementSpeed = 5;

	[Export(PropertyHint.Range, "0,180,1,radians")]
	private float _spreadAngle = 0;


	private AudioStreamPlayer _goalAudioPlayer;
	private AudioStreamPlayer _bounceAudioPlayer;

	private Timer _timer;

	public ShootDirection StartDirection { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = this.GetNode<Timer>("Timer");
		_goalAudioPlayer = this.GetNode<AudioStreamPlayer>("GoalSound");
		_bounceAudioPlayer = this.GetNode<AudioStreamPlayer>("BounceSound");

		// randomly sellects to shoot to either player 1 or player 2
		StartDirection = (ShootDirection)(GD.Randi() % 2);

		_timer.Timeout += () =>
		{
			Shoot(StartDirection);
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (var colBody in this.GetCollidingBodies())
		{
			if (colBody is not Area2D)
			{
				_bounceAudioPlayer.Play();
			}
		}
	}

	/// <summary>
	/// Shoots the puck into the direction of the given player.
	/// </summary>
	/// <param name="direction"> The player the puck should start moving to.</param>
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

	public void OnGoalEntered(Node goal, long player)
	{
		_goalAudioPlayer.Play();

		// set the start direction to the scoring player
		StartDirection = (ShootDirection)(~player & 1);

		LinearVelocity = Vector2.Zero;

		// There seems to be a bug where setting the position directly causes it to not overwrite the physics
		var old = GlobalTransform;
		old.Origin = Vector2.Zero;

		GlobalTransform = old;
		_timer.Start();
	}

	public enum ShootDirection
	{
		PlayerOne,
		PlayerTwo
	}
}
