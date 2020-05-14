using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Level : GameObject
{
    List<Button> buttons;
    public Tutorial tutorial;
    public bool isInTutorial;
    Font font;
    public List<Scene> scenes;
    public Journal journal;
    public CurrencySystem currencySystem;
    public bool isInScene;
    bool isInHub;
    bool inTutorial;
    Sprite hub, moneyIcon, home;
    public MyGame myGame;
    Canvas canvas;
    Sound clicks;
    public Options _options;
    public Level(MyGame myGame,Options options) : base()
    {
        inTutorial = false;
        clicks = new Sound("clicking button sound.wav");
        tutorial = new Tutorial(new Vec2(game.width / 2 - 150, game.height - 400), this);
        font = new Font("Fast Action", 24);
        moneyIcon = new Sprite("coin.png");
        moneyIcon.SetScaleXY(0.06f);
        home = new Sprite("home_icon.png");
        home.SetScaleXY(0.4f);
        home.SetXY(game.width / 2, game.height - home.height);
        moneyIcon.SetXY(game.width - 200, 30);
        canvas = new Canvas(200, 100);
        canvas.SetXY(game.width - 140, 50);
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
        AddScene(new Scene("bottom_1", currencySystem, this, 1,_options,10));
        AddScene(new Scene("bottom_2", currencySystem, this, 2, _options, 100));
        AddScene(new Scene("bottom_3", currencySystem, this, 3, _options, 1000));
        AddChild(canvas);
        AddChild(moneyIcon);
        AddChild(journal);
        AddChild(home);
        AddChildAt(tutorial, 100);
        SetChildIndex(journal, 100);
    }

    void Update()
    {
        canvas.graphics.Clear(Color.Transparent);
        canvas.graphics.DrawString(currencySystem.getMoney().ToString(), font, Brushes.Yellow, 0, 0);
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
                        if (_options.isSoundPlaying)
                        {
                            clicks.Play();
                        }
                        if(tutorial.count == 1 && i == 0)
                        {
                            tutorial.count = 2;
                        }
                    }
                }
            }
            home.alpha = 1f;
        }
        if (isInScene)
        {
            home.alpha = 0f;
        }
        if (HasChild(tutorial)){
            inTutorial = true;
        }
        if (!HasChild(tutorial))
        {
            inTutorial = false;
        }
        if(MyGame.CheckMouseInRectClick(home) && home.alpha == 1f)
        {
            parent.RemoveChild(this);
            myGame.isPlaying = false;
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


