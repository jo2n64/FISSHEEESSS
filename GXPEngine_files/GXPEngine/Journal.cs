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
    List<Sprite> fishSprites;
    List<Button> freshButtons, seaButtons, deepButtons, categories, buttonsToShow;
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
        freshButtons = new List<Button>();
        seaButtons = new List<Button>();
        deepButtons = new List<Button>();
        categories = new List<Button>();
        fishSprites = new List<Sprite>();
        listToShow = freshFish;
        buttonsToShow = freshButtons;
        journalButton = new Sprite("journalbutton.png");
        journalButton.SetXY(game.width - 250, game.height - 200);
        close = new Sprite("jurnalClose.png");
        journal = new Sprite("journalitself.png");
        journal.SetXY(50, 0);
        close.SetXY(journal.x + journal.width - close.width, journal.y);
        canvas = new Canvas(journal.width, journal.height);
        descriptionCanvas = new Canvas(500, 500);
        category = 0;
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
                category = 0;
                for(int i = 0; i < buttonsToShow.Count; i++)
                {
                    Button button = buttonsToShow[i];
                    button.SetXY(journal.x + 50, journal.y + 150 + 50 * i);
                    if (!HasChild(button))
                    {
                        AddChild(button);
                    }
                    button.isActive = true;
                }
                foreach(Button button in categories)
                {
                    if (!HasChild(button))
                    {
                        AddChild(button);
                    }
                }
               
            }
        }

        if (inWindow)
        {
            switch (category)
            {
                case 0:
                    listToShow = freshFish;
                    buttonsToShow = freshButtons;
                    break;
                case 1:
                    listToShow = seaFish;
                    buttonsToShow = seaButtons;
                    break;
                case 2:
                    listToShow = deepFish;
                    buttonsToShow = deepButtons;
                    break;
            }

            for(int i = 0; i < categories.Count; i++)
            {
                if (MyGame.CheckMouseInRect(categories[i]))
                {
                    categories[i].SetScaleXY(1.1f);
                }
                else categories[i].SetScaleXY(1.0f);
                if (MyGame.CheckMouseInRectClick(categories[i]))
                {
                    category = i;
                    Console.WriteLine(category);
                }
            }

            for(int i = 0; i < listToShow.Count; i++)
            {
                if (MyGame.CheckMouseInRectClick(buttonsToShow[i]))
                { 
                    if(i >= 1)
                    {
                        fishSprites[i - 1].alpha = 0f;
                    }
                    fishSprites[i].alpha = 1f;
                    if (i < listToShow.Count - 1)
                    {
                        fishSprites[i + 1].alpha = 0f;
                    }
                    descriptionCanvas.graphics.Clear(Color.Transparent);
                    descriptionCanvas.graphics.DrawString(listToShow[i].GetFishDescription(), textFont, Brushes.Black, 0, 0);
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
                foreach (Button button in buttonsToShow)
                {
                    RemoveChild(button);
                }
                foreach (Button button in categories)
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
        fishSprites.Add(spr);
        AddChild(spr);
        switch (fish.GetFishType())
        {
            case "Fresh water":
                freshFish.Add(fish);
                freshButtons.Add(button);
                break;
            case "Sea water":
                seaFish.Add(fish);
                seaButtons.Add(button);
                break;
            case "Deep water":
                deepFish.Add(fish);
                deepButtons.Add(button);
                break;
        }

    }


}

