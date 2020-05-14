using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;


public class Options : GameObject
{
    public bool isMusicPlaying = true;
    public bool isSoundPlaying = true;

    private Button music;
    private Button sound;
    private Button backToMainMenu;

    private Sprite musicIcon;
    private Sprite soundIcon;

    private Sprite cross1;
    private Sprite cross2;

    private Sprite textMusic;
    private Sprite textSound;

    private Sprite backGround;
    private Sprite home;

    private Sound click;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Options()
    {
        makeBackground();
        makeMusicIcon();
        makeSoundIcon();
        makeCross1();
        makeCross2();
        makeTextMusic();
        makeTextSound();
        makeHomeIcon();
        makeButtons();

        addChildrenToScreen();

        click = new Sound("clicking buttons sound.wav");
    }
    //------------------------------------------------------------------------
    //                          addChildrenToScreen
    //------------------------------------------------------------------------
    private void addChildrenToScreen()
    {
        AddChild(backGround);
        AddChild(musicIcon);
        AddChild(soundIcon);

        AddChild(textMusic);
        AddChild(textSound);
        AddChild(home);
    }
    //------------------------------------------------------------------------
    //                          makeButtons
    //------------------------------------------------------------------------
    private void makeButtons()
    {
        backToMainMenu = new Button(new Vec2(50, game.height - 220), 150, 150, "Bach to main menu");
        music = new Button(new Vec2(musicIcon.x - musicIcon.width / 2, musicIcon.y - musicIcon.width / 2), musicIcon.width, musicIcon.height, "MUSIC");
        sound = new Button(new Vec2(soundIcon.x - soundIcon.width / 2, soundIcon.y - soundIcon.height / 2), soundIcon.width, soundIcon.height, "SOUND");
    }
    //------------------------------------------------------------------------
    //                              private void makeHomeIcon()

    //------------------------------------------------------------------------
    private void makeHomeIcon()
    {
        home = new Sprite("home_icon.png");
        home.SetOrigin(home.width / 2, home.height / 2);
        home.SetXY(game.width / 13, game.height - 150);
        home.width /= 3;
        home.height /= 3;
    }
    //------------------------------------------------------------------------
    //                          makeTextSound
    //------------------------------------------------------------------------
    private void makeTextSound()
    {
        textSound = new Sprite("sound_icon.png");
        textSound.SetOrigin(textSound.width / 2, textSound.height / 2);
        textSound.SetXY(game.width / 3 * 2 - 130, game.height / 3 * 2);
        textSound.width /= 3;
        textSound.height /= 3;
    }
    //------------------------------------------------------------------------
    //                          makeTextMusic
    //------------------------------------------------------------------------
    private void makeTextMusic()
    {
        textMusic = new Sprite("music_icon.png");
        textMusic.SetOrigin(textMusic.width / 2, textMusic.height / 2);
        textMusic.SetXY(game.width / 3 * 2 - 130, game.height / 3);
        textMusic.width /= 3;
        textMusic.height /= 3;
    }
    //------------------------------------------------------------------------
    //                          makeCross2
    //------------------------------------------------------------------------
    private void makeCross2()
    {
        cross2 = new Sprite("cross2.png");
        cross2.SetOrigin(cross2.width / 2, cross2.height / 2);
        cross2.SetXY(game.width / 3 + 130, game.height / 3 * 2);
        cross2.width /= 4;
        cross2.height /= 4;
    }
    //------------------------------------------------------------------------
    //                          makeCross1
    //------------------------------------------------------------------------
    private void makeCross1()
    {
        cross1 = new Sprite("cross2.png");
        cross1.SetOrigin(cross1.width / 2, cross1.height / 2);
        cross1.SetXY(game.width / 3 + 130, game.height / 3);
        cross1.width /= 4;
        cross1.height /= 4;
    }
    //------------------------------------------------------------------------
    //                          makeSoundIcon
    //------------------------------------------------------------------------
    private void makeSoundIcon()
    {
        soundIcon = new Sprite("sound.png");
        soundIcon.SetOrigin(soundIcon.width / 2, soundIcon.height / 2);
        soundIcon.SetXY(game.width / 3 + 130, game.height / 3 * 2);
        soundIcon.width /= 3;
        soundIcon.height /= 3;
    }
    //------------------------------------------------------------------------
    //                          makeMusicIcon
    //------------------------------------------------------------------------
    private void makeMusicIcon()
    {
        musicIcon = new Sprite("music.png");
        musicIcon.SetOrigin(musicIcon.width / 2, musicIcon.height / 2);
        musicIcon.SetXY(game.width / 3 + 130, game.height / 3);
        musicIcon.width /= 3;
        musicIcon.height /= 3;
    }
    //------------------------------------------------------------------------
    //                          makeBackground
    //------------------------------------------------------------------------
    private void makeBackground()
    {
        backGround = new Sprite("main_menu_plants.png");
        backGround.width = game.width;
        backGround.height = game.height;
    }
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        if (visible == true)
        {
            handleMusicOption();
            handleSoundOption();
            handleReturToMenuOption();
        }
    }
    //------------------------------------------------------------------------
    //                          handleReturToMenuOption
    //------------------------------------------------------------------------
    private void handleReturToMenuOption()
    {
        if (MyGame.CheckMouseInRectClick(backToMainMenu))
        {
            visible = false;
            if (isSoundPlaying)
            {
                click.Play();
            }
        }
    }
    //------------------------------------------------------------------------
    //                          handleSoundOption
    //------------------------------------------------------------------------
    private void handleSoundOption()
    {
        if (MyGame.CheckMouseInRectClick(sound))
        {
            if (isSoundPlaying == true)
            {
                isSoundPlaying = false;
                AddChild(cross2);
            }
            else
            {
                isSoundPlaying = true;
                RemoveChild(cross2);
            }
        }
    }
    //------------------------------------------------------------------------
    //                          handleMusicOption
    //------------------------------------------------------------------------
    private void handleMusicOption()
    {
        if (MyGame.CheckMouseInRectClick(music))
        {
            if (isMusicPlaying == true)
            {
                isMusicPlaying = false;
                AddChild(cross1);
            }
            else
            {
                isMusicPlaying = true;
                RemoveChild(cross1);
            }
        }
    }
}

