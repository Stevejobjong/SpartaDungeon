namespace SpartaDungeon
{
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
        public bool DeleteItem(Item itm)
        {
            itemList.Remove(itm);
            return true;
        }
        public void ShowItems()
        {
            int i = 0;

            foreach (Item item in sortedList)
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
                Console.WriteLine($"| {item.Price} G");
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