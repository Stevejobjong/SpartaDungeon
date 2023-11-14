namespace SpartaDungeon
{
    public class Item
    {
        public enum ItemType { Weapon, Armor };
        public ItemType itemType;
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public bool isEquipped { get; set; }    //장착 여부
        public bool isOwned { get; set; }     //보유 여부
        public string description;
        public int Price { get; }


        public Item(string name, ItemType type, int atk, int def, string description, int price, bool equipped = false, bool isOwned = false)
        {
            itemType = type;
            Name = name;
            Atk = atk;
            Def = def;
            Price = price;
            this.description = description;
            this.isOwned = isOwned;
        }
        public int CompareLength(Item itm)
        {
            if (Name.Length > itm.Name.Length)
                return 1;
            else if (Name.Length == itm.Name.Length)
                return 0;
            else
                return -1;
        }
    }

}