using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Dirt:Sprite
    {
        public float _radius;
        public Vec2 _position;
        public int cleanImpact;
        public Dirt(ref int meter) : base("dirt.png")
        {
            x = Utils.Random(200, game.width - 250);
            y = Utils.Random(200, game.height - 250);
            _position.SetXY(x,y);
            int type = Utils.Random(1, 100);
            if (type <= 50)
            {
                width /= 5;
                height /= 5;
                cleanImpact = 5;
            }
            else
            {
                width /= 10;
                height /= 10;
                cleanImpact = 3;
            }
            meter += cleanImpact;
            _radius = width / 2;
           
        }
    }
}
