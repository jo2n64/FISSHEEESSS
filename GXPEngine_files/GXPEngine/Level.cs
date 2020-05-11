using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Level : Sprite
{
    List<Button> buttons;
    List<Scene> scenes;
    public Journal journal;
    public CurrencySystem currencySystem;
    public bool isInScene;
    //Tutorial tutorial;
    public Level() : base("aquariums.png")
    {
        //tutorial = new Tutorial();
        isInScene = false;
        buttons = new List<Button>();
        scenes = new List<Scene>();
        journal = new Journal(this);
        currencySystem = new CurrencySystem();
        AddButton(new Button(new Vec2(100, game.height / 2), 300, 200, "dis de first tenk"));
        AddButton(new Button(new Vec2(game.width / 2 - 100, game.height / 2), 300, 200, "dis de second denk"));
        AddButton(new Button(new Vec2(game.width - 300, game.height / 2), 300, 200, "und diese ist die dritte Aquarium"));
        AddScene(new Scene("bottom_1.png", currencySystem, this, 1,200));
        AddScene(new Scene("empty_tank2.jpg", currencySystem, this, 2,300));
        AddScene(new Scene("fishtank3.jpg", currencySystem, this, 3,400));
        AddChild(journal);
        //AddChild(tutorial);
    }

    void Update()
    {

        if (!isInScene && !journal.inWindow)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (MyGame.CheckMouseInRectClick(buttons[i]))
                {
                    scenes[i].visible = true;
                    scenes[i].isActive = true;
                    isInScene = true;
                }
            }
        }
    }

    void AddScene(Scene scene)
    {
        AddChild(scene);
        scenes.Add(scene);
    }


    void AddButton(Button button)
    {
        AddChild(button);
        buttons.Add(button);
    }

    public CurrencySystem GetCurrencySystem()
    {
        return currencySystem;
    }

}


