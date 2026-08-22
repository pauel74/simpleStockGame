List<string> stockNames = ["NVDA", "AAPL", "GOOGL", "MSFT", "AMZN", "TSLA", "META", "NFLX", "SAP", "AMD"];
List<Stock> stocks = [];
Random random = new Random();

Console.Clear();
createStock();
Console.ForegroundColor = ConsoleColor.White;


void createStock() // creates stock with random values and adds it to the stocks list
{
    Stock stock = new Stock();
    stock.name = stockNames[random.Next(stockNames.Count())];
    stockNames.Remove(stock.name);
    stock.startingPrice = random.Next(10, 10000) / 10.0;
    stock.stable = random.Next(1,50) / 10.0;
    stock.history.Add(stock.startingPrice);
    Console.WriteLine($"{stock.name}\tPRICE\t\tPERCENTAGE\tPROFIT\n*{stock.stable}");
    for(int i = 0; i < 20; i++)
    {
        double multiplier = random.Next(200)-100;
        multiplier = (multiplier / 2000.0 * stock.stable + 1) ;
        stock.history.Add(stock.history[stock.history.Count() -1] * multiplier);

        Console.ForegroundColor = ConsoleColor.White;

        if(stock.history[stock.history.Count-1] > stock.history[stock.history.Count-2])
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else {Console.ForegroundColor = ConsoleColor.Red;}

        Console.Write($"\t{stock.history[stock.history.Count-1]:F2}$\t\t");

        if(Console.ForegroundColor == ConsoleColor.Green)
        {
            Console.Write("+");
        }
        double difference = stock.history[stock.history.Count-2] - stock.history[stock.history.Count-1];
        difference = difference * -1.0;
        Console.Write($"{multiplier-1:F3}%\t\t");
        if(Console.ForegroundColor == ConsoleColor.Green)
        {
            Console.Write("+");
        }
        Console.WriteLine($"{difference:F2}$");

        Thread.Sleep(50);
    }
    stocks.Add(stock);
}
public class Stock
{
    public string name = ""; // from stockNames list
    public double startingPrice; // between 1 and 1000
    public double stable; // multiplies profit
    public List<double> history = [];
}