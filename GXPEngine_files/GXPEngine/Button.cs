using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class Button : GameObject
{
    Canvas canvas;
    int width, height;
    StringFormat sf;
    Font font;
    string text;
    public bool isActive;
    public float Width
    {
        get { return width; }
    }
    public float Height
    {
        get { return height; }
    }


    public Button(Vec2 position, int width, int height, string text)
    {
        font = new Font("MV Boli", 18);
        isActive = true;
        sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;
        this.text = text;
        this.width = width;
        this.height = height;
        SetXY(position.x, position.y);
        canvas = new Canvas(width, height);
        AddChild(canvas);
    }



    void Update()
    {
        canvas.graphics.FillRectangle(Brushes.Transparent, 0, 0, width, height);
        canvas.graphics.DrawString(text, font, Brushes.Black, 0, 0);
    }

}

