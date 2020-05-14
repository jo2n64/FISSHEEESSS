using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Tutorial:GameObject{

        Canvas canvas;
        Scene scene;
        Button skip, next;
        Font font;
        Sprite image;
        string text;
        public bool isVisible;
        public int count;
        public Tutorial(Vec2 position, Scene scene) : base()
        {
            canvas = new Canvas(500, 400);
            canvas.SetXY(position.x, position.y);
            image = new Sprite("tutorial_background.png");
            image.SetScaleXY(0.4f);
            image.SetXY(canvas.x, canvas.y);
            isVisible = true;
            skip = new Button(new Vec2(canvas.x, canvas.y), 100, 100, "SKIP");
            next = new Button(new Vec2(canvas.x + canvas.width - 100, canvas.y), 100, 100, "NEXT");
            count = 0;
            font = new Font("MV Boli", 18);
            AddChild(image);
            AddChild(canvas);
            AddChild(skip);
            AddChild(next);
            this.scene = scene;
        }


        void Update()
        {
            canvas.graphics.Clear(Color.Transparent);
            if (isVisible)
            {
                canvas.graphics.DrawString(text, font, Brushes.Black, canvas.width / 2 - 200, canvas.height/2);
                if (MyGame.CheckMouseInRectClick(skip))
                {
                    isVisible = false;
                    scene.passedTutorial = true;
                    RemoveChild(skip);
                    RemoveChild(next);
                    RemoveChild(image);
                }
                if (MyGame.CheckMouseInRectClick(next))
                {
                    count++;
                    if(count >= 9)
                    {
                        isVisible = false;
                        scene.passedTutorial = true;
                        RemoveChild(skip);
                        RemoveChild(next);
                        RemoveChild(image);
                    }
                }

                switch (count)
                {
                    case 0:
                        text = "Hello, and welcome to your\nfirst tank! Click next to continue";
                        break;
                    case 1:
                        text = "Start off by clicking on the left square.\nThis will unlock the aquarium you're in.\nYou will notice";
                        break;
                    case 2:
                        text = "On the right you see all the stuff\nyou will need to take care of the aquarium";
                        break;
                    case 3:
                        text = "As you can see, the aquarium is dirty.\n Click on the sponge to pick it up.";
                        break;
                    case 4:
                        text = "Great! Now mouse over\nthe aquarium to remove the dirt";
                        break;
                    case 5:
                        text = "Now that you've got the aquarium cleaned, \nyou can click on the shop icon to get a fish";
                        break;
                    case 6:
                        text = "Nice! Now click on the food icon to feed the fish";
                        break;
                    case 7:
                        text = "Now that you have the food picked up,\nchoose a place on the\naquarium and click to drop food";
                        break;
                    case 8:
                        text = "Congrats, you passed the tutorial!\nClick on Next or Skip to exit this window";
                        break;

                }
            }
        }
    }
}
