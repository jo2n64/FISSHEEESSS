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
        Button backToMainMenu;
        Sprite MusicIcon;
        Sprite SoundIcon;
        Sprite Box1;
        Sprite Box2;
        Sprite Check1;
        Sprite Check2;
        Sprite TextMusic;
        Sprite TextSound;
        Sprite bg;
        Sprite home;


        public Options()
        {
            bg = new Sprite("main_menu_plants.png");
            bg.width = game.width;
            bg.height = game.height;

            backToMainMenu = new Button(new Vec2(50, game.height - 220), 150, 150, "Bach to main menu");
            MusicIcon = new Sprite("music.png");
            MusicIcon.SetOrigin(MusicIcon.width / 2, MusicIcon.height / 2);
            MusicIcon.x = game.width / 3+130;
            MusicIcon.y = game.height / 3;
            MusicIcon.width /= 3;
            MusicIcon.height /= 3;

            SoundIcon = new Sprite("sound.png");
            SoundIcon.SetOrigin(SoundIcon.width / 2, SoundIcon.height / 2);
            SoundIcon.x = game.width / 3+130;
            SoundIcon.y = game.height / 3*2;
            SoundIcon.width /= 3;
            SoundIcon.height /= 3;

            //Box1 = new Sprite("border.png");
            //Box1.SetOrigin(Box1.width / 2, Box1.height / 2);
            //Box1.x = game.width / 3*2;
            //Box1.y = game.height / 3;
            //Box1.width /= 3;
            //Box1.height /= 3;

            //Box2 = new Sprite("border.png");
            //Box2.SetOrigin(Box2.width / 2, Box2.height / 2);
            //Box2.x = game.width / 3*2;
            //Box2.y = game.height / 3 * 2;
            //Box2.width /= 3;
            //Box2.height /= 3;

            Check1 = new Sprite("cross2.png");
            Check1.SetOrigin(Check1.width / 2, Check1.height / 2);
            Check1.x = game.width / 3 + 130;
            Check1.y = game.height / 3;
            Check1.width /= 4;
            Check1.height /= 4;

            Check2 = new Sprite("cross2.png");
            Check2.SetOrigin(Check2.width / 2, Check2.height / 2);
            Check2.x = game.width / 3 + 130;
            Check2.y = game.height / 3 * 2;
            Check2.width /= 4;
            Check2.height /= 4;

            TextMusic = new Sprite("music_icon.png");
            TextMusic.SetOrigin(TextMusic.width / 2, TextMusic.height / 2);
            TextMusic.SetXY(game.width / 3*2-130, game.height / 3);
            TextMusic.width /= 3;
            TextMusic.height /= 3;

            TextSound = new Sprite("sound_icon.png");
            TextSound.SetOrigin(TextSound.width / 2, TextSound.height / 2);
            TextSound.SetXY(game.width / 3*2-130, game.height / 3 * 2);
            TextSound.width /= 3;
            TextSound.height /= 3;

            home = new Sprite("home_icon.png");
            home.SetOrigin(home.width / 2, home.height / 2);
            home.SetXY(game.width / 13, game.height -150);
            home.width /= 3;
            home.height /= 3;

            music = new Button(new Vec2(MusicIcon.x - MusicIcon.width / 2, MusicIcon.y - MusicIcon.width/2), MusicIcon.width, MusicIcon.height, "MUSIC");
            sound = new Button(new Vec2(SoundIcon.x - SoundIcon.width / 2, SoundIcon.y - SoundIcon.height / 2), SoundIcon.width, SoundIcon.height, "SOUNDS");
            AddChild(bg);
            AddChild(MusicIcon);
            AddChild(SoundIcon);

            //AddChild(Check1);
            //AddChild(Check2);

            //AddChild(Box1);
            //AddChild(Box2);

            //AddChild(SoundIcon);
            AddChild(TextMusic);
            AddChild(TextSound);
            AddChild(home);
           // AddChild(music);
            //AddChild(sound);
            //AddChild(backToMainMenu);
        }

        void Update()
        {
            if (visible == true)
            {
                if (MyGame.CheckMouseInRectClick(music))
                {
                    if (isMusicPlaying == true)
                    {
                        isMusicPlaying = false;
                        AddChild(Check1);
                    }
                    else
                    {
                        isMusicPlaying = true;
                        RemoveChild(Check1);
                    }
                }
                if (MyGame.CheckMouseInRectClick(sound))
                {
                    if (isSoundPlaying == true)
                    {
                        isSoundPlaying = false;
                        AddChild(Check2);
                    }
                    else
                    {
                        isSoundPlaying = true;
                        RemoveChild(Check2);
                    }
                }
                if (MyGame.CheckMouseInRectClick(backToMainMenu))
                {
                    visible = false;
                }
            }
        }

    }
}
