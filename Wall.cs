using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class Wall:Sprite
    {
    //BoxCollider collider;
        public Wall(float px, float py, string pic) : base(pic, true) 
        {
            SetOrigin(width/2,height/2);
            x = px;
            y = py;
        }

    void Update() 
    {
        if (HitTest(PlayerTank.Instance))
        {
            PlayerTank.Instance.velocity = new Vec2(0,0) -PlayerTank.Instance.velocity;
        }
    }
}

