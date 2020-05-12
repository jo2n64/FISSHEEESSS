using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class Journal : GameObject
{
    Sprite journalButton, close;
    Sprite journal;
    Font titleFont, textFont;
    List<Fish> freshFish, seaFish, deepFish, listToShow;
    List<Sprite> freshSprites, seaSprites, deepSprites, spritesToShow, fishSprites;
    List<Button> freshButtons, seaButtons, deepButtons, categories, buttonsToShow, buttons;
    Canvas canvas, descriptionCanvas;
    Level level;
    int category;

    public bool inWindow;
    public Journal(Level level) : base()
    {
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
        journalButton = new Sprite("journalbutton.png");
        journalButton.SetXY(game.width - 250, game.height - 200);
        close = new Sprite("jurnalClose.png");
        journal = new Sprite("journalitself.png");
        journal.SetXY(50, 0);
        close.SetXY(journal.x + journal.width - close.width, journal.y);
        canvas = new Canvas(journal.width, journal.height);
        descriptionCanvas = new Canvas(500, 500);
        category = 1;
        AddChild(journalButton);
        AddChild(journal);
        AddChild(close);
        AddChild(canvas);
        AddChild(descriptionCanvas);
        journal.alpha = 0f;
        close.alpha = 0f;
        titleFont = new Font("Times New Roman", 24);
        textFont = new Font("Times New Roman", 16);
        inWindow = false;
        for (int i = 0; i < 3; i++)
        {
            string text = "";
            switch (i)
            {
                case 0:
                    text = "Fresh Fish";
                    break;
                case 1:
                    text = "Sea Fish";
                    break;
                case 2:
                    text = "Deep Fish";
                    break;
            }
            Button button = new Button(new Vec2(journal.x + 50 + 110 * i, journal.y + 50), 100, 50, text);
            categories.Add(button);
        }
    }

    void Update()
    {
        canvas.SetXY(journal.x, journal.y);
        descriptionCanvas.SetXY(journal.x + 500, journal.y + 450);
        if (!inWindow)
        {
            if (MyGame.CheckMouseInRectClick(journalButton))
            {
                journal.alpha = 1f;
                close.alpha = 1f;
                inWindow = true;
                foreach(Button button in categories)
                {
                    AddChild(button);
                }
                
            }
        }

        if (inWindow)
        {
            for(int i = 0; i < categories.Count; i++)
            {
                Button button = categories[i];
                if (MyGame.CheckMouseInRect(button))
                {
                    button.SetScaleXY(1.1f);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Console.WriteLine("Category used to be " + category);
                        category = i;
                        Console.WriteLine("current category is: " + category);
                        
                    }
                }
                else button.SetScaleXY(1f);
            }
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

            for(int i = 0; i < listToShow.Count; i++)
            {
                Button button = buttonsToShow[i];
                button.SetXY(journal.x + 100, journal.y + 200 + 50 * i);
                AddChild(button);
                if (MyGame.CheckMouseInRectClick(button))
                {
                    descriptionCanvas.graphics.Clear(Color.Transparent);
                    descriptionCanvas.graphics.DrawString(listToShow[i].GetFishDescription(), textFont, Brushes.Black, 0, 0);
                    spritesToShow[i].alpha = 1f;
                }
            }

            if (MyGame.CheckMouseInRectClick(close))
            {
                canvas.graphics.Clear(Color.Transparent);
                close.alpha = 0f;
                journal.alpha = 0f;
                inWindow = false;
                descriptionCanvas.graphics.Clear(Color.Transparent);
                foreach(Sprite spr in fishSprites)
                {
                    spr.alpha = 0f;
                }
                foreach(Button button in categories)
                {
                    RemoveChild(button);
                }
                foreach(Button button in buttons)
                {
                    RemoveChild(button);
                }
            }
        }
    }

    public void AddFish(Fish fish)
    {
        Button button = new Button(new Vec2(0, 0), 300, 30, fish.GetFishName());
        Sprite spr = new Sprite(fish.GetFishName() + "-icon.png");
        spr.SetXY(journal.x + 500, journal.y + 150);
        spr.SetScaleXY(0.2f);
        spr.alpha = 0f;
        buttons.Add(button);
        fishSprites.Add(spr);
        AddChild(spr);
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


}

