using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Level : GameObject
{
    List<Button> buttons;
    Font font;
    public List<Scene> scenes;
    public Journal journal;
    public CurrencySystem currencySystem;
    public bool isInScene;
    bool isInHub;
    Sprite hub, moneyIcon;
    public MyGame myGame;
    Canvas canvas;
    Options _options;
    public Level(MyGame myGame,Options options) : base()
    {
        font = new Font("Fast Action", 24);
        moneyIcon = new Sprite("coin.png");
        moneyIcon.SetScaleXY(0.06f);
        moneyIcon.SetXY(game.width - 200, 30);
        canvas = new Canvas(200, 100);
        canvas.SetXY(game.width - 100, 50);
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
        AddButton(new Button(new Vec2(0, 300), 500, 400, "dis de first tenk"));
        AddButton(new Button(new Vec2(game.width / 2  - 260, game.height / 2 - 140), 650, 300, "dis de second denk"));
        AddButton(new Button(new Vec2(game.width - 310, 300), 320, 400, "und diese ist die dritte Aquarium"));
        AddButton(new Button(new Vec2(game.width / 2, game.height - 100), 200, 100, "MAIN MENU"));
        AddScene(new Scene("bottom_1", currencySystem, this, 1,_options,10));
        AddScene(new Scene("bottom_2", currencySystem, this, 2, _options, 100));
        AddScene(new Scene("bottom_3", currencySystem, this, 3, _options, 1000));
        AddChild(canvas);
        AddChild(moneyIcon);
        AddChild(journal);
        SetChildIndex(journal, 100);
    }

    void Update()
    {
        canvas.graphics.Clear(Color.Transparent);
        canvas.graphics.DrawString(currencySystem.money.ToString(), font, Brushes.Yellow, 0, 0);
        if ((!isInScene && !journal.inWindow))
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (MyGame.CheckMouseInRectClick(buttons[i]) && !MyGame.CheckMouseInRect(journal.journalButton))
                {
                    if (i < 3)
                    {
                        scenes[i].visible = true;
                        scenes[i].isActive = true;
                        isInScene = true;
                    }
                    if(i == 3 && isInHub) {
                        isInHub = false;
                        myGame.isPlaying = false;
                        myGame.RemoveChild(this);
                        //Console.WriteLine(myGame.isPlaying);
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
        buttons.Add(button);
    }

    public CurrencySystem GetCurrencySystem()
    {
        return currencySystem;
    }

}


