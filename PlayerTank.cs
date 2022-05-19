using GXPEngine;

// TODO: Fix this mess! - see Assignment 2.2
public class PlayerTank : TankBase
{	
	public static TankBase Instance;

	public PlayerTank(float px, float py) : base(px, py,"assets/bodies/t34.png") 
	{
		this.SetOrigin(width /2, height / 2);

		
		_position.x = px;
		_position.y = py;
		_barrel = new Barrel ("assets/barrels/t34.png");
		AddChild (_barrel);
	    //barrel.x -= this.width  / 2;
	    //barrel.y -= this.height / 2;
		Instance = this as TankBase;
	} 

	void Turn() 
	{
		if (Input.GetKey(Key.LEFT))
		{
			rotation -= velocity.Length() / 2;
		}
		if (Input.GetKey(Key.RIGHT))
		{
			rotation += velocity.Length() / 2;
		}
	}

	void Move()
	{
		if (Input.GetKey(Key.UP))
		{
			// move in the direction we're currently facing
			float angleRadians = rotation * Mathf.PI / 180;
			if (velocity.Length() < _maxSpeed)
			{
				velocity += new Vec2(Mathf.Cos(angleRadians) * _acceleration,
					Mathf.Sin(angleRadians) * _acceleration);
			}
		}
		else if (Input.GetKey(Key.DOWN))
		{
			float angleRadians = rotation * Mathf.PI / 180;
			if (velocity.Length() < _maxSpeed)
			{
				velocity -= new Vec2(Mathf.Cos(angleRadians) * _acceleration,
					Mathf.Sin(angleRadians) * _acceleration);
			}
		}
		else 
		{
			velocity -= velocity * _friction;
		}

		// friction:
		velocity.x *= (1 - _friction);
		velocity.y *= (1 - _friction);
	}

	void Aim()
	{
	// Get the delta vector to mouse:
		float dx = Input.mouseX - _position.x;
		float dy = Input.mouseY - _position.y;
	// Get angle to mouse, convert from radians to degrees:
		float targetAngle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
		//float myrotation = 0f;
		//myrotation = targetAngle;
		//_barrel.rotation = targetAngle;// - rotation;

		if (targetAngle > _barrel.rotation + rotation + _barrelRotationSpeed)
		{
			_barrel.rotation+=_barrelRotationSpeed;
		} 
		else if (targetAngle<rotation - rotation - _barrelRotationSpeed) 
		{
			_barrel.rotation-=_barrelRotationSpeed;
		}
		
		
	}

	new public void Shoot() {
		if (Input.GetMouseButtonUp(0))
		{
			base.Shoot(this as TankBase);

		}
	}

	new public void Update() 
	{
		if (isDead == true) 
			MyGame.state = GameState.GameOver;
			
		Turn();
		Move();

		if (!isDead)
		{
			foreach (EnemyTank e in MyGame.enemyTanks)
			{
				if (!e.isDead)
				{
					if (HitTest(e))
					{
						this.velocity = new Vec2() - velocity;
					}
				}
			}
		}

		Shoot ();
		Aim();
		
		base.Update();
	}
}