using System;

abstract class PriceCalculatorBase
{
    public abstract double Calculate();
}

class PriceCalculator : PriceCalculatorBase
{
    public double PricePerDay { get; set; }
    public int NumberOfDays { get; set; }
    public SeasonType Season { get; set; }
    public DiscountType Discount { get; set; }

    public enum SeasonType
    {
        Autumn = 1,
        Spring = 2,
        Winter = 3,
        Summer = 4
    }

    public enum DiscountType
    {
        VIP,
        SecondVisit,
        None
    }

    public PriceCalculator(double pricePerDay, int numberOfDays, SeasonType season, DiscountType discount)
    {
        PricePerDay = pricePerDay;
        NumberOfDays = numberOfDays;
        Season = season;
        Discount = discount;
    }

    public override double Calculate()
    {
        double discountCoefficient;

        switch (Discount)
        {
            case DiscountType.VIP:
                discountCoefficient = 0.8;
                break;
            case DiscountType.SecondVisit:
                discountCoefficient = 0.9;
                break;
            default:
                discountCoefficient = 1.0;
                break;
        }

        return PricePerDay * NumberOfDays * (int)Season * discountCoefficient;
    }

}
class Program
{
    static void Main()
    {
        Console.WriteLine("Enter booking info: PricePerDay NumberOfDays Season DiscountType");
        string[] info = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        double pricePerDay = double.Parse(info[0]);
        int numberOfDays = int.Parse(info[1]);
        PriceCalculator.SeasonType season = Enum.Parse<PriceCalculator.SeasonType>(info[2], true);
        PriceCalculator.DiscountType discount;
        if (info.Length == 4)
            discount = Enum.Parse<PriceCalculator.DiscountType>(info[3], true);
        else
            discount = PriceCalculator.DiscountType.None;

        PriceCalculatorBase calculator = new PriceCalculator(pricePerDay, numberOfDays, season, discount);
        Console.WriteLine($"{calculator.Calculate():F2}");
    }
}