using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public enum GameState { InGame, GameOver }
public class MyGame : Game
{	
	public static List<Vec2> spawnPoints;
	public static List<Vec2> destinationPoints;
	public static List<EnemyTank> enemyTanks;
	public static List<Wall> walls;

	public static GameState state = GameState.InGame;

	public static int score=0;
	public bool gameOver = false;

	EasyDraw _score;
	EasyDraw _text;
	static void Main() 
	{

		new MyGame().Start();
	}

	public void InitGameLogic() 
	{			
			spawnPoints = new List<Vec2>();
			destinationPoints = new List<Vec2>();
			enemyTanks = new List<EnemyTank>();
			walls = new List<Wall>();


			spawnPoints.Add(new Vec2(-100, 300));
			destinationPoints.Add(new Vec2(900, 300));


			spawnPoints.Add(new Vec2(-100, 300));
			destinationPoints.Add(new Vec2(900, 300));

			spawnPoints.Add(new Vec2(-100, 400));
			destinationPoints.Add(new Vec2(900, 300));

			spawnPoints.Add(new Vec2(-100, 100));
			destinationPoints.Add(new Vec2(900, 100));

			spawnPoints.Add(new Vec2(-100, 300));
			destinationPoints.Add(new Vec2(900, 100));


			spawnPoints.Add(new Vec2(900,100));
			destinationPoints.Add(new Vec2(-100, 300));

			spawnPoints.Add(new Vec2(900, 300));
			destinationPoints.Add(new Vec2(-100, 400));

			spawnPoints.Add(new Vec2(900, 400));
			destinationPoints.Add(new Vec2(-100, 100));

			spawnPoints.Add(new Vec2(900, 300));
			destinationPoints.Add(new Vec2(-100, 300));
	}

	public MyGame () : base(800, 600, false,false)
	{
			// background:
			AddChild(new Sprite("assets/desert.png"));

			InitGameLogic();

			for (int i = 0; i < 9; i++)
			{
				Wall w = new Wall(100 + 75 * i, 50, "assets/wall.png");
				walls.Add(w);
				AddChild(w);
			}

			for (int i = 0; i < 9; i++)
			{
				Wall w = new Wall(100 + 75 * i, 550, "assets/wall.png");
				walls.Add(w);
				AddChild(w);
			}

			// tank:
			AddChild(new PlayerTank(width / 2, height / 2));



			_score = new EasyDraw(500, 50);
			_score.TextAlign(CenterMode.Min, CenterMode.Min);
			AddChild(_score);

		_text = new EasyDraw(500, 50);
		_text.TextAlign(CenterMode.Min, CenterMode.Min);
		//AddChild(_score);
	}

	void Update() 
	{
		if (state == GameState.InGame)
		{
			while (enemyTanks.Count < 1)
			{
				int route = new Random().Next(9);
				EnemyTank t = new EnemyTank(spawnPoints[route], destinationPoints[route]);
				if (spawnPoints[route].x < 0) 
				{
					t.setLeft(true);
                }
                else
				{
					t.setLeft(false);
				}
				enemyTanks.Add(t);
				AddChild(t);
			}

			_score.Clear(Color.Transparent);
			_score.Text("points: " + score, 0, 0);
		}

		if (state == GameState.GameOver && !gameOver)
		{
			//_text.Clear(Color.Transparent);
			//_text.Text("GAME OVER", 0, 0);
			AddChild(new Sprite("assets/end.png"));
			AddChild(_text);

			_text.Clear(Color.Transparent);
			_text.Text("GAME OVER. your record: " + score, 0, 0);

		}
	}

	
}