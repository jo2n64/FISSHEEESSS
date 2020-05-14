using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Shop:GameObject
    {
        List<Fish> fishList;
        Level _level;
        Sound buyFish;
        Sound notEnpughMoneyToBuyFish;
        Sprite close;
        Sprite notEnoughMoney;
        Inventory inv;
        Options _option;
        public Shop(List<Fish> fishListOfTank,Level level, Inventory inventory,Options option)
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
        void makeShop()
        {
            int i = 1;
            int j = 1;
            makeBackground();
            foreach (Fish fish in fishList)
            {
                if (fish.GetIsUnlocked() == false)
                {
                    AddChild(fish.buyToUnlock);
                    fish.buyToUnlock.x = i * game.width / 4 - fish.buyToUnlock.width / 2;
                    fish.buyToUnlock.y = j * game.height / 3+105;

                    AddChild(fish.fishNameAndPrice);
                    fish.fishNameAndPrice.x = i * game.width / 4 - fish.fishNameAndPrice.width / 2;
                    fish.fishNameAndPrice.y = j * game.height / 3 + 10;

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

        private void makeIconsForFish(int i, int j, Fish fish)
        {
            Sprite fishIcon = new Sprite(fish.GetFishName() + "-icon.png");
            AddChild(fishIcon);
            fishIcon.SetOrigin(fishIcon.width / 2, fishIcon.height / 2);
            fishIcon.width /= 8;
            fishIcon.height /= 8;
            fishIcon.x = i * game.width / 4;
            fishIcon.y = j * game.height / 3 - fishIcon.height / 2+20;
        }

        private void makeBackground()
        {
            Sprite backgroundShop = new Sprite("shop_shelf.png");
            AddChild(backgroundShop);
            backgroundShop.width = game.width - game.width / 5;
            backgroundShop.height = game.height - game.height / 5;
            backgroundShop.x = game.width / 10;
            backgroundShop.y = game.height / 10-20;
            close.x = backgroundShop.width+ game.width / 10-close.width-20;
            close.y = game.height / 10;
        }

        void Update()
        {
            if (visible == true)
            {
                foreach (Fish fish in fishList)
                {

                    if (MyGame.CheckMouseInRectClick(fish.buyToUnlock))
                    {

                        if (_level.currencySystem.money >= fish.GetFishPrice())
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
                                fish.soldOut.y = fish.buyToUnlock.y-10;
                                RemoveChild(fish.buyToUnlock);
                                RemoveChild(fish.fishNameAndPrice);
                                if(_level.tutorial.count == 6)
                                {
                                    _level.tutorial.count = 7;
                                }
                            }

                        }
                        else
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
                    }

                }

                if (MyGame.CheckMouseInRectClick(close))
                {
                    visible = false;
                    inv.DeselectShop();
                    if(_level.tutorial.count == 7)
                    {
                        _level.tutorial.count = 8;
                    }

                }
            }
            else
            {
                if (HasChild(notEnoughMoney))
                {
                    RemoveChild(notEnoughMoney);
                }
            }
        }

    }
}
