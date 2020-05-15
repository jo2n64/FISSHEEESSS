using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Scene : GameObject
{
    public bool isActive;

    private int tankIsDirty = 75;
    private int maxDirtTimer = 20000;
    private int dirtTimer = 20000;
    private int cleanMeter = 0;

    private Random soundRand;

    private Sprite tank, downArrow;
    private Level level;
    private bool canMakeFood;
    private Sponge sponge;
    private Sprite foodCan;
    private List<Food> foodList;
    private List<Fish> fishListPerScene;
    private Shop shop;
    private Inventory inv;

    private int scene;
    private int priceOfAquarium;
    private int spongeTimer;

    private bool isBought = false;
    private bool isOneFishShown = false;
    private bool isActivated;
    private bool spongeSoundsPlaying;
    private bool spongeOnScreen = false;
    private bool isShopDisplayed = false;
    private bool isFoodDisplayed = false;

    private Sprite clickToBuyAquarium;
    private Sprite notEnoughMoney;

    private Options _option;
    private Sound[] spongeSounds;
    private SoundChannel spongeClean;
    private Sound repairAquarium;
    private Sound makeFoodSound;
    private Sound openShop;
    private SoundChannel openShopSoundChannel;
    Sound sceneMusic;
    private Sound notEnpughMoneyToBuyFish;

    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Scene(string path, Level level, int scene, Options option, int price = 400) : base()
    {
        if (scene == 2)
        {
            sceneMusic = new Sound("seaTank.mp3");
        }
        _option = option;
        this.scene = scene;
        this.level = level;
        priceOfAquarium = price;
        initializeVariables();
        makeTank(path);
        makeDownArrow();
        DisplayFishInScene fishes = new DisplayFishInScene(scene, foodList, fishListPerScene);
        makeBuyAquariumButton(path);
        makeItemsAndInventory(level);
        makeInitialDirt();
        makeSounds();
    }
    //------------------------------------------------------------------------
    //                          makeItemsAndInventory
    //------------------------------------------------------------------------
    private void makeItemsAndInventory(Level level)
    {
        inv = new Inventory();
        sponge = new Sponge(this);
        spongeTimer = 791;
        makeFoodCan();
        makeShopInScene(level);
    }
    //------------------------------------------------------------------------
    //                          makeBuyAquariumButton
    //------------------------------------------------------------------------
    private void makeBuyAquariumButton(string path)
    {
        clickToBuyAquarium = new Sprite(path + "-buy.png");
        clickToBuyAquarium.SetXY(game.width / 2 - clickToBuyAquarium.width / 2, game.height / 2 - clickToBuyAquarium.height);
        AddChild(clickToBuyAquarium);
    }
    //------------------------------------------------------------------------
    //                          makeTank
    //------------------------------------------------------------------------
    private void makeTank(string path)
    {
        tank = new Sprite(path + ".png");
        tank.width = game.width;
        tank.height = game.height;
        AddChildAt(tank, 0);
    }
    //------------------------------------------------------------------------
    //                          makeDownArrow
    //------------------------------------------------------------------------
    private void makeDownArrow()
    {
        downArrow = new Sprite("downarrow.png");
        downArrow.SetXY(game.width / 2, game.height - 300);
        downArrow.SetScaleXY(0.5f);
        AddChild(downArrow);
    }
    //------------------------------------------------------------------------
    //                          initializeVariables
    //------------------------------------------------------------------------
    private void initializeVariables()
    {
        isActive = false;
        canMakeFood = true;
        visible = false;
        spongeSoundsPlaying = false;
        isActivated = false;
        foodList = new List<Food>();
        fishListPerScene = new List<Fish>();
        notEnoughMoney = new Sprite("no_money.png");
    }
    //------------------------------------------------------------------------
    //                          makeSounds
    //------------------------------------------------------------------------
    private void makeSounds()
    {
        notEnpughMoneyToBuyFish = new Sound("no_money_sound.wav", false, true);
        soundRand = new Random();
        spongeSounds = new Sound[3];
        spongeSounds[0] = new Sound("sponge_use_sound.wav", false, true);
        spongeSounds[1] = new Sound("sponge_use_sound_high.wav", false, true);
        spongeSounds[2] = new Sound("sponge_use_sound_low.wav", false, true);
        repairAquarium = new Sound("repair_aquarium_sound.wav", false, true);
        makeFoodSound = new Sound("fish_food_pick_sound.wav", false, true);
        openShop = new Sound("opening_journal_shop_sound.wav", false, true);
    }
    //------------------------------------------------------------------------
    //                          makeShopInScene
    //------------------------------------------------------------------------
    private void makeShopInScene(Level level)
    {
        shop = new Shop(fishListPerScene, level, inv, _option);
        shop.visible = false;
        AddChild(shop);
    }
    //------------------------------------------------------------------------
    //                          makeFoodCan
    //------------------------------------------------------------------------
    private void makeFoodCan()
    {
        foodCan = new Sprite("fish_food_can.png");
        foodCan.SetOrigin(foodCan.width / 4, 0);
        foodCan.width /= 5;
        foodCan.height /= 5;
    }
    //------------------------------------------------------------------------
    //                          makeInitialDirt
    //------------------------------------------------------------------------
    private void makeInitialDirt()
    {
        for (int i = 0; i < 50; i++)
        {
            Dirt dirt = new Dirt(ref cleanMeter);
            sponge.addDirt(dirt);
            AddChild(dirt);
        }
    }
    //------------------------------------------------------------------------
    //                          addFish
    //------------------------------------------------------------------------
    private void addFish()
    {
        foreach (Fish fish in fishListPerScene)
        {
            if (fish.GetIsUnlocked() == true)
            {
                if (fish.GetIsAdded() == false)
                {
                    AddChildAt(fish, 1);
                    fish.SetIsAddedToTrue();
                    if (isOneFishShown == false)
                    {
                        isOneFishShown = true;
                    }
                }
            }
        }
    }
    //------------------------------------------------------------------------
    //                          makeFood
    //------------------------------------------------------------------------
    private void makeFood()
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
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        if (level.GetMyGame().GetIsPlaying())
        {

            if (isActivated)
            {
                actionsIfSceneIsActive();

            }
            else
            {
                actionsIfSceneIsNotActive();
            }
        }
        if (isActive)
        {
            isActivated = true;
        }
    }
    //------------------------------------------------------------------------
    //                          actionsIfSceneIsActive
    //------------------------------------------------------------------------
    private void actionsIfSceneIsActive()
    {
        if (isBought == true)
        {
            actionsPossibleIfTankIsUnlocked();
        }
        else
        {
            actionsPossibleIfTankIsLocked();
        }
    }
    //------------------------------------------------------------------------
    //                          actionsIfSceneIsNotActive
    //------------------------------------------------------------------------
    private void actionsIfSceneIsNotActive()
    {
        inv.Deselect();
        handleMoney();
        if (isOneFishShown == true)
        {

            makeDirt();
        }
    }
    //------------------------------------------------------------------------
    //                          actionsPossibleIfTankIsLocked
    //------------------------------------------------------------------------
    private void actionsPossibleIfTankIsLocked()
    {
        goBack();
        buyAquarium();
    }
    //------------------------------------------------------------------------
    //                          actionsPossibleIfTankIsUnlocked
    //------------------------------------------------------------------------
    private void actionsPossibleIfTankIsUnlocked()
    {
        canMakeFood = true;
        addFish();
        handleMoney();
        if (isOneFishShown == true)
        {
            makeDirt();
        }
        handleItemSelected();
        handleSpongeSound();
        if (level.tutorial.GetCount() == 3 && sponge.getNumerOfElementInDirtList() <= 0)
        {
            level.tutorial.SetCount(4);
        }
    }
    //------------------------------------------------------------------------
    //                          handleSpongeSound
    //------------------------------------------------------------------------
    private void handleSpongeSound()
    {
        if (spongeSoundsPlaying && _option.isSoundPlaying)
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
    //------------------------------------------------------------------------
    //                          handleItemSelected
    //------------------------------------------------------------------------
    private void handleItemSelected()
    {
        switch (inv.getID())
        {
            case Inventory.Food:
                foodActions();
                break;
            case Inventory.Sponge:
                spongeActions();
                break;
            case Inventory.Shop:
                shopActions();
                break;
            case Inventory.Deselected:
                noItemActions();
                break;
        }
    }
    //------------------------------------------------------------------------
    //                          noItemActions
    //------------------------------------------------------------------------
    private void noItemActions()
    {
        RemoveShop();
        RemoveSponge();
        RemoveFoodCan();
        goBack();
    }
    //------------------------------------------------------------------------
    //                          shopActions
    //------------------------------------------------------------------------
    private void shopActions()
    {
        displayShop();
        RemoveSponge();
        RemoveFoodCan();
        if (level.tutorial.GetCount() == 4)
        {
            level.tutorial.SetCount(5);
        }
    }
    //------------------------------------------------------------------------
    //                          spongeActions
    //------------------------------------------------------------------------
    private void spongeActions()
    {
        displaySponge();
        RemoveShop();
        RemoveFoodCan();
    }
    //------------------------------------------------------------------------
    //                          foodActions
    //------------------------------------------------------------------------
    private void foodActions()
    {
        if (inv.checkIfItemIsOverlapped() == false)
        {
            makeFood();
        }
        displayFoodCan();
        moveFoodCan();
        RemoveShop();
        RemoveSponge();
        if (level.tutorial.GetCount() == 8)
        {
            level.tutorial.SetCount(9);
        }
    }
    //------------------------------------------------------------------------
    //                          buyAquarium
    //------------------------------------------------------------------------
    private void buyAquarium()
    {
        if (MyGame.CheckMouseInRectClick(clickToBuyAquarium))
        {
            if (level.currencySystem.getMoney() >= priceOfAquarium)
            {
                aquariumIsBought();
            }
            else
            {
                aquariumIsNotBought();
            }

        }
    }
    //------------------------------------------------------------------------
    //                          aquariumIsNotBought
    //------------------------------------------------------------------------
    private void aquariumIsNotBought()
    {
        if (_option.isSoundPlaying)
        {
            notEnpughMoneyToBuyFish.Play();
        }
        AddChild(notEnoughMoney);
        notEnoughMoney.x = clickToBuyAquarium.x;
        notEnoughMoney.y = clickToBuyAquarium.y;
        notEnoughMoney.width = clickToBuyAquarium.width;
        notEnoughMoney.height = clickToBuyAquarium.height;
    }
    //------------------------------------------------------------------------
    //                          aquariumIsBought
    //------------------------------------------------------------------------
    private void aquariumIsBought()
    {
        clickToBuyAquarium.LateDestroy();
        isBought = true;
        AddChild(inv);
        level.currencySystem.RemoveMoney(priceOfAquarium);
        if (_option.isSoundPlaying)
        {
            repairAquarium.Play();
        }
        if (level.tutorial.GetCount() == 2)
        {
            level.tutorial.SetCount(3);
        }
    }
    //------------------------------------------------------------------------
    //                          handleMoney
    //------------------------------------------------------------------------
    private void handleMoney()
    {
        foreach (Fish fish in fishListPerScene)
        {
            if (fish.GetIsUnlocked() == true)
            {
                if (fish.GetHungerMeter() > fish.GetIsFishHungry() && cleanMeter < tankIsDirty)
                {
                    handleFishProgress(fish);
                }
            }
        }
    }
    //------------------------------------------------------------------------
    //                          handleFishProgress
    //------------------------------------------------------------------------
    private void handleFishProgress(Fish fish)
    {
        if (fish.GetFishProgress() >= fish.GetMaxProgress())
        {
            for (int i = 0; i < fish.GetNumberOfCoinsProduced(); i++)
            {
                Coin coin = new Coin(fish, level, _option, fish.GetFishName() + "-money.png");
                AddChildAt(coin, 1);
            }
            fish.SetFishProgressToZero();
        }
        else
        {
            fish.IncreaseFishProgress();
        }
    }
    //------------------------------------------------------------------------
    //                          makeDirt
    //------------------------------------------------------------------------
    private void makeDirt()
    {
        dirtTimer -= Time.deltaTime;

        if (dirtTimer <= 0)
        {
            Dirt dirt = new Dirt(ref cleanMeter);
            sponge.addDirt(dirt);
            AddChild(dirt);
            SetChildIndex(dirt, 2);
            dirtTimer = maxDirtTimer;
        }
    }
    //------------------------------------------------------------------------
    //                          removeDirtConsequence
    //------------------------------------------------------------------------
    public void removeDirtConsequence(Dirt dirt)
    {
        cleanMeter -= dirt.getCleanImpact();
    }
    //------------------------------------------------------------------------
    //                          goBack
    //------------------------------------------------------------------------
    private void goBack()
    {
        if (MyGame.CheckMouseInRectClick(downArrow) && !level.journal.GetInWindow())
        {
            isActive = false;
            isActivated = false;
            level.isInScene = false;
            visible = false;
            if (HasChild(shop))
            {
                RemoveChild(shop);
            }
            if (HasChild(notEnoughMoney))
            {
                RemoveChild(notEnoughMoney);
            }
        }
    }
    //------------------------------------------------------------------------
    //                          displaySponge
    //------------------------------------------------------------------------
    private void displaySponge()
    {
        if (spongeOnScreen == false)
        {

            spongeSoundsPlaying = true;
            AddChild(sponge);
            spongeOnScreen = true;
        }
    }
    //------------------------------------------------------------------------
    //                          RemoveSponge
    //------------------------------------------------------------------------
    private void RemoveSponge()
    {
        if (spongeOnScreen == true)
        {
            if (spongeClean != null && _option.isSoundPlaying)
            {
                spongeClean.Stop();
            }
            RemoveChild(sponge);
            spongeOnScreen = false;
            spongeSoundsPlaying = false;
        }
    }
    //------------------------------------------------------------------------
    //                          moveFoodCan
    //------------------------------------------------------------------------
    private void moveFoodCan()
    {
        foodCan.x = Input.mouseX;
        foodCan.y = Input.mouseY;
    }
    //------------------------------------------------------------------------
    //                          displayFoodCan
    //------------------------------------------------------------------------
    private void displayFoodCan()
    {
        if (isFoodDisplayed == false)
        {
            AddChild(foodCan);
            isFoodDisplayed = true;
        }
    }
    //------------------------------------------------------------------------
    //                          RemoveFoodCan
    //------------------------------------------------------------------------
    private void RemoveFoodCan()
    {
        if (isFoodDisplayed == true)
        {
            RemoveChild(foodCan);
            isFoodDisplayed = false;
        }
    }
    //------------------------------------------------------------------------
    //                          displayShop
    //------------------------------------------------------------------------
    private void displayShop()
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
    //------------------------------------------------------------------------
    //                          RemoveShop
    //------------------------------------------------------------------------
    private void RemoveShop()
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
    //------------------------------------------------------------------------
    //                          GetScene
    //------------------------------------------------------------------------
    public int GetScene()
    {
        return scene;
    }

}


