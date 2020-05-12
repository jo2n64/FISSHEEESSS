using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;

public class MyGame : Game
{
    Button play, options, exit;
    Level level;
    public SoundChannel musicChannel;
    public Sound music;
    bool isPlaying;
    

    public MyGame() : base(1200, 700, false)        // Create a window that's 800x600 and NOT fullscreen
    {
        
        isPlaying = false;
        play = new Button(new Vec2(width / 2, height / 2 - 100), 200, 100, "Welcome");
        options = new Button(new Vec2(width / 2, height / 2), 200, 100, "Options");
        exit = new Button(new Vec2(width / 2, height / 2 + 100), 200, 100, "Exit");
        music = new Sound("freshTank.mp3");
        AddChild(play);
        AddChild(options);
        AddChild(exit);

    }

    void Update()
    {
        if (CheckMouseInRectClick(play) && !isPlaying)
        {
            level = new Level(this);
           // musicChannel = music.Play();
            AddChild(level);
            isPlaying = true;
        }
        if (CheckMouseInRectClick(options) && !isPlaying)
        {

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