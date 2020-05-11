using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
   public class Food:Sprite
    {
        public Vec2 _position;
        float minY, maxY;
        float move=0.3f;
        public Food() : base("fish_food.png")
        {
            this.SetOrigin(width / 2, height / 2);
            width /= 50;
            height /= 50;
            _position.SetXY(Input.mouseX, Input.mouseY);
            this.x = Input.mouseX;
            this.y = Input.mouseY;
            minY = this.y-20;
            maxY = this.y+20;
        }
        void Update()
        {
            animate();
        }
        void animate()
        {
            this.y += move;
            if (this.y >= maxY|| this.y <= minY) move *= -1;
        }
    }
}
