using Godot;
using System;

public partial class Ground : Node2D
{
	[Export]
	private float _speed= -125.0f;

	private StaticBody2D _staticBody2D;
	private StaticBody2D _staticBody2DB;
	private int _textureWidth;
	
	public override void _Ready()
	{
		_staticBody2D=GetNode<StaticBody2D>("Ground_1");
		_staticBody2DB=GetNode<StaticBody2D>("Ground_2");
		_textureWidth=_staticBody2D.GetNode<Sprite2D>("GroundSprite").Texture.GetWidth();
		_staticBody2DB.GlobalPosition=_staticBody2DB.GlobalPosition with {X = _staticBody2D.GlobalPosition.X+_textureWidth};
	}

	public override void _Process(double delta)
	
	{
		_staticBody2D.GlobalPosition += new Vector2(_speed * (float)delta, 0);
		_staticBody2DB.GlobalPosition += new Vector2(_speed * (float)delta, 0);

		if (_staticBody2D.GlobalPosition.X < -_textureWidth)
		{
			_staticBody2D.GlobalPosition = _staticBody2DB.GlobalPosition + new Vector2(_textureWidth, 0);
		}
		if (_staticBody2DB.GlobalPosition.X < -+_textureWidth)
		{
			_staticBody2DB.GlobalPosition = _staticBody2D.GlobalPosition + new Vector2(_textureWidth, 0);
		}
	}
}
