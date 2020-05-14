using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class CurrencySystem : GameObject
{
    private int money = 2000000;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
    public CurrencySystem()
    {

    }
    //------------------------------------------------------------------------
    //                          AddMoney
    //------------------------------------------------------------------------
    public void AddMoney(int addition)
    {
        money += addition;
    }
    //------------------------------------------------------------------------
    //                          RemoveMoney
    //------------------------------------------------------------------------
    public void RemoveMoney(int subtraction)
    {
        money -= subtraction;
    }
    //------------------------------------------------------------------------
    //                          getMoney
    //------------------------------------------------------------------------
    public int getMoney()
    {
        return money;
    }
}

