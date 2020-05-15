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
        
        clicks = new Sound("clicking button sound.wav");
        tutorial = new Tutorial(new Vec2(game.width / 2 - 150, game.height - 400), this);
        font = new Font("Fast Action", 24);

        initSprites();
        

        _options = options;
        this.myGame = myGame;
        
        
        isInScene = false;
        canvas = new Canvas(200, 100);
        canvas.SetXY(game.width - 140, 50);
        initLists();
        journal = new Journal(this);
        initBools();
        currencySystem = new CurrencySystem();
        initButtons();
        AddChild(hub);
        initScenes();
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

        checkForButtonClicks();

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
            myGame.SetIsPlaying(false);
        }

    }

    void checkForButtonClicks()
    {
        if ((!isInScene && !journal.GetInWindow()))
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (MyGame.CheckMouseInRectClick(buttons[i]) && !MyGame.CheckMouseInRect(journal.GetJournalButton()))
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
                        if (tutorial.GetCount() == 1 && i == 0)
                        {
                            tutorial.SetCount(2);
                        }
                    }
                }
            }
            home.alpha = 1f;
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

    void initSprites()
    {
        moneyIcon = new Sprite("coin.png");
        moneyIcon.SetScaleXY(0.06f);
        moneyIcon.SetXY(game.width - 200, 30);

        home = new Sprite("home_icon.png");
        home.SetScaleXY(0.4f);
        home.SetXY(game.width / 2, game.height - home.height);

        hub = new Sprite("aquariums.png");
        hub.SetScaleXY(0.9f);
    }

    void initScenes()
    {
        AddScene(new Scene("bottom_1", this, 1, _options, 10));
        AddScene(new Scene("bottom_2", this, 2, _options, 100));
        AddScene(new Scene("bottom_3", this, 3, _options, 1000));
    }

    void initBools()
    {
        isInHub = true;
        inTutorial = false;
    }

    void initLists()
    {
        buttons = new List<Button>();
        scenes = new List<Scene>();
    }

    void initButtons()
    {
        AddButton(new Button(new Vec2(0, 300), 500, 400, "dis de first tenk"));
        AddButton(new Button(new Vec2(game.width / 2 - 260, game.height / 2 - 140), 650, 300, "dis de second denk"));
        AddButton(new Button(new Vec2(game.width - 310, 300), 320, 400, "und diese ist die dritte Aquarium"));
    }

    public CurrencySystem GetCurrencySystem()
    {
        return currencySystem;
    }

    public void SetIsInScene(bool isInScene)
    {
        this.isInScene = isInScene;
    }

    public void SetInTutorial(bool inTutorial)
    {
        this.inTutorial = inTutorial;
    }
    
    public Tutorial GetTutorial()
    {
        return tutorial;
    }

    public Options GetOptions()
    {
        return _options;
    }

    public Journal GetJournal()
    {
        return journal;
    }

    public MyGame GetMyGame()
    {
        return myGame;
    }

}


