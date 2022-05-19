using GXPEngine;
using System;
// using static System.MathF;

public struct Vec2
{
	public float x;
	public float y;

	#region Vector operations
	//public Vec2() { x = 0; y = 0; }
	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	public static Vec2 operator +(Vec2 v1, Vec2 v2)
	{
		return new Vec2(v1.x + v2.x, v1.y + v2.y);
	}

	public static Vec2 operator -(Vec2 v1, Vec2 v2)
	{
		return new Vec2(v1.x - v2.x, v1.y - v2.y);
	}

	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}

	public float Length()
	{
		return Mathf.Sqrt(x * x + y * y);
	}

	//TODO:CheckME
	public static Vec2 Lerp(Vec2 v1, Vec2 v2, float p)
	{
		//if (p > 1) p = 1;
		//if (p < 0) p = 0;

		float x = v1.x + (v2.x - v1.x) * p;
		float y = v1.y + (v2.y - v1.y) * p;

		return new Vec2(x, y);
	}

	public void Normalize()
	{
		float len = this.Length();
		this.x = x / len;
		this.y = y / len;

		//	return this;
	}

	public Vec2 Normalized()
	{
		float len = this.Length();
		return new Vec2(x / len, y / len);
	}

	public static Vec2 operator *(Vec2 v1, float f1)
	{
		return new Vec2(v1.x * f1, v1.y * f1);
	}

	public static Vec2 operator *(float f1, Vec2 v1)
	{
		return v1 * f1;
	}

	public void SetXY(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	#endregion

	#region trigenometry
	///new
	public static float Deg2Rad(float degrees)
	{
		return (Mathf.PI * degrees) / 180;
	}

	public static float Rad2Deg(float rads)
	{
		return rads * (180 / Mathf.PI);
	}

	public static Vec2 GetUnitVectorDeg(float deg)
	{
		float rad = Deg2Rad(deg);
		float y = Mathf.Cos(rad);
		float x = Mathf.Sin(rad);

		return new Vec2(x, y);
	}

	public static Vec2 GetUnitVectorRad(float rad)
	{
		float y = Mathf.Cos(rad);
		float x = Mathf.Sin(rad);

		return new Vec2(x, y);
	}

	public static Vec2 RandomUnitVector()
	{
		Random r = new Random();
		float rand = (float)r.NextDouble();
		float angle = 360 * rand;
		return GetUnitVectorDeg(angle);
	}
	public void setAngleRadians(float angle)
	{
		Vec2 v2 = Vec2.GetUnitVectorRad(angle) * Length();
		this = v2;
	}

	public void setAngleDegrees(float angle)
	{
		Vec2 v2 = Vec2.GetUnitVectorDeg(angle) * Length();
		this = v2;
	}

	public float getAngleRadians()
	{
		Vec2 v = Normalized();
		return Mathf.Atan2(v.y, v.x);
	}

	public float getAngleDegres()
	{
		return Rad2Deg(getAngleRadians());
	}

	public Vec2 rotateRadians(float angle)
	{
		//angle = (2 * MathF.PI) - angle;
		float x = (this.x * Mathf.Cos(angle)) + (this.y * -Mathf.Sin(angle));
		float y = (this.x * Mathf.Sin(angle)) + (this.y * Mathf.Cos(angle));

		return new Vec2(x, y);
	}

	public Vec2 rotateDeegres(float angl)
	{
		float rad = Deg2Rad(angl);
		return rotateRadians(rad);
	}

	public void rotatreAroundRadians(Vec2 target, float angle)
	{
		Vec2 temp = new Vec2(this.x - target.x, this.y - target.y);
		temp = temp.rotateRadians(angle);
		x = target.x + temp.x;
		y = target.y + temp.y;
	}

	public void rotateAroundDegres(Vec2 target, float angle)
	{
		float rad = Deg2Rad(angle);
		rotatreAroundRadians(target, rad);
	}

	#endregion

	public void LookAt(Vec2 t) 
	{
		float targetAngle = Mathf.Atan2(t.y, t.x) * 180 / Mathf.PI;
		this.rotateDeegres(targetAngle);
	}
}

