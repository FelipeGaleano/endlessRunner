using Godot;
using System;

public partial class ObstacleSpawner : Node
{
	[Export]
	private float _decreaseSpawnTimeOnDifficultyIncrease = 0.2f;

	[Export]
	private float _chanceToSpawnFly = 0.1f;
	private PackedScene _flyScene=GD.Load<PackedScene>("res://Scenes/flying.tscn");
	private PackedScene _prayScene=GD.Load<PackedScene>("res://Scenes/pray.tscn");
	RectangleShape2D[]collisionShapes=new RectangleShape2D[6];
	Texture2D[] texture2Ds= new Texture2D[2];
	private StaticBody2D _staticBody2D;

	 private StaticBody2D _ground1;
	private StaticBody2D _ground2;

	private Timer _obstacleSpawnTimer;
	private Node2D _spawnPoint;
	private Node main;

	 private float[] _obstacleSpawnTimeRange = { 1f, 2f };
	public override void _Ready()
	{
		main = GetNode<Node>("/root/main");
		_spawnPoint = GetNode<Node2D>("SpawnPoint");
		_obstacleSpawnTimer = GetNode<Timer>("Timer");
		_ground1 = GetParent().GetNode<StaticBody2D>("Ground_1");
		_ground2 = GetParent().GetNode<StaticBody2D>("Ground_2");

		collisionShapes[0] = GD.Load<RectangleShape2D>("res://CollisionShape/prayCollsion.tres");
		collisionShapes[1] = GD.Load<RectangleShape2D>("res://CollisionShape/tombCollsion.tres");

		texture2Ds[0] = GD.Load<Texture2D>("res://Assets/Obstacles/Pray.png");
		texture2Ds[1] = GD.Load<Texture2D>("res://Assets/Obstacles/Tomb.png");

		 _obstacleSpawnTimer.Timeout += SpawnObstacle;


	}
  private void SpawnObstacle()
	{
		Random random = new Random();
		float randomValue = (float)random.NextDouble();

		if (randomValue <= _chanceToSpawnFly)
		{
			SpawnFly();
		}
		else
		{

			SpawnPray();
		}

		_obstacleSpawnTimer.Stop();
		_obstacleSpawnTimer.WaitTime = (float)GD.RandRange(_obstacleSpawnTimeRange[0], _obstacleSpawnTimeRange[1]);
		_obstacleSpawnTimer.Start();
	}

	private void SpawnFly()
	{
		var fly = _flyScene.Instantiate<Flying>();

		main.AddChild(fly);
		var positionY = GetViewport().GetVisibleRect().Size.Y - (float)GD.RandRange(128.0, 192.0);
		fly.Position = new Vector2(_spawnPoint.Position.X, positionY);
	}


	 private void SpawnPray()
	{
		var Pray = _prayScene.Instantiate<Pray>();

		var parentGround = _ground1.GlobalPosition.X > _ground2.GlobalPosition.X ? _ground1 : _ground2;

		parentGround.AddChild(Pray);


		Random random = new Random();
		int randomIndex = random.Next(0, 6);
		Pray.Sprite2D.Texture = texture2Ds[randomIndex];
		Pray.CollisionShape2D.Shape = collisionShapes[randomIndex];

		Pray.GlobalPosition = new Vector2(_spawnPoint.Position.X, parentGround.GlobalPosition.Y);

	}
	 public void IncreaseDifficulty()
	{
		_chanceToSpawnFly += 0.05f;

		if (_obstacleSpawnTimeRange[0] > 0.5f)
		{
			_obstacleSpawnTimeRange[0] -= _decreaseSpawnTimeOnDifficultyIncrease;
			_obstacleSpawnTimeRange[1] -= _decreaseSpawnTimeOnDifficultyIncrease;
		}

	}
}
