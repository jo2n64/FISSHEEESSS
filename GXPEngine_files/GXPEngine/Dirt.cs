using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Dirt : Sprite
{
    public float _radius;
    public Vec2 _position;
    private int cleanImpact;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Dirt(ref int meter) : base("dirt.png")
    {
        setTypeAndSize();
        meter += cleanImpact;
        _radius = width / 2;
        x = Utils.Random(width, game.width - width-100);
        y = Utils.Random(height, game.height - height-100);
        _position.SetXY(x, y);

    }
    //------------------------------------------------------------------------
    //                          setTypeAndSize
    //------------------------------------------------------------------------
    private void setTypeAndSize()
    {
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
    }
    //------------------------------------------------------------------------
    //                          getCleanImpact
    //------------------------------------------------------------------------
    public int getCleanImpact()
    {
        return cleanImpact;
    }
}

