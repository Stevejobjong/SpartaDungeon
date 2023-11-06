namespace SpartaDungeon {
    internal class Program {
        private static Character player;

        static void Main(string[] args) {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting() {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
            // 아이템 정보 세팅
            Item item1 = new Item("무쇠갑옷", 0, 5, true,"무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item item2 = new Item("낡은 검", 2, 0, false, "쉽게 볼 수 있는 낡은 검입니다.");
            Inventory inven = new Inventory();
            inven.InputItem(item1);
            inven.InputItem(item2);
            player.inventory = inven;
        }

        static void DisplayGameIntro() {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();

            int input = CheckValidInput(1, 2);
            switch (input) {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input) {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowInventory();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input) {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayGameIntro();
                    break;
            }
        }

        static int CheckValidInput(int min, int max) {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true) {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess) {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Inventory inventory;

        public Character(string name, string job, int level, int atk, int def, int hp, int gold) {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
    public class Item {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public bool isEquipped { get; set; }
        public string description;


        public Item(string name, int atk, int def, bool equipped, string description) {
            Name = name;
            Atk = atk;
            Def = def;
            isEquipped = equipped;
            this.description = description;
        }
    }
    public class Inventory {
        public Item[] itemArr;
        int idx;
        public Inventory() {
            itemArr = new Item[100];
        }
        public bool InputItem(Item itm) {
            if (idx < 100) {
                itemArr[idx++] = itm;
                return true;
            } else
                return false;
        }
        public void ShowInventory() {
            for (int i = 0; i < idx; i++) {
                Console.Write("- " + (i + 1)+" ");
                if (itemArr[i].isEquipped) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[E]");
                    Console.ResetColor();
                }
                Console.Write(itemArr[i].Name+"\t");
                if (itemArr[i].Atk > 0) { Console.Write("| 공격력 +" + itemArr[i].Atk); }
                if (itemArr[i].Def > 0) { Console.Write("| 방어력 +" + itemArr[i].Def); }
                Console.WriteLine("| " + itemArr[i].description);
            }
        }

    }
}