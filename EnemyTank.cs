using GXPEngine;

// TODO: Fix this mess! - see Assignment 2.2
public class EnemyTank : TankBase
{
	bool isLeft;
	public EnemyTank(Vec2 source, Vec2 dest) : base(source.x, source.y,"assets/bodies/t34.png")
	{
		this.SetOrigin(width / 2, height / 2);

		_position.x = source.x;
		_position.y = source.y;
		_barrel = new Barrel("assets/barrels/t34.png");
		AddChild(_barrel);

		rotation = (dest - source).getAngleDegres();

		_inCoolDown = true;
		_coolDownTime = 3000f;
	}

	void TurlLeft() 
	{
		rotation -= velocity.Length() / 2;
	}

	void TurnRight() 
	{
		rotation += velocity.Length() / 2;
	}

	void MoveForward()
	{
		// move in the direction we're currently facing
		float angleRadians = rotation * Mathf.PI / 180;
		if (velocity.Length() < _maxSpeed)
		{
			velocity += new Vec2(Mathf.Cos(angleRadians) * _acceleration, Mathf.Sin(angleRadians) * _acceleration);
		}
		// friction:
		velocity.x *= (1 - _friction);
		velocity.y *= (1 - _friction);
	}

	void MoveBackward() 
	{
		float angleRadians = rotation * Mathf.PI / 180;
		if (velocity.Length() < _maxSpeed)
		{
			velocity -= new Vec2(Mathf.Cos(angleRadians) * _acceleration,
				Mathf.Sin(angleRadians) * _acceleration);
		}
		// friction:
		velocity.x *= (1 - _friction);
		velocity.y *= (1 - _friction);
	}

	public void setLeft(bool left= true) 
	{
		isLeft = left;
	}
	void Aim()
	{

		float dx=PlayerTank.Instance.position.x - _position.x;
		float dy = PlayerTank.Instance.position.y - _position.y;

		// Get angle to mouse, convert from radians to degrees:
		float targetAngle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;

		if (isLeft)
			_barrel.rotation = targetAngle;
		else
			_barrel.rotation = targetAngle+180;

	}

	void Shoot()
	{
		if (isDead) return;

		if (position.x >0 && position.x < Game.main.width && position.y >0 && position.y < Game.main.height)
		{
			//	AddChild (new Bullet (new Vec2 (0, 0), new Vec2 (1, 0)));
			base.Shoot(this as TankBase);	
			

		}
	}

	public void Update()
	{
		//Controls ();		
		//Turn();
		//Move();

		//Basic Euler integration:
		if (! isDead && !PlayerTank.Instance.isDead)
			if( HitTest(PlayerTank.Instance))
			{
				//only for forward
				this.velocity = new Vec2()-velocity;
			}
        if (isDead)
        {
			MyGame.enemyTanks.Remove(this);
			MyGame.score += 10;
        }
		base.Update();
		MoveForward();
		Shoot();
		Aim();
	}
}