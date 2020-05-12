using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Options:GameObject
    {
        public bool isMusicPlaying = true;
        public bool isSoundPlaying = true;
        Button music;
        Button sound;
        public Options()
        {
            music = new Button(new Vec2(300, 200), 100, 50, "");
            sound = new Button(new Vec2(300, 400), 100, 50, "");
        }

        void Update()
        {
            if (MyGame.CheckMouseInRectClick(music))
            {
                if (isMusicPlaying == true)
                {
                    isMusicPlaying = false;
                }
                else
                {
                    isMusicPlaying = true;
                }
            }
            if (MyGame.CheckMouseInRectClick(sound))
            {
                if (isSoundPlaying == true)
                {
                    isSoundPlaying = false;
                }
                else
                {
                    isSoundPlaying = true;
                }
            }
        }

    }
}
