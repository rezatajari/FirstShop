using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class ItemsEntity
    {
        public string Stuff { get; set; }
        public int Qnt { get; set; }

        public const string MILK = "milk";
        public const string BREAD = "bread";
        public const string RICE = "rice";
        public const string POTATO = "potato";
        public const string TOMATO = "tomato";
    }
}
