using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    public class Scene : GameObject
    {
        int tankIsDirty = 75;
        int dirtTimer = 20000;


        Random soundRand;

        Sprite tank, downArrow;
        Level level;
        int timer = 20000;
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
        int spongeTimer;
        bool isBought = false;
        bool isOneFishShown = false;
        bool isPlayingMusic;
        public bool passedTutorial = false;
        bool spongeSoundsPlaying;
        Sprite clickToBuy;
        Tutorial _tutorial;
        Options _option;
        //Sound cleanDirtWithSponge;
        Sound[] spongeSounds;
        SoundChannel spongeClean;
        Sound repairAquarium;
        Sound makeFoodSound;
        Sound openShop;
        SoundChannel openShopSoundChannel;
        Sound sceneMusic;
        SoundChannel sceneChannel;

        public Scene(string path, CurrencySystem currency, Level level, int scene,Options option, int price = 400) : base()
        {
            _option = option;
            if (scene == 1 && !passedTutorial)
            {
                _tutorial = new Tutorial(new Vec2(game.width / 2 - 300, game.height / 2), this);
            }
            if (scene == 2)
            {
                sceneMusic = new Sound("seaTank.mp3");
            }

            this.scene = scene;
            _currency = currency;
            visible = false;
            spongeSoundsPlaying = false;
            this.level = level;
            isActive = false;
            canMakeFood = true;
            tank = new Sprite(path);
            tank.width = game.width;
            tank.height = game.height;
            downArrow = new Sprite("downarrow.png");
            isPlayingMusic = false;
            downArrow.SetXY(game.width / 2, game.height - 200);
            downArrow.SetScaleXY(0.2f);
            foodList = new List<Food>();
            AddChildAt(tank, 0);
            AddChild(downArrow);
            priceOfAquarium = price;
            soundRand = new Random();
            fishListPerScene = new List<Fish>();
            DisplayFishInScene fishes = new DisplayFishInScene(scene, foodList, fishListPerScene);
            sponge = new Sponge(this);
            inv = new Inventory();
            shop = new Shop(fishListPerScene, level, inv, _option);
            for (int i = 0; i < 30; i++)
            {
                Dirt dirt = new Dirt(ref cleanMeter);
                sponge.addDirt(dirt);
                AddChild(dirt);
            }
            clickToBuy = new Sprite("buy_button.png");
            clickToBuy.SetXY(game.width / 2 - clickToBuy.width / 2, game.height / 2 - clickToBuy.height / 2);

            AddChild(clickToBuy);
            spongeTimer = 791;
            foodCan = new Sprite("fish_food_can.png");
            foodCan.SetOrigin(foodCan.width / 4, 0);
            foodCan.width /= 5;
            foodCan.height /= 5;

            AddChild(shop);
            shop.visible = false;
            spongeSounds = new Sound[3];
            spongeSounds[0] = new Sound("sponge_use_sound.wav", false, true);
            spongeSounds[1] = new Sound("sponge_use_sound_high.wav", false, true);
            spongeSounds[2] = new Sound("sponge_use_sound_low.wav", false, true);
            //cleanDirtWithSponge = new Sound("sponge_use_sound.wav", true, true);

            repairAquarium = new Sound("repair_aquarium_sound.wav", false, true);
            makeFoodSound = new Sound("fish_food_pick_sound.wav", false, true);
            openShop = new Sound("opening_journal_shop_sound.wav", false, true);
            if (_tutorial != null)
            {
                AddChild(_tutorial);
                Console.WriteLine(_tutorial.Index);
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
                if (foodList.Count < 10)
                {
                    Food food = new Food();
                    AddChildAt(food, 1);
                    foodList.Add(food);
                    if (_option.isSoundPlaying)
                    {
                        makeFoodSound.Play();
                    }
                }
            }
        }
        void Update()
        {
            if (level.myGame.isPlaying)
            {
                if (isActive)
                {
                    //Console.WriteLine(timer);
                    if (isBought == true)
                    {
                        canMakeFood = true;
                        addFish();
                        handleMoney();
                        if (isOneFishShown == true)
                        {
                            makeDirt();
                        }
                        if (isOneFishShown == true)
                        {
                            makeDirt();
                        }
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
                                if (scene == 1 && _tutorial.isVisible && _tutorial.count == 6)
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
                                if (scene == 1 && _tutorial.isVisible && _tutorial.count == 5)
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
                        if (scene == 1 && sponge.dirtList.Count <= 0 && !isOneFishShown && _tutorial.count == 4)
                        {
                            _tutorial.count = 5;
                        }
                        if (spongeSoundsPlaying&&_option.isSoundPlaying)
                        {
                            int rand = soundRand.Next(0, spongeSounds.Length - 1);
                            spongeTimer -= Time.deltaTime;
                            if (spongeTimer <= 0)
                            {
                                spongeClean = spongeSounds[rand].Play();
                                spongeTimer = 791;
                            }
                        }

                    }
                    else
                    {
                        goBack();
                        buyAquarium();
                    }

                }
                else
                {
                    if (isOneFishShown == true)
                    {
                        makeDirt();
                    }
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
                    if (_option.isSoundPlaying)
                    {
                        repairAquarium.Play();
                    }
                    if (scene == 1 && _tutorial.isVisible && _tutorial.count == 1)
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

                    if (fish.hungerMeterForFish > fish.isFishHungry && cleanMeter < tankIsDirty)
                    {
                        if (fish.FishProgrss >= fish.maxProgress)
                        {

                            for (int i = 0; i < fish.HowManyCoins; i++)
                            {
                                Coin coin = new Coin(fish, level, _option);
                                AddChildAt(coin, 1);
                            }
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
                SetChildIndex(dirt, 2);
                timer = dirtTimer;
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

                spongeSoundsPlaying = true;
                AddChild(sponge);
                spongeOnScreen = true;
            }
        }
        void RemoveSponge()
        {
            if (spongeOnScreen == true)
            {
                if (spongeClean != null&&_option.isSoundPlaying)
                {
                    spongeClean.Stop();
                }
                RemoveChild(sponge);
                spongeOnScreen = false;
                spongeSoundsPlaying = false;
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
                if (_option.isSoundPlaying)
                {
                    openShopSoundChannel = openShop.Play();
                }
                shop.visible = true;
                isShopDisplayed = true;
            }

        }
        void RemoveShop()
        {
            if (isShopDisplayed == true)
            {
                RemoveChild(shop);
                if (_option.isSoundPlaying)
                {
                    openShopSoundChannel.Stop();
                }
                shop.visible = false;
                isShopDisplayed = false;
            }
        }

        public int GetScene()
        {
            return scene;
        }

    }

}
