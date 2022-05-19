using GXPEngine;
using GXPEngine.Core;
using System;

class Bullet : Sprite
{
	public BoxCollider myCollider;
	public static float speeed = 20.0f;
	public int damage = 100;
	public bool rikosheted = false;
	//explode aftere death

	//for explode before destroy
	public float _deathTimr = 0f;
	public float _DeathTimee = 100f;
	public bool isDead = false;

	public TankBase owner;

	// public fields & properties:
	public Vec2 position
	{
		get
		{
			return _position;
		}
	}
	public Vec2 velocity;

	// private fields:
	Vec2 _position;

	public Bullet(Vec2 pPosition, Vec2 pVelocity, TankBase owner) : base("assets/bullet.png")
	{
		_position = pPosition;
		velocity = pVelocity;
		myCollider = new BoxCollider(this);
		this.owner = owner;
	}

	void UpdateScreenPosition()
	{
		x = _position.x;
		y = _position.y;
	}

	public void Update()
	{
		for (int i = 0; i < MyGame.enemyTanks.Count; i++)
		{
			TankBase e = MyGame.enemyTanks[i];
			if (e == owner)
				continue;

			if (e.isDead)
				continue;

			else if (HitTest(e))
			{
				e.health -= damage;
				Destroy();

			}

		}
		if (PlayerTank.Instance.isDead)
		{

		}
		else if (HitTest(PlayerTank.Instance))
		{
			//Console.WriteLine(PlayerTank.Instance);
			//Console.WriteLine(owner);
			if (PlayerTank.Instance != owner)
			{
				PlayerTank.Instance.health -= damage;
				Destroy();
			}
		}

		for (int i = 0; i < MyGame.walls.Count; i++)
		{
			Wall e = MyGame.walls[i];
			if (HitTest(e))
			{
				if (!rikosheted)
				{
					this.velocity = new Vec2(velocity.x, -velocity.y);
					this.rotation = this.rotation * 2;
					rikosheted = true;
				}
				else
				{
					Explode();
				}

			}
		}

		if (x < -1000 || y < -1000 || x > 1000 || y > 1000)
			Destroy();

		_position += velocity;
		UpdateScreenPosition();


		if (isDead)
		{
			_deathTimr += Time.deltaTime;
			if (_deathTimr >= _DeathTimee)
			{
				//collider=null;
				Destroy();
			}
		}
	}

	public void Explode()
	{
		this.velocity = new Vec2(0f, 0f);
		Sprite s = new Sprite("assets/explodes/boom2.png");
		s.SetOrigin(width, height);
		//s.SetOrigin(0, 0);
		//Console.WriteLine("boom");
		AddChild(s);
		isDead = true;
	}
}
