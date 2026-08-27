using System.Globalization;

List<string> stockNames = ["NVDA", "AAPL", "MSFT", "AMZN", "META", "TSLA", "GOOG", "PLTR", "SMCI", "ASML", "INTC", "NFLX", "QCOM", "AVGO", "PYPL", "ABNB", "UBER", "COIN", "EBAY", "SBUX", "COST", "MRNA", "NIOU", "BABA", "BIDU", "SHOP", "SNAP", "ROKU", "PINS", "DASH"];
List<Stock> stocks = [];
bool gameloop = true;

Random random = new Random();

int day = 0;
double balance = 1000.0;
double brokerage = 0;

brandNewDay(); // start day 1

while(gameloop)
{
    int choice = menu();
    if(choice == 1)
    {
        market();
    }
    else if(choice == 2)
    {
        Investsments();
    }
    else if(choice == 3)
    {
        brandNewDay();
    }
}

void market()
{
    Console.Clear();
    Console.WriteLine($"Stock\t\tPrice\t\tTodays %\tTodays P/L\n");
    for(int i = 0; i < stocks.Count(); i++)
    {
        Console.Write($"[{i+1}] {stocks[i].name}\t");

        if(stocks[i].history[day] > (stocks[i].history[day-1]))
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;            
        }

        double amountToday = stocks[i].history[day];
        double amountYesterday = stocks[i].history[day-1];
        double difference = amountToday - amountYesterday;
        double differenceInPercentage = difference / amountYesterday;

        Console.Write($"{stocks[i].history[day]:F2}$");

        if(stocks[i].history[day]<1000.0) { // prevent weird spacing if stocks are over 5 digits long
            Console.Write("\t");
        }
        
        Console.WriteLine($"\t{differenceInPercentage:+0.00%;-0.00%}\t\t{difference:+0.00$;-0.00$}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    
    Console.Write($"\n[0] Home\tBalance: {balance:F2}$\t\tInput a number: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    if(choice != 0)
    {
        reviewStock(choice-1);
    }
}

void reviewStock(int stockID)
{
    Console.Clear();
    Console.WriteLine($"{stocks[stockID].name}\t\tPrice\t\tTodays %\tTodays P/L\n");

    for(int i = 0; i < day; i++)
    {   

        if(stocks[stockID].history[i+1] > (stocks[stockID].history[i]))
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        Console.Write($"\t\t{stocks[stockID].history[i+1]:F2}$");
        double amountToday = stocks[stockID].history[i+1];
        double amountYesterday = stocks[stockID].history[i];
        double difference = amountToday - amountYesterday;
        double differenceInPercentage = difference / amountYesterday;

        if(stocks[stockID].history[i+1]<1000.0) { // prevent weird spacing if stocks are over 5 digits long AGAIN
            Console.Write("\t");
        }
        
        Console.WriteLine($"\t{differenceInPercentage:+0.00%;-0.00%}\t\t{difference:+0.00$;-0.00$}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    Console.WriteLine($"\nBuy 1 {stocks[stockID].name} for {stocks[stockID].history[day]:F2}$ ?\t Balance: {balance:F2}");
    Console.WriteLine("[1] Buy\n[2] Back\n[3] Home");
    Console.Write($"\nInput a number: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    if(choice == 1 && balance > stocks[stockID].history[day])
    {
        balance = balance - stocks[stockID].history[day];
        stocks[stockID].invested = stocks[stockID].invested + stocks[stockID].history[day];
        stocks[stockID].owning++;
        reviewStock(stockID);
    }
}

void brandNewDay()
{
    day++;
    createStock();
}

int menu()
{
    Console.Clear();
    Console.WriteLine($"Day {day}/30");
    Console.WriteLine($"Balance:\t{balance:F2}$");
    calculateBrokerage();
    Console.WriteLine($"Brokerage\t{brokerage:F2}$\n");
    Console.WriteLine("[1] Market");
    Console.WriteLine("[2] Investments");
    Console.WriteLine("[3] Next Day");
    Console.Write($"\nInput a number: ");
    return Convert.ToInt32(Console.ReadLine());
}

void createStock() // creates stock with random values and adds it to the stocks list
{
    Stock stock = new Stock();

    stock.name = stockNames[random.Next(stockNames.Count())];
    stockNames.Remove(stock.name);

    stock.startingPrice = random.Next(10, 3000) / 10.0;
    stock.stable = random.Next(1,50) / 10.0;
    stock.history.Add(stock.startingPrice);
    double substract = 100;

    for(int i = 0; i < 100; i++)
    {   
        substract = substract + (random.Next(0,10) - 5);
        double multiplier = random.Next(0,201)-substract;
        multiplier = (multiplier / 2000.0 * stock.stable + 1) ;
        stock.history.Add(stock.history[stock.history.Count() -1] * multiplier);
    }
    stocks.Add(stock);
}
void calculateBrokerage()
{
    brokerage = 0;
    for(int i = 0; i < day; i++) // for every stock
    {
        brokerage += stocks[i].history[day] * stocks[i].owning;
    }
}
void Investsments()
{
    Console.Clear();
    int stockID = 0;
    Console.WriteLine($"Stock\t\tAmount\tTotal\t\tPrice\n");
    for(int i = 0; i < day;i++)
    {
        if(stocks[i].owning > 0)
        {
            stockID++;
            Console.Write($"[{stockID}] {stocks[i].name}\t{stocks[i].owning}\t");
            Console.WriteLine($"{(stocks[i].history[day]*stocks[i].owning):F2}\t\t{stocks[i].history[day]:F2}");
        }
    }
        if(stockID == 0)
        {
            return;
        }
    Console.WriteLine($"\n[0] Menu\t\tBalance: {balance:F2}");
    
    Console.Write($"\nSell stock: ");
    int sellstock = Convert.ToInt32(Console.ReadLine());

    stockID = 0;
    for(int k = 0; k < day; k++)
    {
        if(stocks[k].owning > 0)
        {
            stockID++;
            if(sellstock == stockID)
            {
                Console.WriteLine($"You own {stocks[k].owning} stocks. \nHow many do you want to sell? (0 to cancel)");
                stockID = Convert.ToInt32(Console.ReadLine());
                if(stockID > 0 && stockID <= stocks[k].owning)
                {
                    stocks[k].owning = stocks[k].owning - stockID;
                    balance = balance + (stockID * stocks[k].history[day]);
                }
            }
        }
    }
}
public class Stock
{
    public string name = ""; // from stockNames list
    public double startingPrice; // between 1 and 1000
    public double stable; // multiplies profit
    public List<double> history = [];

    public int owning = 0;
    public double invested = 0;
}