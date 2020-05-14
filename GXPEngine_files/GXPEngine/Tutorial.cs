using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Tutorial:GameObject{

        Canvas canvas;
        Level level;
        Button skip, next;
        Font font;
        Sprite image;
        string text;
        public bool isVisible;
        public int count;
        public Tutorial(Vec2 position, Level level) : base()
        {
            canvas = new Canvas(500, 400);
            canvas.SetXY(position.x, position.y);
            image = new Sprite("tutorial_background.png");
            image.SetScaleXY(0.4f);
            image.SetXY(canvas.x, canvas.y);
            isVisible = true;
            skip = new Button(new Vec2(canvas.x + 50, canvas.y + canvas.height - 50), 100, 50, "SKIP");
            next = new Button(new Vec2(canvas.x + canvas.width - 200, canvas.y + canvas.height - 50), 100, 500, "NEXT");
            count = 0;
            font = new Font("MV Boli", 18);
            AddChild(image);
            AddChild(canvas);
            AddChild(skip);
            AddChild(next);
            this.level = level;
        }


        void Update()
        {
            canvas.graphics.Clear(Color.Transparent);
            if (isVisible)
            {
                canvas.graphics.DrawString(text, font, Brushes.Black, canvas.width / 2 - 200, 20);
                if (MyGame.CheckMouseInRectClick(skip) && HasChild(skip))
                {
                    isVisible = false;
                    level.isInTutorial = false;
                    RemoveChild(skip);
                    RemoveChild(next);
                    RemoveChild(image);
                }
                if (MyGame.CheckMouseInRectClick(next) && HasChild(next))
                {
                    count++;
                    if(count > 10)
                    {
                        isVisible = false;
                        level.isInTutorial = false;
                        RemoveChild(skip);
                        RemoveChild(next);
                        RemoveChild(image);
                    }
                }

                switch (count)
                {
                    case 0:
                        text = "Hello and welcome to your\nnew aquarium. This is the\nmain hub. From here you\ncan see all" +
                            "of your aquariums\nand you can also repair them\nto get access to new\ntypes of fish.";
                        break;
                    case 1:
                        text = "First you need to buy\nyour first aquarium.\nThankfully you have enough\nmoney to do so. Click on\nthe aquarium to the left\nto select it.";
                        RemoveChild(next);
                        break;
                    case 2:
                        text = "Good Job! This is the\nfirst aquarium. Later you\nwill be able to marvel at\nyour fishes here. Click on\n'Buy' to purchase your\nfirst aquarium.";
                        break;
                    case 3:
                        text = "Congratulations on your\nnew Aquarium. But I think it\nhas seen better days. Click on\nthe sponge on the right and\nuse it to wipe the stains\nfrom the glass.Beware as\nstains could be behind\nthis tutorial";
                        break;
                    case 4:
                        text = "Perfect. Now you just\nneed a fish and you have a\nworking Aquarium. To buy\nfish you need to click\non the shop icon\nto the right.";
                        break;
                    case 5:
                        text = "This is the shop. Here you\ncan buy new fish. The choice\nof fish depends on the type\nof Aquarium you're in.";
                        AddChild(next);
                        break;
                    case 6:
                        RemoveChild(next);
                        text = "Go and use the rest of\nyour money to buy the first\nfish by clicking on the buy\nbutton.";
                        break;
                    case 7:
                        text = "Good Job you bought your\nfirst fish! Fish create\ndirt and need to be fed\nfrom time to time.";
                        AddChild(next);
                        break;
                    case 8:
                        text = "This icon shows that\nthe fish is hungry. To feed\nhim click on the food\ncontainer to the right and\nclick anywhere in the level\nto spread some fish food";
                        RemoveChild(next);
                        break;
                    case 9:
                        text = "As you can see the\nfish goes towards the food\nand will produce a coin.\nFish only produce coins when\nthey" +
                            "are fed and the\naquarium is clean. Coins can\nbe used to buy more\nfish and aquariums.";
                        AddChild(next);
                        break;
                    case 10:
                        text = "One last thing before\nI let you run your own\naquarium. The journal at the\nbottom right includes \ninformation to all fish\nyou collect. Now go\nand have fun!";
                        break;

                }
            }
        }
    }
}
