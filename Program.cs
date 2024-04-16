using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueOperation = true;
            Recipe recipe = null;

            while (continueOperation)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Display Recipe");
                Console.WriteLine("2. Scale Recipe");
                Console.WriteLine("3. Reset Quantities");
                Console.WriteLine("4. Add Recipe");
                Console.WriteLine("5. Clear All Data");
                Console.WriteLine("6. Exit");
                Console.WriteLine("----------------------");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        if (recipe != null)
                        {
                            Console.WriteLine("----------------------");
                            recipe.DisplayRecipe();
                            Console.WriteLine("----------------------");
                        }
                        else
                        {
                            Console.WriteLine("No recipe available. Please add a recipe first.");
                        }
                        break;
                    case 2:
                        if (recipe != null)
                        {
                            Console.WriteLine("----------------------");
                            recipe.ScaleRecipe();
                            Console.WriteLine("----------------------");
                        }
                        else
                        {
                            Console.WriteLine("No recipe available. Please add a recipe first.");
                        }
                        break;
                    case 3:
                        if (recipe != null)
                        {
                            Console.WriteLine("----------------------");
                            recipe.ResetQuantities();
                            Console.WriteLine("----------------------");
                        }
                        else
                        {
                            Console.WriteLine("No recipe available. Please add a recipe first.");
                        }
                        break;
                    case 4:
                        recipe = new Recipe();
                        recipe.GetRecipeDetails();
                        break;
                    case 5:
                        if (recipe != null)
                        {
                            recipe = null;
                            Console.WriteLine("All data cleared.");
                        }
                        else
                        {
                            Console.WriteLine("No data to clear.");
                        }
                        break;
                    case 6:
                        continueOperation = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            }
        }
    }

    class Recipe
    {
        private Ingredient[] ingredients;
        private Step[] steps;
        private Ingredient[] originalIngredients;

        
        public void GetRecipeDetails() //This method gets the recipe details and initialises the arrays
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients = Convert.ToInt32(Console.ReadLine());
            ingredients = new Ingredient[numIngredients];
            originalIngredients = new Ingredient[numIngredients];

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");
                ingredients[i] = new Ingredient();
                ingredients[i].GetIngredientDetails();
                originalIngredients[i] = new Ingredient(ingredients[i]);
            }

            Console.WriteLine("Enter the number of steps:");
            int numSteps = Convert.ToInt32(Console.ReadLine());
            steps = new Step[numSteps];

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                steps[i] = new Step();
                steps[i].GetStepDetails();
            }
        }

        
        public void DisplayRecipe() //This method display's the recipe details
        {
            Console.WriteLine("Recipe Details:");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }

            Console.WriteLine("Steps:");
            foreach (var step in steps)
            {
                Console.WriteLine($"{step.Description}");
            }
        }

        
        public void ScaleRecipe() 
        {
            Console.WriteLine("Enter the scaling factor (0.5, 2, or 3):");
            double factor = Convert.ToDouble(Console.ReadLine());

            foreach (var ingredient in ingredients)
            {
                ingredient.ScaleQuantity(factor);
            }

            Console.WriteLine("Scaled Recipe:");
            DisplayRecipe();
        }

        
        public void ResetQuantities() //This method will reset ingredient quantities to their original values
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                ingredients[i].Quantity = originalIngredients[i].Quantity;
            }
            Console.WriteLine("Quantities Reset to Original:");
            DisplayRecipe();
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        public Ingredient()
        {
        }

        public Ingredient(Ingredient original)
        {
            Name = original.Name;
            Quantity = original.Quantity;
            Unit = original.Unit;
        }

        
        public void GetIngredientDetails() // This method will get the ingredient details from the user input
        {
            Console.Write("Enter ingredient name: ");
            Name = Console.ReadLine();

            bool validInput = false;
            while (!validInput)
            {
                Console.Write("Enter quantity: ");
                string quantityInput = Console.ReadLine();
                validInput = double.TryParse(quantityInput, out double quantity);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid quantity.");
                }
                else
                {
                    Quantity = quantity;
                }
            }

            Console.Write("Enter unit of measurement: ");
            Unit = Console.ReadLine();
        }

        
        public void ScaleQuantity(double factor) // Ingredient scaler, scale the quantity by either 0.5, 2, or 3
        {
            Quantity *= factor;
        }
    }

    class Step
    {
        public string Description { get; set; }

        
        public void GetStepDetails() // Get the step details from the user's input
        {
            Description = Console.ReadLine();
        }
    }
}
