using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winden_Apriori
{
    class CreateData
    {
        private Itemset _items;
        private ItemsetCollection _db;
        public ItemsetCollection setData()
        {
            _items = new Itemset();
            _items.Add("Game");
            _items.Add("Water");
            _items.Add("Monster");
            _items.Add("Candy");
            _items.Add("Shoes");
            _items.Add("Controller");
            _items.Add("PizzaRolls");
            _items.Add("Console");

            _db = new ItemsetCollection();
            _db.Add(new Itemset() { _items[0], _items[2], _items[3], _items[5] });
            _db.Add(new Itemset() { _items[1], _items[4] });
            _db.Add(new Itemset() { _items[0], _items[2], _items[6] });
            _db.Add(new Itemset() { _items[1], _items[3], _items[4], _items[7] });
            _db.Add(new Itemset() { _items[0], _items[1], _items[6] });

            return _db;
        }

    }
}
