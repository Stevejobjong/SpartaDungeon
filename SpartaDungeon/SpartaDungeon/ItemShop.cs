namespace SpartaDungeon
{
    public class ItemShop
    {
        public List<Item> items;

        public ItemShop()
        {
            items = new List<Item>();

        }

        public bool InputItem(Item itm)
        {
            items.Add(itm);
            return true;
        }

        public void ShowItems()
        {
            int i = 0;

            foreach (Item item in items)
            {
                Console.Write("- " + (i++ + 1) + " ");
                if (item.isEquipped)
                {
                    Console.Write('[');
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('E');
                    Console.ResetColor();
                    Console.Write(']');
                    Console.Write(PadRightForMixedText(item.Name, 12));
                }
                else
                    Console.Write(PadRightForMixedText(item.Name, 15));

                if (item.Atk > 0) { Console.Write(PadRightForMixedText("| 공격력 +" + item.Atk.ToString(), 13)); }
                if (item.Def > 0) { Console.Write(PadRightForMixedText("| 방어력 +" + item.Def.ToString(), 13)); }
                Console.Write(PadRightForMixedText("| " + item.description, 40));
                Console.WriteLine($"| {(item.isOwned ? "구매완료" : $"{item.Price} G")} ");
            }
        }

        public int BuyItem(Character p, int idx)
        {
            //if (p.inventory.itemList.Find(x => x.Name == items[idx].Name) != null)  //이미 보유중이면 return
            //    return;

            if (items[idx].isOwned == true)
                return 1;
            if (p.Gold - items[idx].Price < 0)
                return 2;
            items[idx].isOwned = true;
            p.Gold -= items[idx].Price;
            p.inventory.InputItem(items[idx]);
            return 3;
        }
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

    }

}