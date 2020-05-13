using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Inventory : GameObject
    {
        public const int Food = 1;
        public const int Sponge = 2;
        public const int Shop = 3;
        Item food;
        Item sponge;
        public Item shop;
        Journal journal;
        Sprite emptySpace1;
        Sprite emptySpace2;
        Sprite emptySpace3;
        Sprite inventoryBackground;
        List<Item> listOfItemsInInventory;
        public int id = 0;
        //Sprite emptySpace4;
        public Inventory()
        {
            listOfItemsInInventory = new List<Item>();
            food = new Item("fish_food_can.png", Food);
            sponge = new Item("sponge.png", Sponge);
            shop = new Item("shop_icon_idea.png", Shop);
            emptySpace1 = new Sprite("checkers.png");
            emptySpace2 = new Sprite("checkers.png");
            emptySpace3 = new Sprite("checkers.png");
            inventoryBackground = new Sprite("inventory.png");
            emptySpace1.x = game.width - 130;
            emptySpace2.x = game.width - 130;
            emptySpace3.x = game.width - 130;
            // inventoryBackground.x = game.width - 150;
            inventoryBackground.SetXY(game.width - 150, 100);
            emptySpace1.y = 140;
            emptySpace2.y = 270;
            emptySpace3.y = 400;
            inventoryBackground.SetScaleXY(0.75f);
            emptySpace1.width += 40;
            emptySpace1.height += 30;
            emptySpace2.width += 40;
            emptySpace2.height += 30;
            emptySpace3.width += 40;
            emptySpace3.height += 30;
            emptySpace1.alpha = 0;
            emptySpace2.alpha = 0;
            emptySpace3.alpha = 0;
            AddChild(inventoryBackground);
            AddChild(emptySpace1);
            AddChild(emptySpace2);
            AddChild(emptySpace3);

            food.x = emptySpace1.x+emptySpace1.width/6;
            food.y = emptySpace1.y;
            food.width /= 5;
            food.height /= 5;
            // AddChild(food);

            sponge.x = emptySpace2.x;
            sponge.y = emptySpace2.y-5;
            sponge.width /= 7;
            sponge.height /= 7;
            // AddChild(sponge);

            shop.x = emptySpace3.x;
            shop.y = emptySpace3.y-5;
            shop.width /= 4;
            shop.height /= 4;
            //AddChild(shop);
            listOfItemsInInventory.Add(food);
            listOfItemsInInventory.Add(sponge);
            listOfItemsInInventory.Add(shop);
            //emptySpace4 = new Sprite("checkers.png");
            AddChild(food);
            AddChild(sponge);
            AddChild(shop);
        }
        void Update()
        {
            checkIfItemIsPressed();
            checkID();
        }
        void checkIfItemIsPressed()
        {
            foreach (Item item in listOfItemsInInventory)
            {
                if (MyGame.CheckMouseInRectClick(item))
                {
                    if (item.selected == false)
                    {
                        foreach (Item thing in listOfItemsInInventory)
                        {
                            if (thing != item)
                            {
                                thing.selected = false;
                            }
                            else thing.selected = true;
                        }
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

        void checkID()
        {
            switch (id)
            {
                case Food:
                    //RemoveChild(food);
                    food.visible = false;
                    sponge.visible = true;
                    //shop.visible = true;
                    //AddChild(sponge);
                    //AddChild(shop);
                    break;
                case Sponge:
                    //RemoveChild(sponge);
                    //AddChild(food);
                    //AddChild(shop);
                    food.visible = true;
                    sponge.visible = false;
                    // shop.visible = true;
                    break;
                case Shop:
                    ////RemoveChild(shop);
                    //AddChild(sponge);
                    //AddChild(food);
                    food.visible = true;
                    sponge.visible = true;
                    //shop.visible = true;
                    break;
                case 0:
                    //AddChild(food);
                    //AddChild(sponge);
                    //AddChild(shop);
                    food.visible = true;
                    sponge.visible = true;
                    //shop.visible = true;
                    break;
            }
        }
    }
}
