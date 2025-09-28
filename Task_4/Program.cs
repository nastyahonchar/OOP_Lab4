using System;

abstract class TreasureItem
{
    public string Name;
    public int Amount;
    public TreasureItem(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}

class Gold : TreasureItem
{
    public Gold(int amount) : base("Gold", amount) { }
}

class Gem : TreasureItem
{
    public Gem(string name, int amount) : base(name, amount) { }
}

class Cash : TreasureItem
{
    public Cash(string name, int amount) : base(name, amount) { }
}

class Bag
{
    public int Capacity;
    public int CurrentAmount;
    public Gold GoldItem = null;
    public List<Gem> GemItems = new List<Gem>();
    public List<Cash> CashItems = new List<Cash>();

    public Bag(int capacity)
    {
        Capacity = capacity;
        CurrentAmount = 0;
    }

    public void TryAddItem(TreasureItem item, string type)
    {
        if (CurrentAmount + item.Amount > Capacity)
            return;

        if (type == "Gold")
        {
            if (GoldItem == null)
                GoldItem = new Gold(item.Amount);
            else
                GoldItem.Amount += item.Amount;

            CurrentAmount += item.Amount;
        }
        else if (type == "Gem")
        {
            int totalGold = GoldItem != null ? GoldItem.Amount : 0;
            int totalGems = 0;
            for (int i = 0; i < GemItems.Count; i++)
            {
                totalGems += GemItems[i].Amount;
            }

            if (totalGold >= totalGems + item.Amount)
            {
                bool found = false;
                for (int i = 0; i < GemItems.Count; i++)
                {
                    if (GemItems[i].Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        GemItems[i].Amount += item.Amount;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    GemItems.Add(new Gem(item.Name, item.Amount));

                CurrentAmount += item.Amount;
            }
        }
        else if (type == "Cash")
        {
            int totalGems = 0;
            for (int i = 0; i < GemItems.Count; i++)
            {
                totalGems += GemItems[i].Amount;
            }

            int totalCash = 0;
            for (int i = 0; i < CashItems.Count; i++)
            {
                totalCash += CashItems[i].Amount;
            }

            if (totalGems >= totalCash + item.Amount)
            {
                bool found = false;
                for (int i = 0; i < CashItems.Count; i++)
                {
                    if (CashItems[i].Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        CashItems[i].Amount += item.Amount;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    CashItems.Add(new Cash(item.Name, item.Amount));

                CurrentAmount += item.Amount;
            }
        }
    }

    public void Print()
    {
        List<TreasureItem> list;
        int sum;

        if (GoldItem != null)
        {
            sum = GoldItem.Amount;
            Console.WriteLine($"<Gold> $ {sum}");
            Console.WriteLine($"## {GoldItem.Name} - {GoldItem.Amount}");
        }

        if (GemItems.Count > 0)
        {
            sum = 0;
            for (int i = 0; i < GemItems.Count; i++)
            {
                sum += GemItems[i].Amount;
            }
            Console.WriteLine($"<Gem> $ {sum}");
            GemItems.Sort((a, b) =>
            {
                int cmp = b.Name.CompareTo(a.Name);
                if (cmp == 0)
                    return a.Amount.CompareTo(b.Amount);

                return cmp;
            });
            for (int i = 0; i < GemItems.Count; i++)
            {
                Console.WriteLine($"## {GemItems[i].Name} - {GemItems[i].Amount}");
            }
        }

        if (CashItems.Count > 0)
        {
            sum = 0;
            for (int i = 0; i < CashItems.Count; i++)
            {
                sum += CashItems[i].Amount;
            }
            Console.WriteLine($"<Cash> $ {sum}");
            CashItems.Sort((a, b) =>
            {
                int cmp = b.Name.CompareTo(a.Name);
                if (cmp == 0)
                    return a.Amount.CompareTo(b.Amount);

                return cmp;
            });
            for (int i = 0; i < CashItems.Count; i++)
            {
                Console.WriteLine($"## {CashItems[i].Name} - {CashItems[i].Amount}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        int capacity = int.Parse(Console.ReadLine());
        string[] input = Console.ReadLine().Split(' ');

        Bag bag = new Bag(capacity);

        for (int i = 0; i < input.Length; i += 2)
        {
            string name = input[i];
            int amount = int.Parse(input[i + 1]);
            string type = "";
            if (name.Equals("Gold", StringComparison.OrdinalIgnoreCase))
                type = "Gold";
            else if (name.Length == 3)
                type = "Cash";
            else if (name.Length >= 4 && name.EndsWith("Gem", StringComparison.OrdinalIgnoreCase))
                type = "Gem";
            else
                continue;

            TreasureItem item;
            if (type == "Gold")
                item = new Gold(amount);
            else if (type == "Gem")
                item = new Gem(name, amount);
            else
                item = new Cash(name, amount);

            bag.TryAddItem(item, type);
        }

        bag.Print();
    }
}
