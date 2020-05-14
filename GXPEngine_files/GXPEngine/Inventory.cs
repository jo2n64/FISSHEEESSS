using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Inventory : GameObject
{
    public const int Deselected = 0;
    public const int Food = 1;
    public const int Sponge = 2;
    public const int Shop = 3;

    private Item food;
    private Item sponge;
    private Item shop;

    private Sprite emptySpace1;
    private Sprite emptySpace2;
    private Sprite emptySpace3;

    private Sprite inventoryBackground;
    private List<Item> listOfItemsInInventory;

    private int id = 0;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public Inventory()
    {
        initializeObjects();
        handleBackground();
        setEmptySpaceDimensionAndPosition();
        setDimentionsAndPositionForItems();
        addItemsToListOfItems();
        addChildrenToScreen();

    }
    //------------------------------------------------------------------------
    //                          initializeObjects
    //------------------------------------------------------------------------
    private void initializeObjects()
    {
        emptySpace1 = new Sprite("checkers.png");
        emptySpace2 = new Sprite("checkers.png");
        emptySpace3 = new Sprite("checkers.png");

        listOfItemsInInventory = new List<Item>();
        food = new Item("fish_food_can.png", Food);
        sponge = new Item("sponge.png", Sponge);
        shop = new Item("shop_icon_idea.png", Shop);
    }
    //------------------------------------------------------------------------
    //                          handleBackground
    //------------------------------------------------------------------------
    private void handleBackground()
    {
        inventoryBackground = new Sprite("inventory.png");
        inventoryBackground.SetXY(game.width - 150, 100);
        inventoryBackground.SetScaleXY(0.75f);
    }
    //------------------------------------------------------------------------
    //                          setDimentionsAndPositionForItems
    //------------------------------------------------------------------------
    private void setDimentionsAndPositionForItems()
    {
        food.SetXY(emptySpace1.x + emptySpace1.width / 6, emptySpace1.y);
        food.width /= 5;
        food.height /= 5;

        sponge.SetXY(emptySpace2.x, emptySpace2.y - 5);
        sponge.width /= 7;
        sponge.height /= 7;

        shop.SetXY(emptySpace3.x, emptySpace3.y - 5);
        shop.width /= 4;
        shop.height /= 4;
    }
    //------------------------------------------------------------------------
    //                          addItemsToListOfItems
    //------------------------------------------------------------------------
    private void addItemsToListOfItems()
    {
        listOfItemsInInventory.Add(food);
        listOfItemsInInventory.Add(sponge);
        listOfItemsInInventory.Add(shop);
    }
    //------------------------------------------------------------------------
    //                          setEmptySpaceDimensionAndPosition
    //------------------------------------------------------------------------
    private void setEmptySpaceDimensionAndPosition()
    {
        emptySpace1.SetXY(game.width - 130, 140);
        emptySpace2.SetXY(game.width - 130, 270);
        emptySpace3.SetXY(game.width - 130, 400);

        emptySpace1.width += 40;
        emptySpace1.height += 30;
        emptySpace2.width += 40;
        emptySpace2.height += 30;
        emptySpace3.width += 40;
        emptySpace3.height += 30;
        emptySpace1.alpha = 0;
        emptySpace2.alpha = 0;
        emptySpace3.alpha = 0;
    }
    //------------------------------------------------------------------------
    //                          addChildrenToScreen
    //------------------------------------------------------------------------
    private void addChildrenToScreen()
    {
        AddChild(inventoryBackground);
        AddChild(emptySpace1);
        AddChild(emptySpace2);
        AddChild(emptySpace3);
        AddChild(food);
        AddChild(sponge);
        AddChild(shop);
    }
    //------------------------------------------------------------------------
    //                          Update
    //------------------------------------------------------------------------
    void Update()
    {
        checkIfItemIsPressed();
        checkID();
    }
    //------------------------------------------------------------------------
    //                          checkIfItemIsPressed
    //------------------------------------------------------------------------
    void checkIfItemIsPressed()
    {
        foreach (Item item in listOfItemsInInventory)
        {
            if (MyGame.CheckMouseInRectClick(item))
            {
                if (item.selected == false)
                {
                    Deselect();
                    item.selected = true;
                    id = item.id;
                }
                else
                {
                    item.selected = false;
                    id = 0;
                }

            }
        }
    }
    //------------------------------------------------------------------------
    //                          checkIfItemIsOverlapped
    //------------------------------------------------------------------------
    public bool checkIfItemIsOverlapped()
    {
        foreach (Item item in listOfItemsInInventory)
        {
            if (MyGame.CheckMouseInRect(item))
            {
                return true;
            }
        }
        return false;
    }
    //------------------------------------------------------------------------
    //                          Deselect
    //------------------------------------------------------------------------
    public void Deselect()
    {
        id = 0;
        food.selected = false;
        sponge.selected = false;
        shop.selected = false;
    }
    //------------------------------------------------------------------------
    //                          DeselectShop
    //------------------------------------------------------------------------
    public void DeselectShop()
    {
        id = 0;
        shop.selected = false;
    }
    //------------------------------------------------------------------------
    //                          checkID
    //------------------------------------------------------------------------
    void checkID()
    {
        switch (id)
        {
            case Food:
                food.visible = false;
                sponge.visible = true;
                break;
            case Sponge:
                food.visible = true;
                sponge.visible = false;
                break;
            case Shop:
                food.visible = true;
                sponge.visible = true;
                break;
            case Deselected:
                food.visible = true;
                sponge.visible = true;
                break;
        }

    }
    //------------------------------------------------------------------------
    //                          getID
    //------------------------------------------------------------------------
    public int getID()
    {
        return id;
    }
}

