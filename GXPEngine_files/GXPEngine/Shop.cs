using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Shop : GameObject
{
    private List<Fish> fishList;
    private Level _level;
    private Sound buyFish;
    private Sound notEnpughMoneyToBuyFish;
    private Sprite close;
    private Sprite notEnoughMoney;
    private Inventory inv;
    private Options _option;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Shop(List<Fish> fishListOfTank, Level level, Inventory inventory, Options option)
    {
        _option = option;
        close = new Sprite("close_button.png");
        notEnoughMoney = new Sprite("no_money.png");
        notEnoughMoney.width /= 10;
        notEnoughMoney.height /= 10;
        close.width /= 5;
        close.height /= 5;
        _level = level;
        fishList = fishListOfTank;
        buyFish = new Sound("buying_fish_sound.mp3", false, true);
        notEnpughMoneyToBuyFish = new Sound("no_money_sound.wav", false, true);
        makeShop();
        inv = inventory;
        AddChild(close);
    }
    //------------------------------------------------------------------------
    //                          makeShop
    //------------------------------------------------------------------------
    private void makeShop()
    {
        int i = 1;
        int j = 1;
        makeBackground();
        foreach (Fish fish in fishList)
        {
            if (fish.GetIsUnlocked() == false)
            {
                makeIconsAndButtons(i, j, fish);
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
    //                          makeIconsAndButtons
    //------------------------------------------------------------------------
    private void makeIconsAndButtons(int i, int j, Fish fish)
    {
        makeBuyButton(i, j, fish);
        makeFishNameAndPriceIcon(i, j, fish);
        makeIconsForFish(i, j, fish);
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
    //                          makeFishNameAndPriceIcon
    //------------------------------------------------------------------------
    private void makeFishNameAndPriceIcon(int i, int j, Fish fish)
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
        Sprite fishIcon = new Sprite(fish.GetFishName() + "-icon.png");
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
            handleBuyingFish();
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
    //                          handleBuyingFish
    //------------------------------------------------------------------------
    private void handleBuyingFish()
    {
        foreach (Fish fish in fishList)
        {
            if (MyGame.CheckMouseInRectClick(fish.buyToUnlock))
            {

                if (_level.currencySystem.money >= fish.GetFishPrice())
                {
                    fishIsBought(fish);
                }
                else
                {
                    fishIsNotBought(fish);
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
            if (_level.tutorial.count == 7)
            {
                _level.tutorial.count = 8;
            }

        }
    }
    //------------------------------------------------------------------------
    //                          fishIsNotBought
    //------------------------------------------------------------------------
    private void fishIsNotBought(Fish fish)
    {
        if (fish.GetIsUnlocked() == false)
        {
            if (_option.isSoundPlaying)
            {
                notEnpughMoneyToBuyFish.Play();
            }
            AddChild(notEnoughMoney);
            notEnoughMoney.x = fish.buyToUnlock.x;
            notEnoughMoney.y = fish.buyToUnlock.y;
        }
    }
    //------------------------------------------------------------------------
    //                          fishIsBought
    //------------------------------------------------------------------------
    private void fishIsBought(Fish fish)
    {
        if (fish.GetIsUnlocked() == false)
        {
            if (_option.isSoundPlaying)
            {
                buyFish.Play();
            }
            _level.currencySystem.RemoveMoney(fish.GetFishPrice());
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

