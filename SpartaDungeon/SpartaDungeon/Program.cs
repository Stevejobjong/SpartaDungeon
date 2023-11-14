using System.Text;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;
        private static ItemShop shop;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
            shop = new ItemShop();

            // 아이템 정보 세팅

            shop.InputItem(new Item("낡은 검", Item.ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 200));
            shop.InputItem(new Item("보통 검", Item.ItemType.Weapon, 4, 0, "보통의 검입니다.", 400));
            shop.InputItem(new Item("종이갑옷", Item.ItemType.Armor, 0, 2, "종이로 만들어진 갑옷입니다.", 100));
            shop.InputItem(new Item("신성한 검", Item.ItemType.Weapon, 10, 0, "신성한 힘이 깃든 검입니다.", 1000));
            shop.InputItem(new Item("무쇠갑옷", Item.ItemType.Armor, 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", 400));
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine();

            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    EntranceDungeon();
                    break;
                case 5:
                    TakeRest();
                    break;

            }
        }

        private static void TakeRest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    Console.Write("휴식을 취합니다.");
                    player.Gold -= 500;
                    player.Hp = 100;
                    Thread.Sleep(1000);
                    DisplayGameIntro();
                    break;
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void EntranceDungeon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전   | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    EasyDungeon();
                    break;
                case 2:
                    NormalDungeon();
                    break;
                case 3:
                    HardDungeon();
                    break;
            }

        }

        private static void EasyDungeon()
        {
            Random rand = new Random();
            int d = player.Def - 5;
            int r = rand.Next(20, 36);
            int win = rand.Next(0, 10);
            int bonus = rand.Next((int)player.Atk, (int)player.Atk * 2);
            if ((player.Hp - r + d) > 0 && (d >= 0 || win < 6))   //방어력이 권장 방어력보다 높으면 무조건 클리어, 낮으면 60% 확률로 클리어
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("던전 클리어");
                Console.ResetColor();
                Console.WriteLine("축하합니다!!");
                Console.WriteLine("쉬운 던전을 클리어 하였습니다.");
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - r + d}");
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + 1000 + 1000 * bonus / 100} G");
                player.ClearCnt++;
                player.LevelCheck();
                player.TakeDamage(r - d);
                player.Gold += 1000 + 1000 * bonus / 100;
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        EntranceDungeon();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("던전 실패");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - player.Hp / 2}");
                player.TakeDamage(player.Hp / 2);
            }
        }

        private static void NormalDungeon()
        {
            Random rand = new Random();
            int d = player.Def - 11;
            int r = rand.Next(20, 36);
            int win = rand.Next(0, 10);
            int bonus = rand.Next((int)player.Atk, (int)player.Atk * 2);
            if ((player.Hp - r + d)>0 && (d >= 0 || win < 6))    //방어력이 권장 방어력보다 높으면 무조건 클리어, 낮으면 60% 확률로 클리어
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("던전 클리어");
                Console.ResetColor();
                Console.WriteLine("축하합니다!!");
                Console.WriteLine("일반 던전을 클리어 하였습니다.");
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - r + d}");
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + 1800 + 1800 * bonus / 100} G");
                player.TakeDamage(r - d);
                player.Gold += 1800 + 1800 * bonus / 100;
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        EntranceDungeon();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("던전 실패");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - player.Hp / 2}");
                player.TakeDamage(player.Hp / 2);
            }
        }
        private static void HardDungeon()
        {
            Random rand = new Random();
            int d = player.Def - 17;
            int r = rand.Next(20, 36);
            int win = rand.Next(0, 10);
            int bonus = rand.Next((int)player.Atk, (int)player.Atk * 2);
            if ((player.Hp - r + d) > 0 && (d >= 0 || win < 6))    //방어력이 권장 방어력보다 높으면 무조건 클리어, 낮으면 60% 확률로 클리어
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("던전 클리어");
                Console.ResetColor();
                Console.WriteLine("축하합니다!!");
                Console.WriteLine("어려운 던전을 클리어 하였습니다.");
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - r + d}");
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + 2500 + 2500 * bonus / 100} G");
                player.TakeDamage(r - d);
                player.Gold += 2500 + 2500 * bonus / 100;
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        EntranceDungeon();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("던전 실패");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - player.Hp / 2}");
                player.TakeDamage(player.Hp / 2);
            }
        }

        static void DisplayShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            shop.ShowItems();
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    BuyItem(0);
                    break;
                case 2:
                    SellItem();
                    break;
            }
        }

        private static void SellItem()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowItems();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int input = CheckValidInput(0, player.inventory.itemList.Count);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    player.SellItem(input - 1);
                    SellItem();
                    break;
            }
        }

        private static void BuyItem(int Message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            shop.ShowItems();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            switch (Message)
            {
                case 1://이미 구매
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ResetColor();
                    break;
                case 2://Gold 부족
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gold가 부족합니다.");
                    Console.ResetColor();
                    break;
                case 3://구매 완료
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("구매를 완료했습니다.");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine();
            int input = CheckValidInput(0, shop.items.Count);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    int result = shop.BuyItem(player, input - 1);
                    BuyItem(result);
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
            player.inventory.ShowItems();
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
            player.inventory.ShowItems();
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
            player.inventory.ShowItems();
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

}