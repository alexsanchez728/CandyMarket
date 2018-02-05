using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
	class Program
	{
		static void Main(string[] args)
		{
			// wanna be a l33t h@x0r? skip all this console menu nonsense and go with straight command line arguments. something like `candy-market add taffy "blueberry cheesecake" yesterday`
			var db = SetupNewApp();

			var run = true;
			while (run)
			{
				ConsoleKeyInfo userInput = MainMenu(db);

				switch (userInput.KeyChar)
				{
					case '0':
						run = false;
						break;
					case '1':

                        var selectedCandyType = AddNewCandyType(db).KeyChar;
                        var amountToAdd = GetAmount("adding");
                        var candyName = GetCandyName();
                        var candyBrand = GetBrand();
                        var candySize = GetSize();

                        var newCandy = new Candy
                        {
                            BrandName = candyBrand,
                            TypeOfCandy = selectedCandyType,
                            HowMany = amountToAdd,
                            Size = candySize
                        };

                        db.SaveNewCandy(candyName, newCandy);
						break;
					case '2':

                        var candyToEat = SubtractCandyType(db, "eat");
                        /** eat candy
						 * select a candy type
                         **/
                        var AmountToEat = GetAmount("eating");

                        db.LoseCandy(candyToEat.KeyChar, AmountToEat);

                         /**
						 * 
						 * select specific candy details to eat from list filtered to selected candy type
						 * 
						 * enjoy candy
						 */
                        break;
					case '3':

                        var candyToToss = SubtractCandyType(db, "get rid of");
                        var amountToLose = GetAmount("throwing away");

                        db.LoseCandy(candyToToss.KeyChar, amountToLose);

                        /** throw away candy
						 * select a candy type
						 * if(moreDifficultDataModel) enhancement - give user the option to throw away old candy in one action. this would require capturing the detail of when the candy was new.
						 * 
						 * select specific candy details to throw away from list filtered to selected candy type
						 * 
						 * cry for lost candy
						 */
                        break;
					case '4':

                        var candyToGive = SubtractCandyType(db, "give up");
                        var amountToGive = GetAmount("giving away");

                        db.LoseCandy(candyToGive.KeyChar, amountToGive);
                        //db.GiveToFriend(candyToGive.KeyChar, amountToGive);

						/** give candy
						 * feel free to hardcode your users. no need to create a whole UI to register users.
						 * no one is impressed by user registration unless it's just amazingly fast & simple
						 * 
						 * select candy in any manner you prefer.
						 * it may be easiest to reuse some code for throwing away candy since that's basically what you're doing. except instead of throwing it away, you're giving it away to another user.
						 * you'll need a way to select what user you're giving candy to.
						 * one design suggestion would be to put candy "on the table" and then "give the candy on the table" to another user once you've selected all the candy to give away
						 */
						break;
					case '5':

                        var candyToTrade = SubtractCandyType(db, "offer up to trade");
                        var amountToTrade = GetAmount("offering to trade");

                        db.LoseCandy(candyToTrade.KeyChar, amountToTrade);
                        //db.AddToTable(candyToTrade.KeyChar, amountToTrade);
						/** trade candy
						 * this is the next logical step. who wants to just give away candy forever?
						 */
						break;
					default: // what about requesting candy? like a wishlist. that would be cool.
						break;
				}
			}
		}

        static DatabaseContext SetupNewApp()
		{
			Console.Title = "Cross Confectioneries Incorporated";

			var cSharp = 554;
			var db = new DatabaseContext(tone: cSharp);

			Console.SetWindowSize(60, 40);
			Console.SetBufferSize(60, 40);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			return db;
		}

		static ConsoleKeyInfo MainMenu(DatabaseContext db)
		{
            View mainMenu = new View()
                    .AddMenuOption("Did you just get some new candy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to throw away the bad candy? Dump it here.")
                    .AddMenuOption("Feeling generous? Give away some candy here.")
                    .AddMenuOption("Looking to make a trade? Make an offer here.")
                    .AddMenuText("Press 0 to exit.")
                    .AddMenuText(db.ShowTaffyCount())
                    .AddMenuText(db.ShowCandyCoatedCount())
                    .AddMenuText(db.ShowChocolateBarCount())
                    .AddMenuText(db.ShowZagnutCount());


            Console.Write(mainMenu.GetFullMenu());

			ConsoleKeyInfo userOption = Console.ReadKey();
			return userOption;
		}

		static ConsoleKeyInfo AddNewCandyType(DatabaseContext db)
		{
			var candyTypes = db.GetCandyTypes();

			var newCandyMenu = new View()
					.AddMenuText("What type of candy did you get?")
					.AddMenuOptions(candyTypes);

			Console.Write(newCandyMenu.GetFullMenu());

			ConsoleKeyInfo selectedCandyType = Console.ReadKey();
			return selectedCandyType;
		}

        static ConsoleKeyInfo SubtractCandyType(DatabaseContext db, string action)
        {
            var candyTypes = db.GetCandyTypes();

            var newCandyMenu = new View()
                    .AddMenuText($"What type of candy did you want to {action}?")
                    .AddMenuOptions(candyTypes)
                    .AddMenuText(db.ShowTaffyCount())
                    .AddMenuText(db.ShowCandyCoatedCount())
                    .AddMenuText(db.ShowChocolateBarCount())
                    .AddMenuText(db.ShowZagnutCount());

            Console.Write(newCandyMenu.GetFullMenu());

            ConsoleKeyInfo selectedCandyType = Console.ReadKey();
            return selectedCandyType;
        }

        static int GetAmount(string action)
        {
            var amountMenu = new View()
                .AddMenuText($"How many are you {action}?");

            Console.Write(amountMenu.GetFullMenu());

            int selectedCandyAmount = int.Parse(Console.ReadLine());
            return selectedCandyAmount;
        }

        static string GetBrand()
        {
            var brandMenu = new View()
                .AddMenuText("What company makes this candy?")
                .AddMenuText("Press [Enter] to continue.");

            Console.Write(brandMenu.GetFullMenu());
            string Brand = Console.ReadLine();
            return Brand;
        }

        static string GetSize()
        {

            var sizeMenu = new View()
                .AddMenuText("What size candy is it?")
                .AddMenuText("For example: King sized, bite sized, regular.")
                .AddMenuText("Press [Enter] to continue.");
                
            Console.Write(sizeMenu.GetFullMenu());
            string size = Console.ReadLine();
            return size;
        }

        static string GetCandyName()
        {
            var sizeMenu = new View()
                .AddMenuText("What's the name of the candy?")
                .AddMenuText("Press [Enter] to continue.");

            Console.Write(sizeMenu.GetFullMenu());
            string candyName = Console.ReadLine();
            return candyName;
        }
    }
}
