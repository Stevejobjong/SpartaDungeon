namespace SpartaDungeon
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public float Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public int itemAtk;     //장착 아이템으로 오르는 공격력
        public int itemDef;     //장착 아이템으로 오르는 방어력
        public Inventory inventory;
        public int ClearCnt;
        public Character(string name, string job, int level, float atk, int def, int hp, int gold)
        {
            ClearCnt = 0;
            inventory = new Inventory();
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
        public void LevelCheck()
        {
            if (Level == 1 && ClearCnt >= 1)
                LevelUp();
            else if (Level == 2 && ClearCnt >= 2)
                LevelUp();
            else if (Level == 3 && ClearCnt >= 3)
                LevelUp();
            else if (Level == 4 && ClearCnt >= 4)
                LevelUp();
        }
        public void LevelUp()
        {
            Level++;
            Console.WriteLine($"축하합니다! 레벨이 {Level}로 올랐습니다.");
            Console.WriteLine($"공격력:{Atk} -> {Atk + 0.5}"); Atk += 0.5f;
            Console.WriteLine($"방어력:{Def} -> {Def + 1}"); Def++;
        }
        public void TakeDamage(int damage)
        {
            this.Hp -= damage;
        }
        public void EquipItem(int idx)
        {
            Item curItem = inventory.sortedList[idx];

            //같은 타입의 아이템 장착 해제
            for (int i = 0; i < inventory.sortedList.Count; i++)
            {
                if (inventory.sortedList[i].itemType == curItem.itemType)
                    unEquipItem(i);
            }
            curItem.isEquipped = true;

            if (curItem.Atk > 0)
            {
                itemAtk += curItem.Atk;
                Atk += curItem.Atk;
            }
            if (curItem.Def > 0)
            {
                itemDef += curItem.Def;
                Def += curItem.Def;
            }
        }
        public void unEquipItem(int idx)
        {
            Item curItem = inventory.sortedList[idx];
            curItem.isEquipped = false;
            if (curItem.Atk > 0)
            {
                itemAtk -= curItem.Atk;
                Atk -= curItem.Atk;
            }
            if (curItem.Def > 0)
            {
                itemDef -= curItem.Def;
                Def -= curItem.Def;
            }
        }
        public bool isEquipped(int idx)
        {
            Item curItem = inventory.sortedList[idx];
            return curItem.isEquipped;
        }

        public void SellItem(int idx)
        {
            Item item = inventory.sortedList[idx];
            Gold += (int)(item.Price * 0.85);
            if (inventory.sortedList[idx].isEquipped)
                unEquipItem(idx);
            inventory.sortedList.RemoveAt(idx);
            inventory.DeleteItem(item);
        }
    }

}