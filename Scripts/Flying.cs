using Godot;
using System;

public partial class Flying : StaticBody2D
{
	[Export]
	private float _speed = 800.0f;

	private AnimatedSprite2D _animatedSprite;
	private VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_visibleOnScreenNotifier2D = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(-_speed * (float)delta, 0);
	}
	 private void OnScreenExited() => QueueFree();
}
