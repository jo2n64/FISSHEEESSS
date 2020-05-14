using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;

public class MyGame : Game
{
    Button play, options, exit, easterEgg;
    Sprite bg, changedaworld;
    Level level;
    Options option;
    public SoundChannel musicChannel;
    public Sound music, change;
    public bool isPlaying;
    int timer;
    bool hasStarted, inEasterEgg;
    public MyGame() : base(1600, 900, false)        // Create a window that's 800x600 and NOT fullscreen
    {
        timer = 8216;
        isPlaying = false;
        inEasterEgg = false;
        bg = new Sprite("mainmenu.png");
        changedaworld = new Sprite("changedaworld.png");
        changedaworld.SetScaleXY(3.3f, 1.9f);
        change = new Sound("myfinalmessage.mp3");
        bg.SetScaleXY(1.05f);
        play = new Button(new Vec2(width / 2 - 130, height / 2-10), 250, 100, "Welcome");
        options = new Button(new Vec2(width / 2 - 100, height / 2 + 100), 210, 90, "Options");
        exit = new Button(new Vec2(width / 2 - 60, height / 2 + 200), 160, 60, "Exit");
        music = new Sound("freshTank.mp3");
        easterEgg = new Button(new Vec2(width - 100, 0), 100, 100, "dont click if you dont\nwanna have nightmares");
        option = new Options();

        AddChild(bg);
        
        //AddChild(play);
        //AddChild(options);
        //AddChild(exit);
        AddChild(option);
        option.visible = false;

    }

    void Update()
    {
        if (CheckMouseInRectClick(play) && !isPlaying)
        {
            
            if (!hasStarted)
            {
                level = new Level(this, option);
                musicChannel = music.Play();
                hasStarted = true;
            }
            AddChild(level);
            isPlaying = true;
        }

        if(CheckMouseInRectClick(changedaworld) && !hasStarted && !inEasterEgg)
        {
            musicChannel = change.Play();
            AddChild(changedaworld);
            inEasterEgg = true;
        }

        if (musicChannel != null)
        {
            if (!option.isMusicPlaying)
            {
                musicChannel.Volume = 0f;
            }
            if (option.isMusicPlaying)
            {
                musicChannel.Volume = 1f;
            }
        }

        if(inEasterEgg)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                changedaworld.alpha -= 0.005f;
            }
            if(changedaworld.alpha <= 0f)
            {
                RemoveChild(changedaworld);
            }
        }

        if (CheckMouseInRectClick(options) && !isPlaying)
        {
            option.visible = true;
        }
        if (CheckMouseInRectClick(exit) && !isPlaying)
        {
            Environment.Exit(0);
        }

    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }

    public static bool CheckMouseInRect(Button button)
    {
        button.InverseTransformPoint(Input.mouseX, Input.mouseY);
        if (Input.mouseX >= button.x && Input.mouseX <= button.x + button.Width &&
                Input.mouseY >= button.y && Input.mouseY <= button.y + button.Height)
        {
            return true;
        }
        else return false;
    }

    public static bool CheckMouseInRect(Sprite sprite)
    {
        if (Input.mouseX > sprite.x && Input.mouseX < sprite.x + sprite.width &&
                Input.mouseY > sprite.y && Input.mouseY < sprite.y + sprite.height)
        {
            return true;
        }
        else return false;
    }

    public static bool CheckMouseInRectClick(Button button)
    {
        button.InverseTransformPoint(Input.mouseX, Input.mouseY);
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mouseX >= button.x && Input.mouseX <= button.x + button.Width &&
                Input.mouseY >= button.y && Input.mouseY <= button.y + button.Height)
            {
                return true;
            }
            return false;
        }
        else return false;
    }

    public static bool CheckMouseInRectClick(Sprite sprite)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mouseX > sprite.x && Input.mouseX < sprite.x + sprite.width &&
                Input.mouseY > sprite.y && Input.mouseY < sprite.y + sprite.height)
            {
                return true;
            }
            return false;
        }
        else return false;
    }

}