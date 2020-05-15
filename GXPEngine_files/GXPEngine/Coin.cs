using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Coin : Sprite
{
    private int value;
    private Level _level;
    private Sound colectCoin;
    private Options _option;

    //------------------------------------------------------------------------
    //                          Constructor
    //------------------------------------------------------------------------
    public Coin(Fish fish, Level level, Options option, string s) : base(s)
    {
        _option = option;
        x = Utils.Random(fish.x - fish.width, fish.x + fish.width);
        y = Utils.Random(fish.y - fish.height, fish.y + fish.height);
        value = fish.GetCoinValue();
        width /= 5;
        height /= 5;
        _level = level;
        colectCoin = new Sound("pick_up_coin_sound.wav", false, true);
    }
    //------------------------------------------------------------------------
    //                              Update
    //------------------------------------------------------------------------
    void Update()
    {
        overlap();
    }
    //------------------------------------------------------------------------
    //                              overlap
    //------------------------------------------------------------------------
    private void overlap()
    {

        if (MyGame.CheckMouseInRectClick(this))
        {
            if (_option.isSoundPlaying)
            {
                colectCoin.Play();
            }
            //colected = true;
            _level.GetCurrencySystem().AddMoney(value);
            this.LateDestroy();
        }
    }
}

