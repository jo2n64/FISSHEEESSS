using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Level : GameObject
{
    List<Button> buttons;
    public List<Scene> scenes;
    public Journal journal;
    public CurrencySystem currencySystem;
    public bool isInScene;
    bool isInHub;
    Sprite hub;
    public MyGame myGame;
    Options _options;
    public Level(MyGame myGame,Options options) : base()
    {
        _options = options;
        this.myGame = myGame;
        hub = new Sprite("aquariums.png");
        hub.SetScaleXY(0.9f);
        AddChild(hub);
        isInScene = false;
        buttons = new List<Button>();
        scenes = new List<Scene>();
        journal = new Journal(this);
        isInHub = true;
        currencySystem = new CurrencySystem();
        AddButton(new Button(new Vec2(100, game.height / 2), 300, 200, "dis de first tenk"));
        AddButton(new Button(new Vec2(game.width / 2 - 100, game.height / 2), 300, 200, "dis de second denk"));
        AddButton(new Button(new Vec2(game.width - 300, game.height / 2), 300, 200, "und diese ist die dritte Aquarium"));
        AddButton(new Button(new Vec2(game.width / 2, game.height - 100), 200, 100, "MAIN MENU"));
        AddScene(new Scene("bottom_1.png", currencySystem, this, 1,10));
        AddScene(new Scene("bottom_2.png", currencySystem, this, 2,100));
        AddScene(new Scene("fishtank3.jpg", currencySystem, this, 3,1000));
        AddChild(journal);
    }

    void Update()
    {
        if ((!isInScene && !journal.inWindow))
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (MyGame.CheckMouseInRectClick(buttons[i]))
                {
                    if (i < 3)
                    {
                        scenes[i].visible = true;
                        scenes[i].isActive = true;
                        isInScene = true;
                    }
                    if(i == 3 && isInHub) {
                        foreach(Scene scene in scenes)
                        {
                            RemoveChild(scene);
                        }
                        foreach(Button button in buttons)
                        {
                            RemoveChild(button);
                        }
                        RemoveChild(hub);
                        isInHub = false;
                        myGame.isPlaying = false;
                    }
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


