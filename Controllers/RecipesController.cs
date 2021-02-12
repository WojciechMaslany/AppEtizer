using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEtizer.Models;
using MongoDB.Driver;
using AppEtizer.Databases;

namespace AppEtizer.Controllers
{

    public class RecipesController : Controller
    {
        MongoCRUD db = new MongoCRUD("Recipes");

        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(RecipesModel recipe)
        {


            db.InsertRecord("Recipes", new RecipesModel
            {
                RecipeName = recipe.RecipeName,
                RecipeDescription = recipe.RecipeDescription,
                RecipeDifficulty = recipe.RecipeDifficulty,
                RecipeIngredients = recipe.RecipeIngredients,
                RecipePrepareTime = recipe.RecipePrepareTime,
                RecipeServings = recipe.RecipeServings,
                RecipeSteps = recipe.RecipeSteps
            });

            return View("ShowRecipe", recipe);
        }

        public IActionResult RecipesList()
        {
            var items = db.LoadRecords<RecipesModel>("Recipes").ToArray<RecipesModel>();

            return View("RecipesList", items);
        }
    }
}