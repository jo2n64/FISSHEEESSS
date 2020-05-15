using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Sponge : Sprite
{
    private  Vec2 _position;
    private float _radius;
    private Scene _currentScene;
    private  List<Dirt> dirtList;

    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Sponge(Scene currentScene) : base("sponge.png")
    {
        SetOrigin(width / 2, height / 2);
        width /= 5;
        height /= 5;
        _radius = width / 2;
        dirtList = new List<Dirt>();
        _currentScene = currentScene;
    }
    //------------------------------------------------------------------------
    //                          UpdateScreenPosition
    //------------------------------------------------------------------------
    private void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        _position.SetXY(Input.mouseX, Input.mouseY);
        UpdateScreenPosition();
        BallBallCollisionDetection();
    }
    //------------------------------------------------------------------------
    //                          BallBallCollisionDetection
    //------------------------------------------------------------------------
    private void BallBallCollisionDetection()
    {
        for (int i = 0; i < dirtList.Count; i++)
        {
            Dirt dirt = dirtList[i] as Dirt;
            Vec2 relativePosition = _position - dirt._position;
            if (relativePosition.Magnitude() < _radius + dirt._radius)
            {
                _currentScene.removeDirtConsequence(dirt);
                removeDirt(dirt);
                dirt.LateDestroy();
            }
        }
    }
    //------------------------------------------------------------------------
    //                          addDirt
    //------------------------------------------------------------------------
    public void addDirt(Dirt dirt)
    {
        dirtList.Add(dirt);
    }
    //------------------------------------------------------------------------
    //                          removeDirt
    //------------------------------------------------------------------------
    public void removeDirt(Dirt dirt)
    {
        dirtList.Remove(dirt);
    }
    //------------------------------------------------------------------------
    //                          getNumerOfElementInDirtList
    //------------------------------------------------------------------------
    public int getNumerOfElementInDirtList()
    {
        return dirtList.Count;
    }
}

