using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Fish : AnimationSprite
{
    public Sprite buyToUnlock;
    public Sprite soldOut;
    public Sprite fishNameAndPrice;

    private List<Food> foodList;
    private bool isAdded = false;
    private bool isUnlocked = false;

    private int hungerMeterForFish = 0;
    private int isFishHungry;
    private int FishProgrss = 0;
    private int maxProgress;
    private int FishPrice;
    private int HowManyCoins;
    private int coinValue;

    private Sprite hungerIcon;
    private Food currentFood;
    private Vec2 _position;
    private Vec2 velocity;
    private Vec2 currentPoint = new Vec2(0, 0);
    private int increaseFood = 30000;
    private int frameTimer;
    private int offset = 100;
    private string description, type;
    private string fishName;

    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Fish(List<Food> _foodList, int frames, string type, string fishName, string description, int fishMaxProgress = 3000, int hungerMeter = 3000, int fishPrice = 200, int amountOfCoins = 3, int ValueOfCoin = 20) : base(fishName + ".png", frames, 1, frames)
    {
        coinValue = ValueOfCoin;
        HowManyCoins = amountOfCoins;
        FishPrice = fishPrice;
        isFishHungry = hungerMeter;
        maxProgress = fishMaxProgress;
        foodList = _foodList;
        this.type = type;
        this.fishName = fishName;
        this.description = description;
        SetOrigin(width / 2, height / 2);
        _position = new Vec2(Utils.Random(width, game.width - 200), Utils.Random(height, game.height - 200));
        ChangePosition();
        frameTimer = 100;
        makeIconsAndButtonsForFishAndShop(fishName);
    }
    //------------------------------------------------------------------------
    //                          makeIconsAndButtonsForFishAndShop
    //------------------------------------------------------------------------
    private void makeIconsAndButtonsForFishAndShop(string fishName)
    {
        makeHungerIcon();
        makeBuyButtonForShop();
        makeSoldOutButtonForShop();
        makeFishNameAndPriceIconForTheShop(fishName);
    }
    //------------------------------------------------------------------------
    //                          makeFishNameAndPriceIconForTheShop
    //------------------------------------------------------------------------
    private void makeFishNameAndPriceIconForTheShop(string fishName)
    {
        fishNameAndPrice = new Sprite(fishName + "-name.png");
        fishNameAndPrice.SetScaleXY(0.4f);
    }
    //------------------------------------------------------------------------
    //                          makeSoldOutButtonForShop
    //------------------------------------------------------------------------
    private void makeSoldOutButtonForShop()
    {
        soldOut = new Sprite("sold_out.png");
        soldOut.width /= 6;
        soldOut.height /= 6;
    }
    //------------------------------------------------------------------------
    //                          makeBuyButtonForShop
    //------------------------------------------------------------------------
    private void makeBuyButtonForShop()
    {
        buyToUnlock = new Sprite("buy_button.png");
        buyToUnlock.width /= 5;
        buyToUnlock.height /= 5;
    }
    //------------------------------------------------------------------------
    //                          makeHungerIcon
    //------------------------------------------------------------------------
    private void makeHungerIcon()
    {
        hungerIcon = new Sprite("hunger_icon.png");
        hungerIcon.width /= 3;
        hungerIcon.height /= 3;
        hungerIcon.x += 100;
        hungerIcon.y -= 100;
    }
    //------------------------------------------------------------------------
    //                          Unlock
    //------------------------------------------------------------------------
    public void Unlock()
    {
        isUnlocked = true;
    }
    //------------------------------------------------------------------------
    //                          isFoodPresent
    //------------------------------------------------------------------------
    private bool isFoodPresent()
    {
        if (foodList.Count == 0) return false;
        else return true;
    }
    //------------------------------------------------------------------------
    //                          UpdateScreenPosition
    //------------------------------------------------------------------------
    private void UpdateScreenPosition()
    {
        ChangePosition();
        MirrorIfNeded();
    }
    //------------------------------------------------------------------------
    //                          MirrorIfNeded
    //------------------------------------------------------------------------
    private void MirrorIfNeded()
    {
        if (velocity.x < 0)
        {
            Mirror(true, false);
        }
        else
        {
            Mirror(false, false);
        }
    }
    //------------------------------------------------------------------------
    //                          ChangePosition
    //------------------------------------------------------------------------
    private void ChangePosition()
    {
        x = _position.x;
        y = _position.y;
    }
    //------------------------------------------------------------------------
    //                          calcDistToPoint
    //------------------------------------------------------------------------
    private void calcDistToPoint()
    {
        if (currentPoint.x != 0 && currentPoint.y != 0)
        {
            velocity.SetXY(0, 0);
            makeFoodPoint();
            Vec2 deltaVector = currentPoint - _position;
            handleDistanceFisoToPoint(deltaVector);
        }
        else
        {
            makeNewPoint();
        }
    }
    //------------------------------------------------------------------------
    //                          handleDistanceFisoToPoint
    //------------------------------------------------------------------------
    private void handleDistanceFisoToPoint(Vec2 deltaVector)
    {
        if (deltaVector.Magnitude() <= 0.5f)
        {
            currentPoint.SetXY(0, 0);
            handleFishEatingTheFood();
        }
        else
        {
            deltaVector.Normalize();
            velocity += deltaVector;
        }
    }
    //------------------------------------------------------------------------
    //                          handleFishEatingTheFood
    //------------------------------------------------------------------------
    private void handleFishEatingTheFood()
    {
        if (isFoodPresent())
        {
            if (currentFood != null && hungerMeterForFish <= isFishHungry)
            {
                foodList.Remove(currentFood);
                currentFood.LateDestroy();
                hungerMeterForFish = increaseFood;
            }
        }
    }
    //------------------------------------------------------------------------
    //                          makeNewPoint
    //------------------------------------------------------------------------
    private void makeNewPoint()
    {
        makeRandomPoint();
        makeFoodPoint();
    }
    //------------------------------------------------------------------------
    //                          makeFoodPointmakeFoodPoint
    //------------------------------------------------------------------------
    private void makeFoodPoint()
    {
        if (isFoodPresent())
        {
            if (hungerMeterForFish <= isFishHungry)
            {
                calcNearestFood();
            }
        }
    }
    //------------------------------------------------------------------------
    //                          makeRandomPoint
    //------------------------------------------------------------------------
    private void makeRandomPoint()
    {
        if (_position.y + offset >= game.height - height)
        {
            currentPoint.SetXY(Utils.Random(width, game.width - width), Utils.Random(_position.y - offset, _position.y));
        }
        else if (_position.y - offset <= height)
        {
            currentPoint.SetXY(Utils.Random(width, game.width - width), Utils.Random(_position.y, _position.y + offset));
        }
        else
        {
            currentPoint.SetXY(Utils.Random(width, game.width - width), Utils.Random(_position.y - offset, _position.y + offset));
        }
    }
    //------------------------------------------------------------------------
    //                          calcNearestFood
    //------------------------------------------------------------------------
    private void calcNearestFood()
    {
        float minDist = game.width;
        foreach (Food food in foodList)
        {
            if ((food._position - _position).Magnitude() < minDist)
            {
                minDist = (food._position - _position).Magnitude();
                currentPoint = food._position;
                currentFood = food as Food;
            }
        }
    }
    //------------------------------------------------------------------------
    //                          move
    //------------------------------------------------------------------------
    private void move()
    {
        calcDistToPoint();
        _position += velocity;
        UpdateScreenPosition();
    }
    //------------------------------------------------------------------------
    //                          handleAnimation
    //------------------------------------------------------------------------
    private void handleAnimation()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer < 0)
        {
            NextFrame();
            frameTimer = 100;
        }
    }
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        if (hungerMeterForFish > 0)
        {
            hungerMeterForFish -= Time.deltaTime;
        }
        handleAnimation();
        move();
        displayHungerIcon();

    }
    //------------------------------------------------------------------------
    //                          displayHungerIcon
    //------------------------------------------------------------------------
    private void displayHungerIcon()
    {
        if (hungerMeterForFish <= isFishHungry)
        {
            AddChild(hungerIcon);
        }
        else RemoveChild(hungerIcon);
    }
    //------------------------------------------------------------------------
    //                          GetFishType
    //------------------------------------------------------------------------
    public string GetFishType()
    {
        return type;
    }
    //------------------------------------------------------------------------
    //                          GetFishName
    //------------------------------------------------------------------------
    public string GetFishName()
    {
        return fishName;
    }
    //------------------------------------------------------------------------
    //                          GetFishDescription
    //------------------------------------------------------------------------
    public string GetFishDescription()
    {
        return description;
    }
    //------------------------------------------------------------------------
    //                          GetCoinValue
    //------------------------------------------------------------------------
    public int GetCoinValue()
    {
        return coinValue;
    }
    //------------------------------------------------------------------------
    //                          GetFishPrice
    //------------------------------------------------------------------------
    public int GetFishPrice()
    {
        return FishPrice;
    }
    //------------------------------------------------------------------------
    //                          GetNumberOfCoinsProduced
    //------------------------------------------------------------------------
    public int GetNumberOfCoinsProduced()
    {
        return HowManyCoins;
    }
    //------------------------------------------------------------------------
    //                          GetMaxProgress
    //------------------------------------------------------------------------
    public int GetMaxProgress()
    {
        return maxProgress;
    }
    //------------------------------------------------------------------------
    //                          GetFishProgress
    //------------------------------------------------------------------------
    public int GetFishProgress()
    {
        return FishProgrss;
    }
    //------------------------------------------------------------------------
    //                          SetFishProgressToZero
    //------------------------------------------------------------------------
    public void SetFishProgressToZero()
    {
        FishProgrss = 0;
    }
    //------------------------------------------------------------------------
    //                          IncreaseFishProgress
    //------------------------------------------------------------------------
    public void IncreaseFishProgress()
    {
        FishProgrss += Time.deltaTime;
    }
    //------------------------------------------------------------------------
    //                          GetHungerMeter
    //------------------------------------------------------------------------
    public int GetHungerMeter()
    {
        return hungerMeterForFish;
    }
    //------------------------------------------------------------------------
    //                          GetIsFishHungry
    //------------------------------------------------------------------------
    public int GetIsFishHungry()
    {
        return isFishHungry;
    }
    //------------------------------------------------------------------------
    //                          GetIsUnlocked
    //------------------------------------------------------------------------
    public bool GetIsUnlocked()
    {
        return isUnlocked;
    }
    //------------------------------------------------------------------------
    //                          GetIsAdded
    //------------------------------------------------------------------------
    public bool GetIsAdded()
    {
        return isAdded;
    }
    public void SetIsAddedToTrue()
    {
        isAdded = true;
    }

}


