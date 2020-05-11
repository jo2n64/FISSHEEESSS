using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    public class Scene : GameObject
    {
        Sprite tank, downArrow;
        Level level;
        int timer = 1000;
        public bool isActive;
        bool canMakeFood;
        Sponge sponge;
        Sprite foodCan;
        public List<Food> foodList;
        List<Fish> fishListPerScene;
        Shop shop;
        Inventory inv;
        public CurrencySystem _currency;
        int cleanMeter = 0;
        int scene;
        int priceOfAquarium;
        bool isBought = false;
        bool isOneFishShown = false;
        Sprite clickToBuy;
        Tutorial _tutorial;
        Sound cleanDirtWithSponge;
        SoundChannel spongeClean;
        Sound repairAquarium;
        Sound makeFoodSound;
        Sound openShop;
        SoundChannel openShopSoundChannel;

        public Scene(string path, CurrencySystem currency, Level level, int scene, int price = 400, Tutorial tutorial = null) : base()
        {
            if (scene == 1)
            {
                _tutorial = new Tutorial(new Vec2(game.width / 2 - 300, game.height / 2), this);
            }
            this.scene = scene;
            _currency = currency;
            visible = false;
            this.level = level;
            isActive = false;
            canMakeFood = true;
            tank = new Sprite(path);
            tank.width = game.width;
            tank.height = game.height;
            downArrow = new Sprite("downarrow.png");

            downArrow.SetXY(game.width / 2, game.height - 200);
            downArrow.SetScaleXY(0.2f);
            foodList = new List<Food>();
            AddChildAt(tank, 0);
            AddChild(downArrow);
            priceOfAquarium = price;

            fishListPerScene = new List<Fish>();
            DisplayFishInScene fishes = new DisplayFishInScene(scene, foodList, fishListPerScene);
            sponge = new Sponge(this);
            shop = new Shop(fishListPerScene, level);
            inv = new Inventory();
            clickToBuy = new Sprite("checkers.png");
            clickToBuy.width = 200;
            clickToBuy.height = 200;
            clickToBuy.y += 300;
            AddChild(clickToBuy);

            foodCan = new Sprite("fish_food.png");
            foodCan.SetOrigin(foodCan.width / 4, 0);
            foodCan.width /= 5;
            foodCan.height /= 5;
            for (int i = 0; i < 30; i++)
            {
                Dirt dirt = new Dirt(ref cleanMeter);
                sponge.addDirt(dirt);
                AddChild(dirt);
            }
            AddChild(shop);
            shop.visible = false;
            cleanDirtWithSponge = new Sound("sponge_use_sound.wav", true, true);

            repairAquarium = new Sound("repair_aquarium_sound.wav", false, true);
            makeFoodSound = new Sound("fish_food_pick_sound.wav", false, true);
            openShop = new Sound("opening_journal_shop_sound.wav", false, true);
            if (_tutorial != null)
            {
                AddChild(_tutorial);
            }

        }
        void addFish()
        {
            foreach (Fish fish in fishListPerScene)
            {
                if (fish.isUnlocked == true)
                {
                    if (fish.isAdded == false)
                    {
                        AddChildAt(fish, 1);
                        fish.isAdded = true;
                        if (isOneFishShown == false)
                        {
                            isOneFishShown = true;
                        }
                    }
                }
            }
        }
        void makeFood()
        {
            if (Input.GetMouseButtonDown(button: 0) && canMakeFood)
            {
                Food food = new Food();
                AddChildAt(food, 1);
                foodList.Add(food);
                makeFoodSound.Play();
            }
        }
        void Update()
        {

            if (isActive)
            {
                if (isBought == true)
                {
                    canMakeFood = true;
                    addFish();

                    switch (inv.id)
                    {
                        case Inventory.Food:
                            if (inv.checkIfItemIsOverlapped() == false)
                            {
                                makeFood();
                            }
                            displayFoodCan();
                            moveFoodCan();
                            RemoveShop();
                            RemoveSponge();
                            if(scene == 1 && _tutorial.isVisible && _tutorial.count == 6)
                            {
                                _tutorial.count = 7;
                            }
                            break;
                        case Inventory.Sponge:
                            displaySponge();
                            RemoveShop();
                            RemoveFoodCan();
                            if (scene == 1 && _tutorial.isVisible && _tutorial.count == 3)
                            {
                                _tutorial.count = 4;
                            }
                            break;
                        case Inventory.Shop:
                            displayShop();
                            RemoveSponge();
                            RemoveFoodCan();
                            if(scene == 1 && _tutorial.isVisible && _tutorial.count == 5)
                            {
                                _tutorial.count = 6;
                            }
                            break;
                        case 0:
                            RemoveShop();
                            RemoveSponge();
                            handleMoney();
                            RemoveFoodCan();
                            goBack();
                            break;
                    }
                    if(sponge.dirtList.Count <= 0 && !isOneFishShown && _tutorial.count == 4)
                    {
                        _tutorial.count = 5;
                    }
                    {

                    }
                    if (isOneFishShown == true)
                    {
                        makeDirt();
                    }

                }
                else
                {
                    goBack();
                    buyAquarium();
                }


            }

        }

        void buyAquarium()
        {

            if (MyGame.CheckMouseInRectClick(clickToBuy))
            {
                if (level.currencySystem.money >= priceOfAquarium)
                {
                    clickToBuy.LateDestroy();
                    isBought = true;
                    AddChild(inv);
                    level.currencySystem.RemoveMoney(priceOfAquarium);
                    repairAquarium.Play();
                    if(_tutorial.isVisible && scene == 1 && _tutorial.count == 1)
                    {
                        _tutorial.count = 2;
                    }

                }

            }
        }

        void handleMoney()
        {
            foreach (Fish fish in fishListPerScene)
            {
                if (fish.isUnlocked == true)
                {

                    if (fish.isFishHungry > 3000 && cleanMeter < 75)
                    {
                        if (fish.FishProgrss >= 3000)
                        {
                            Coin coin = new Coin(fish, level);
                            AddChildAt(coin, 1);
                            fish.FishProgrss = 0;
                        }
                        else
                        {
                            fish.FishProgrss += Time.deltaTime;
                        }
                    }
                }
            }

        }
        void makeDirt()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Dirt dirt = new Dirt(ref cleanMeter);
                sponge.addDirt(dirt);
                AddChild(dirt);
                timer = 1000;
            }
        }

        public void removeDirtConsequence(Dirt dirt)
        {
            cleanMeter -= dirt.cleanImpact;
        }

        void goBack()
        {
            if (MyGame.CheckMouseInRectClick(downArrow))
            {
                isActive = false;
                level.isInScene = false;
                visible = false;
                if (HasChild(shop))
                {
                    RemoveChild(shop);
                }
            }
        }

        bool spongeOnScreen = false;
        void displaySponge()
        {
            if (spongeOnScreen == false)
            {
                spongeClean = cleanDirtWithSponge.Play();
                AddChild(sponge);
                spongeOnScreen = true;
            }
        }
        void RemoveSponge()
        {
            if (spongeOnScreen == true)
            {
                spongeClean.Stop();
                RemoveChild(sponge);
                spongeOnScreen = false;
            }


        }
        void moveFoodCan()
        {
            foodCan.x = Input.mouseX;
            foodCan.y = Input.mouseY;
        }
        void displayFoodCan()
        {
            if (isFoodDisplayed == false)
            {
                AddChild(foodCan);
                isFoodDisplayed = true;
            }
        }
        void RemoveFoodCan()
        {
            if (isFoodDisplayed == true)
            {
                RemoveChild(foodCan);
                isFoodDisplayed = false;
            }


        }
        bool isShopDisplayed = false;
        bool isFoodDisplayed = false;
        void displayShop()
        {
            AddChild(shop);

            if (isShopDisplayed == false)
            {
                openShopSoundChannel = openShop.Play();
                shop.visible = true;
                isShopDisplayed = true;
            }

        }
        void RemoveShop()
        {
            if (isShopDisplayed == true)
            {
                RemoveChild(shop);
                openShopSoundChannel.Stop();
                shop.visible = false;
                isShopDisplayed = false;
            }
        }
    }
}
