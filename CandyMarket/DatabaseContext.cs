using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
	internal class DatabaseContext
	{
		private int _countOfTaffy;
		private int _countOfCandyCoated;
		private int _countOfChocolateBar;
		private int _countOfZagnut;

        /**
		 * this is just an example.
		 * feel free to modify the definition of this collection "BagOfCandy" if you choose to implement the more difficult data model.
		 */
        //Dictionary<CandyType, List<Candy>> BagOfCandy { get; set; }

        public DatabaseContext(int tone) => Console.Beep(tone, 2500);

		internal List<string> GetCandyTypes()
		{
            return Enum
				.GetNames(typeof(CandyType))
				.Select(candyType =>
					candyType.Humanize(LetterCasing.Title))
				.ToList();
		}

		internal void SaveNewCandy(char selectedCandyMenuOption, int amount)
		{
			var candyOption = int.Parse(selectedCandyMenuOption.ToString());

			var maybeCandyMaybeNot = (CandyType)selectedCandyMenuOption;
			var forRealTheCandyThisTime = (CandyType)candyOption;

			switch (forRealTheCandyThisTime)
			{
				case CandyType.TaffyNotLaffy:
					_countOfTaffy += amount;
					break;
				case CandyType.CandyCoated:
					_countOfCandyCoated += amount;
					break;
				case CandyType.CompressedSugar:
					_countOfChocolateBar += amount;
					break;
				case CandyType.ZagnutStyle:
					_countOfZagnut += amount;
					break;
				default:
					break;
			}
		}

        internal void LoseCandy(char selectedCandy, int amount)
        {
            var candyOption = int.Parse(selectedCandy.ToString());

            var maybeCandyMaybeNot = (CandyType)selectedCandy;
            var forRealTheCandyThisTime = (CandyType)candyOption;

            switch (forRealTheCandyThisTime)
            {
                case CandyType.TaffyNotLaffy:
                    if (_countOfTaffy < 1)
                    {
                        break;
                    }
                    _countOfTaffy -= amount;
                    break;
                case CandyType.CandyCoated:
                    if (_countOfCandyCoated < 1)
                    {
                        break;
                    }
                    _countOfCandyCoated -= amount;
                    break;
                case CandyType.CompressedSugar:
                    if (_countOfChocolateBar < 1)
                    {
                        break;
                    }
                    _countOfChocolateBar -= amount;
                    break;
                case CandyType.ZagnutStyle:
                    if (_countOfZagnut < 1)
                    {
                        break;
                    }
                    _countOfZagnut -= amount;
                    break;
                default:
                    break;
            }
        }

        public string ShowTaffyCount()
        {
            if (_countOfTaffy < 1)
            {
                return NoCandy(CandyType.TaffyNotLaffy);
            }
            return $"You have {_countOfTaffy} pieces of Taffy";
        }
        public string ShowCandyCoatedCount()
        {
            if (_countOfCandyCoated < 1)
            {
                return NoCandy(CandyType.CandyCoated);
            }
            return $"You have {_countOfCandyCoated} pieces of Candy Coated candies";
        }
        public string ShowChocolateBarCount()
        {
            if (_countOfChocolateBar < 1)
            {
                return NoCandy(CandyType.CompressedSugar);
            }
            return $"You have {_countOfChocolateBar} Chocolate Bars";
        }
        public string ShowZagnutCount()
        {
            if (_countOfZagnut < 1)
            {
                return NoCandy(CandyType.ZagnutStyle);
            }
            return $"You have {_countOfZagnut} pieces of Zagnut";
        }

        private string NoCandy(CandyType candyType)
        {
            return $"You don't have any {candyType} candies.";
        }
    }
}