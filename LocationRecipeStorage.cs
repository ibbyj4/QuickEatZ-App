using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2425
{
    public static class LocationRecipeStorage
    {
        public static Dictionary<string, List<string>> CityRecipes = new Dictionary<string, List<string>>()
        {
            { "Manchester", new List<string> { "Manchester Meat Pie", "Northern Curry" } },
            { "Sheffield", new List<string> { "Sheffield Sausage Roll", "Parkin Cake" } },
            { "London", new List<string> { "Fish and Chips", "Full English Breakfast" } },
            { "Newcastle", new List<string> { "Stottie Sandwich", "Pease Pudding" } },
            { "Birmingham", new List<string> { "Balti Chicken", "Birmingham Biryani" } },
            { "Bradford", new List<string> { "Curry Special", "Yorkshire Pudding Wrap" } },
        };
    }
}
