# simpleStockGame

A **STILL IN DEVELOPMENT** simple console-based stock simulation game written in C#.

## Installation

- Clone the repository.
- `cd` into the repository.
- Run the game with `dotnet run`.

## How to Play

Every day, a new stock appears that you can buy.

To buy and review stocks, press `1` in the menu to enter the market. There, you can see all the stocks that are currently available.

To select a stock, enter the number next to it. Alternatively, select `0` to go back to the main menu.

When you select a stock, you can review its performance and choose whether you want to buy it.

Back in the main menu, select `2` to start a new day and see how your portfolio performs.

### Common Issues (will be fixed)

- The game crashes after day 30 because stock names are taken from an array and removed. After 30 days, there are no stock names left, causing the game to crash.
- The game also crashes if you enter a number that is not one of the available options.

## Coming Soon

- Review and sell owned stocks
- End-of-game screen and statistics
