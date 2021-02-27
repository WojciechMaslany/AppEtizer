using AppEtizer.Data.FileManager;
using AppEtizer.Databases;
using AppEtizer.Models;
using AppEtizer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppEtizer.Controllers
{

    public class RecipesController : Controller
    {
        MongoCRUD db = new MongoCRUD("Recipes");
        private IFileManager _fileManager;

        public RecipesController(
            IFileManager fileManager
            )
        {
            _fileManager = fileManager;
        }
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image (string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeViewModel recipe)
        {

            var model = new RecipesModel
            {
                RecipeName = recipe.RecipeName,
                RecipeDescription = recipe.RecipeDescription,
                RecipeDifficulty = recipe.RecipeDifficulty,
                RecipeIngredients = recipe.RecipeIngredients,
                RecipePrepareTime = recipe.RecipePrepareTime,
                RecipeServings = recipe.RecipeServings,
                RecipeSteps = recipe.RecipeSteps,
                Image = await _fileManager.SaveImage(recipe.Image)
            };

            db.InsertRecord("Recipes", model);

            return View("ShowRecipe", model);
        }

        public IActionResult RecipesList()
        {
            var items = db.LoadRecords<RecipesModel>("Recipes").ToArray<RecipesModel>();

            return View("RecipesList", items);
        }

        public IActionResult ShowRecipe(RecipesModel recipesModel)
        {
            db.LoadRecordById<RecipesModel>("Recipes", recipesModel.RecipeId);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return View(new RecipeViewModel());
            }
            else
            {
                var getRecipe = db.LoadRecordById<RecipesModel>("Recipes", id);
                return View(new RecipeViewModel
                {
                    RecipeId = getRecipe.RecipeId,
                    RecipeName = getRecipe.RecipeName,
                    RecipeDescription = getRecipe.RecipeDescription,
                    RecipeIngredients = getRecipe.RecipeIngredients,
                    RecipeDifficulty = getRecipe.RecipeDifficulty,
                    RecipePrepareTime = getRecipe.RecipePrepareTime,
                    RecipeServings = getRecipe.RecipeServings,
                    RecipeSteps = getRecipe.RecipeSteps,
                    CurrentImage = getRecipe.Image,
                });
            } 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeViewModel rec)
        {
            var changeRecipe = new RecipesModel
            {
                RecipeId = rec.RecipeId,
                RecipeName = rec.RecipeName,
                RecipeDescription = rec.RecipeDescription,
                RecipeIngredients = rec.RecipeIngredients,
                RecipeDifficulty = rec.RecipeDifficulty,
                RecipePrepareTime = rec.RecipePrepareTime,
                RecipeServings = rec.RecipeServings,
                RecipeSteps = rec.RecipeSteps,
            };

            if (rec.Image == null)
                changeRecipe.Image = rec.CurrentImage;
            else
            {
                if (!string.IsNullOrEmpty(rec.CurrentImage))
                    _fileManager.RemoveImage(rec.CurrentImage);

                changeRecipe.Image = await _fileManager.SaveImage(rec.Image);
            }

            if (changeRecipe.RecipeId != null)
            {
                db.UpsertRecord("Recipes", changeRecipe.RecipeName, changeRecipe);
            } else
            {
                RedirectToAction("RecipesList");
            }

            return View("ShowRecipe", changeRecipe);
        }
     
        public  IActionResult Delete(string id)
        {         
            db.DeleteRecord<RecipesModel>("Recipes", id);
            var items = db.LoadRecords<RecipesModel>("Recipes").ToArray<RecipesModel>();
            return View("RecipesList", items);   
        }
    }   
}