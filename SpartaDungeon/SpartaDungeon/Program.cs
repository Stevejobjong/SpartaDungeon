using System.Text;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
            // 아이템 정보 세팅
            player.inventory.InputItem(new Item("낡은 검", 2, 0, false, "쉽게 볼 수 있는 낡은 검입니다."));
            player.inventory.InputItem(new Item("보통 검", 4, 0, false, "보통의 검입니다."));
            player.inventory.InputItem(new Item("신성한 검", 10, 0, false, "신성한 힘이 깃든 검입니다."));
            player.inventory.InputItem(new Item("무쇠갑옷", 0, 5, false, "무쇠로 만들어져 튼튼한 갑옷입니다."));
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
            }
        }
        /// <summary>
        /// 상태보기
        /// </summary>
        static void DisplayMyInfo()
        {
            StringBuilder sb = new StringBuilder();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");

            sb.AppendFormat($"공격력 : {player.Atk}");
            string str = player.itemAtk > 0 ? $" (+{player.itemAtk})" : "";
            sb.AppendFormat(str);
            Console.WriteLine(sb.ToString());

            sb.Clear();
            sb.AppendFormat($"방어력 : {player.Def}");
            str = player.itemDef > 0 ? $" (+{player.itemDef})" : "";
            sb.AppendFormat(str);
            Console.WriteLine(sb.ToString());

            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }
        /// <summary>
        /// 인벤토리 보기
        /// </summary>
        static void DisplayInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowInventory(false);
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    ManageEquipped();
                    break;
                case 2:
                    SortInventory();
                    break;
            }

        }
        static void SortInventory()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowInventory(false);
            Console.WriteLine();
            Console.WriteLine("1. 이름");
            Console.WriteLine("2. 장착순");
            Console.WriteLine("3. 공격력");
            Console.WriteLine("4. 방어력");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    player.inventory.SortByLength();
                    DisplayInventory();
                    break;
                case 2:
                    player.inventory.SortByEquipped();
                    DisplayInventory();
                    break;
                case 3:
                    player.inventory.SortByAtk();
                    DisplayInventory();
                    break;
                case 4:
                    player.inventory.SortByDef();
                    DisplayInventory();
                    break;
            }
        }

        /// <summary>
        /// 인벤토리 - 장착관리
        /// </summary>
        static void ManageEquipped()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowInventory(true);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, player.inventory.sortedList.Count);
            if (input == 0)
                DisplayGameIntro();
            else if (player.isEquipped(input - 1))
            {
                player.unEquipItem(input - 1);
            }
            else
            {
                player.EquipItem(input - 1);
            }
            ManageEquipped();

        }
        static int CheckValidInput(int min, int max)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        public int Gold { get; }
        public int itemAtk;     //장착 아이템으로 오르는 공격력
        public int itemDef;     //장착 아이템으로 오르는 방어력
        public Inventory inventory;

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            inventory = new Inventory();
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        public void EquipItem(int idx)
        {
            Item curItem = inventory.sortedList[idx];
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
    }
    public class Item
    {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public bool isEquipped { get; set; }    //장착 여부
        public string description;


        public Item(string name, int atk, int def, bool equipped, string description)
        {
            Name = name;
            Atk = atk;
            Def = def;
            isEquipped = equipped;
            this.description = description;
        }
        public int CompareLength (Item itm)
        {
            if (Name.Length > itm.Name.Length)
                return 1;
            else if (Name.Length == itm.Name.Length)
                return 0;
            else
                return -1;
        }
    }
    /// <summary>
    /// 아이템을 저장하는 클래스
    /// </summary>
    public class Inventory
    {
        public List<Item> itemList;
        public List<Item> sortedList;
        public Inventory()
        {
            itemList = new List<Item>();
            sortedList = new List<Item>();
        }
        public bool InputItem(Item itm)
        {
            itemList.Add(itm);
            sortedList.Add(itm);
            return true;
        }
        public void ShowInventory(bool isSelect)
        {
            int i = 0;

            foreach(Item item in sortedList)
            {
                Console.Write("- " + (i++ + 1) + " ");
                if (item.isEquipped)
                {
                    Console.Write('[');
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('E');
                    Console.ResetColor();
                    Console.Write(']');
                }
                Console.Write(item.Name.PadRight(10) + '\t');
                if (item.Atk > 0) { Console.Write(("| 공격력 +" + item.Atk).PadRight(3)); }
                if (item.Def > 0) { Console.Write(("| 방어력 +" + item.Def).PadRight(3)); }
                Console.WriteLine("\t| " + item.description.PadRight(30));
            }
        }
        public void SortByLength()
        {
            sortedList = itemList.OrderBy(p => p.Name.Length).Reverse().ToList();
        }

        public void SortByEquipped()
        {
            sortedList = itemList.OrderBy(p => p.isEquipped).Reverse().ToList();
        }

        public void SortByAtk()
        {
            sortedList = itemList.OrderBy(p => p.Atk).Reverse().ToList();
        }

        public void SortByDef()
        {
            sortedList = itemList.OrderBy(p => p.Def).Reverse().ToList();
        }

    }
}