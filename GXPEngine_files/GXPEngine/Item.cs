using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Item : Sprite
{
    public bool selected = false;
    public int id;
    public Item(string fileName, int idTag) : base(fileName)
    {
        id = idTag;
    }
}

