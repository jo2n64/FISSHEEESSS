using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


class Shop : GameObject
{
    private List<Fish> fishList;
    private Level _level;
    private Options _option;
    private Inventory inv;

    private Sound buyFishSound;
    private Sound notEnpughMoneySound;

    private Sprite close;
    private Sprite notEnoughMoney;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Shop(List<Fish> fishListOfTank, Level level, Inventory inventory, Options option)
    {
        _option = option;
        inv = inventory;

        initializeNotEnoughMoney();

        _level = level;
        fishList = fishListOfTank;

        buyFishSound = new Sound("buying_fish_sound.mp3", false, true);
        notEnpughMoneySound = new Sound("no_money_sound.wav", false, true);
        makeShop();
    }
    //------------------------------------------------------------------------
    //                          initializeNotEnoughMoney
    //------------------------------------------------------------------------
    private void initializeNotEnoughMoney()
    {
        notEnoughMoney = new Sprite("no_money.png");
        notEnoughMoney.width /= 10;
        notEnoughMoney.height /= 10;
    }

    //------------------------------------------------------------------------
    //                          makeCloseButton
    //------------------------------------------------------------------------
    private void makeCloseButton()
    {
        close = new Sprite("close_button.png");
        close.width /= 5;
        close.height /= 5;
        AddChild(close);
    }
    //------------------------------------------------------------------------
    //                          makeShop
    //------------------------------------------------------------------------
    void makeShop()
    {
        int i = 1;
        int j = 1;
        makeBackground();
        makeandDisplayFishIconsAndButtons(ref i, ref j);
        makeCloseButton();
    }
    //------------------------------------------------------------------------
    //                          makeandDisplayFishIconsAndButtons
    //------------------------------------------------------------------------
    private void makeandDisplayFishIconsAndButtons(ref int i, ref int j)
    {
        foreach (Fish fish in fishList)
        {
            if (fish.isUnlocked == false)
            {
                makeBuyButton(i, j, fish);
                makeFishNameAndPrice(i, j, fish);
                makeIconsForFish(i, j, fish);

                i++;
                if (i >= 4)
                {
                    j++;
                    i = 1;
                }
            }
        }
    }
    //------------------------------------------------------------------------
    //                          makeBuyButton
    //------------------------------------------------------------------------
    private void makeBuyButton(int i, int j, Fish fish)
    {
        AddChild(fish.buyToUnlock);
        fish.buyToUnlock.x = i * game.width / 4 - fish.buyToUnlock.width / 2;
        fish.buyToUnlock.y = j * game.height / 3 + 105;
    }
    //------------------------------------------------------------------------
    //                          makeFishNameAndPrice
    //------------------------------------------------------------------------
    private void makeFishNameAndPrice(int i, int j, Fish fish)
    {
        AddChild(fish.fishNameAndPrice);
        fish.fishNameAndPrice.x = i * game.width / 4 - fish.fishNameAndPrice.width / 2;
        fish.fishNameAndPrice.y = j * game.height / 3 + 10;
    }
    //------------------------------------------------------------------------
    //                          makeIconsForFish
    //------------------------------------------------------------------------
    private void makeIconsForFish(int i, int j, Fish fish)
    {
        Sprite fishIcon = new Sprite(fish.fishName + "-icon.png");
        AddChild(fishIcon);
        fishIcon.SetOrigin(fishIcon.width / 2, fishIcon.height / 2);
        fishIcon.width /= 8;
        fishIcon.height /= 8;
        fishIcon.x = i * game.width / 4;
        fishIcon.y = j * game.height / 3 - fishIcon.height / 2 + 20;
    }
    //------------------------------------------------------------------------
    //                          makeBackground
    //------------------------------------------------------------------------
    private void makeBackground()
    {
        Sprite backgroundShop = new Sprite("shop_shelf.png");
        AddChild(backgroundShop);
        backgroundShop.width = game.width - game.width / 5;
        backgroundShop.height = game.height - game.height / 5;
        backgroundShop.x = game.width / 10;
        backgroundShop.y = game.height / 10 - 20;
        close.x = backgroundShop.width + game.width / 10 - close.width - 20;
        close.y = game.height / 10;
    }
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        if (visible == true)
        {
            handleFishBuying();
            closeShop();
        }
        else
        {
            if (HasChild(notEnoughMoney))
            {
                RemoveChild(notEnoughMoney);
            }
        }
    }
    //------------------------------------------------------------------------
    //                          handleFishBuying
    //------------------------------------------------------------------------
    private void handleFishBuying()
    {
        foreach (Fish fish in fishList)
        {
            if (MyGame.CheckMouseInRectClick(fish.buyToUnlock))
            {
                if (_level.currencySystem.getMoney() >= fish.FishPrice)
                {
                    buyFish(fish);
                }
                else
                {
                    cannotBuyFish(fish);
                }
            }
        }
    }
    //------------------------------------------------------------------------
    //                          closeShop
    //------------------------------------------------------------------------
    private void closeShop()
    {
        if (MyGame.CheckMouseInRectClick(close))
        {
            visible = false;
            inv.DeselectShop();
        }
    }
    //------------------------------------------------------------------------
    //                          cannotBuyFish
    //------------------------------------------------------------------------
    private void cannotBuyFish(Fish fish)
    {
        if (fish.isUnlocked == false)
        {
            if (_option.isSoundPlaying)
            {
                notEnpughMoneySound.Play();
            }
            AddChild(notEnoughMoney);
            notEnoughMoney.x = fish.buyToUnlock.x;
            notEnoughMoney.y = fish.buyToUnlock.y;
        }
    }
    //------------------------------------------------------------------------
    //                          buyFish
    //------------------------------------------------------------------------
    private void buyFish(Fish fish)
    {
        if (fish.isUnlocked == false)
        {
            if (_option.isSoundPlaying)
            {
                buyFishSound.Play();
            }
            _level.currencySystem.RemoveMoney(fish.FishPrice);
            fish.Unlock();
            _level.journal.AddFish(fish);
            AddChild(fish.soldOut);
            fish.soldOut.x = fish.buyToUnlock.x;
            fish.soldOut.y = fish.buyToUnlock.y - 10;
            RemoveChild(fish.buyToUnlock);
            RemoveChild(fish.fishNameAndPrice);
            if (_level.tutorial.count == 6)
            {
                _level.tutorial.count = 7;
            }
        }
    }
}

