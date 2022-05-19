using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

public class TankBase : Sprite
{
   // public BoxCollider collider;


    public Vec2 velocity;
    public Vec2 finalVelocity;

    //private fields:
    protected Vec2 _position;
    protected Barrel _barrel;
    protected float _maxSpeed = 1f;
    protected float _friction = 0.2f;
    protected float _acceleration = 0.2f;
    protected float _barrelRotationSpeed = 1.5f;

    //cooldown
    public float _coolDownTime = 2000f;
    protected float _coolDonTimr = 0f;

    //explode aftere death
    public float _deathTimr = 0f;
    public float _DeathTimee = 300f;


    protected bool _inCoolDown = false;    
    public bool isDead = false;
	// public fields & properties:
    //
    public int health = 100;

	public Vec2 position 
	{
		get 
		{
			return _position;
		}
	}
    public TankBase(float px, float py, string picture) : base(picture,true) 
    {
        this.SetOrigin(width / 2, height / 2);
        //collider = new BoxCollider(this);
    }

    public void Shoot( TankBase owner) 
    {
        if (!_inCoolDown)
        {
            Vec2 nozzleOffset = new Vec2(_barrel.x, _barrel.y) + new Vec2(_barrel.width / 1.5f, 0);
            nozzleOffset = nozzleOffset.rotateDeegres(_barrel.rotation);
            Vec2 spawnPoint = nozzleOffset;
            Bullet b = new Bullet(nozzleOffset, nozzleOffset.Normalized() * Bullet.speeed, owner);

           

            b.rotation = _barrel.rotation;
            AddChild(b);

          //   Vector2 worldSpaceBullet = TransformPoint(b.x, b.y);
          //   b.x= worldSpaceBullet.x;
          //   b.y = worldSpaceBullet.y;
          //  Game.main.AddChild(b);
            
            
            _inCoolDown = true;
            _coolDonTimr = 0f;
        }
    }

   
    void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}
    public void Update() 
    {
        _position += velocity;
        UpdateScreenPosition();
      //  Console.WriteLine(_coolDonTimr);
        _coolDonTimr += Time.deltaTime;
        if (_inCoolDown) 
        {
            
            if (_coolDonTimr > _coolDownTime)
            {
                _inCoolDown = false;
                _coolDonTimr=0;
            }
        }

        if (health <= 0) 
        {
            Explode();
        }

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
        Sprite s = new Sprite("assets/explodes/boom.png");
        s.SetOrigin(width , height );
        //s.SetOrigin(0, 0);
        //Console.WriteLine("boom");
        AddChild(s);
        isDead = true;
    }
}

