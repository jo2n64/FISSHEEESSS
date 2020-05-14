﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class CurrencySystem:GameObject
    {
        public int money=11;
        public CurrencySystem()
        {

        }

        public void AddMoney(int addition)
        {
            money += addition;
        }
        public void RemoveMoney(int subtraction)
        {
            money -= subtraction;
        }

        public int getMoney()
        {
            return money;
        }
    }
}
