using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class Journal : GameObject
{
    Sprite journalButton;
    Sprite journal, window, close;
    Font titleFont, textFont;
    List<Fish> freshFish, seaFish, deepFish, listToShow;
    List<Sprite> freshSprites, seaSprites, deepSprites, spritesToShow, fishSprites;
    List<Button> freshButtons, seaButtons, deepButtons, categories, buttonsToShow, buttons;
    Canvas canvas, descriptionCanvas;
    Level level;
    Sound open;
    SoundChannel channel;
    int category;
    bool inWindow;

    public Journal(Level level) : base()
    {
        open = new Sound("opening_journal_shop_sound.wav");
        this.level = level;
        freshFish = new List<Fish>();
        seaFish = new List<Fish>();
        deepFish = new List<Fish>();
        buttons = new List<Button>();
        freshButtons = new List<Button>();
        seaButtons = new List<Button>();
        deepButtons = new List<Button>();
        categories = new List<Button>();
        fishSprites = new List<Sprite>();
        freshSprites = new List<Sprite>();
        seaSprites = new List<Sprite>();
        deepSprites = new List<Sprite>();
        journalButton = new Sprite("journal_icon.png");
        journalButton.SetScaleXY(0.25f);
        journalButton.SetXY(game.width - 130, game.height - 400);
        close = new Sprite("cross1.png");
        close.SetScaleXY(0.1f);
        journal = new Sprite("journalitself.png");
        journal.SetScaleXY(1.8f);
        journal.SetXY(game.width / 2 - journal.width / 2, game.height / 2 - journal.height / 2);
        close.SetXY(journal.x + journal.width - 120, journal.y + 40);
        canvas = new Canvas(journal.width, journal.height);
        descriptionCanvas = new Canvas(550, 500);
        category = 1;
        window = new Sprite("window_PNG17666.png");
        window.SetScaleXY(0.9f, 0.5f);
        window.SetXY(journal.x + 660, journal.y + 120);
        window.alpha = 0f;
        AddChild(journalButton);
        AddChild(journal);
        AddChild(close);
        AddChild(canvas);
        AddChild(descriptionCanvas);
        AddChild(window);
        journal.alpha = 0f;
        close.alpha = 0f;
        titleFont = new Font("MV Boli", 48);
        textFont = new Font("MV Boli", 16);
        inWindow = false;
        for (int i = 0; i < 3; i++)
        {
            string text = "";
            switch (i)
            {
                case 0:
                    text = "Fresh Water";
                    break;
                case 1:
                    text = "Sea Water";
                    break;
                case 2:
                    text = "Deep Water";
                    break;
            }
            Button button = new Button(new Vec2(journal.x + 50 + 180 * i, journal.y + 50), 150, 75, text);
            categories.Add(button);
        }
    }

    void Update()
    {
        canvas.SetXY(journal.x, journal.y);
        descriptionCanvas.SetXY(journal.x + 660, journal.y + 450);
        checkOutsideWindow();
        checkInWindow();

       
    }

    void checkOutsideWindow()
    {
        if (!inWindow)
        {
            if (MyGame.CheckMouseInRectClick(journalButton))
            {
                journal.alpha = 1f;
                close.alpha = 1f;
                window.alpha = 1f;
                inWindow = true;
                if (level.GetOptions().isSoundPlaying)
                {
                    channel = open.Play();
                }
                foreach (Button button in categories)
                {
                    AddChild(button);
                }

            }
        }
    }

    void makeCategoryButtons()
    {
        for (int i = 0; i < categories.Count; i++)
        {
            Button button = categories[i];
            if (MyGame.CheckMouseInRect(button))
            {
                button.SetScaleXY(1.1f);
                if (Input.GetMouseButtonDown(0))
                {
                    button.SetScaleXY(1.1f);
                    foreach (Button catButton in buttonsToShow)
                    {
                        RemoveChild(catButton);
                    }
                    descriptionCanvas.graphics.Clear(Color.Transparent);
                    foreach (Sprite spr in spritesToShow)
                    {
                        spr.alpha = 0f;
                    }
                    category = i;
                }
            }
            else button.SetScaleXY(1f);
        }
    }

    void checkForCategory()
    {
        switch (category)
        {
            case 0:
                buttonsToShow = freshButtons;
                listToShow = freshFish;
                spritesToShow = freshSprites;
                break;
            case 1:
                buttonsToShow = seaButtons;
                listToShow = seaFish;
                spritesToShow = seaSprites;
                break;
            case 2:
                buttonsToShow = deepButtons;
                listToShow = deepFish;
                spritesToShow = deepSprites;
                break;
        }
    }

    void mouseOverCheck()
    {
        foreach (Button button in buttonsToShow)
        {
            if (MyGame.CheckMouseInRect(button))
            {
                button.SetScaleXY(1.1f);
            }
            else button.SetScaleXY(1f);
        }
    }


    void showFishList()
    {
        for (int i = 0; i < listToShow.Count; i++)
        {
            Button button = buttonsToShow[i];
            button.SetXY(journal.x + 100, journal.y + 200 + 50 * i);
            AddChild(button);
            if (MyGame.CheckMouseInRectClick(button))
            {
                descriptionCanvas.graphics.Clear(Color.Transparent);
                descriptionCanvas.graphics.DrawString(listToShow[i].GetFishDescription(), textFont, Brushes.Black, 0, 0);
                spritesToShow[i].alpha = 1f;
                foreach (Sprite spr in spritesToShow)
                {
                    if (spritesToShow.IndexOf(spr) != i)
                    {
                        spr.alpha = 0f;
                    }
                }
            }
        }
    }

    void checkClosing()
    {
        if (MyGame.CheckMouseInRectClick(close))
        {
            canvas.graphics.Clear(Color.Transparent);
            close.alpha = 0f;
            journal.alpha = 0f;
            window.alpha = 0f;
            inWindow = false;
            descriptionCanvas.graphics.Clear(Color.Transparent);
            foreach (Sprite spr in fishSprites)
            {
                spr.alpha = 0f;
            }
            foreach (Button button in categories)
            {
                RemoveChild(button);
            }
            foreach (Button button in buttons)
            {
                RemoveChild(button);
            }
        }
    }

    void checkInWindow()
    {
        if (inWindow)
        {

            makeCategoryButtons();
            checkForCategory();
            mouseOverCheck();
            showFishList();
            checkClosing();

        }
    }

    public void AddFish(Fish fish)
    {
        Button button = new Button(new Vec2(0, 0), 300, 30, fish.GetFishName());
        Sprite spr = new Sprite(fish.GetFishName() + "-icon.png");
        spr.SetXY(journal.x + 690, journal.y + 150);
        spr.SetScaleXY(0.2f);
        spr.alpha = 0f;
        buttons.Add(button);
        fishSprites.Add(spr);
        AddChild(spr);
        checkForType(fish, button, spr);

    }

    void checkForType(Fish fish, Button button, Sprite spr)
    {
        switch (fish.GetFishType())
        {
            case "Fresh water":
                freshFish.Add(fish);
                freshButtons.Add(button);
                freshSprites.Add(spr);
                break;
            case "Sea water":
                seaFish.Add(fish);
                seaButtons.Add(button);
                seaSprites.Add(spr);
                break;
            case "Deep water":
                deepFish.Add(fish);
                deepButtons.Add(button);
                deepSprites.Add(spr);
                break;
        }
    }

    public bool GetInWindow()
    {
        return inWindow;
    }

    public Sprite GetJournalButton()
    {
        return journalButton;
    }


}

