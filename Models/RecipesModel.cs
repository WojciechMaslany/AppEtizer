using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using AppEtizer.Models.Comments;

namespace AppEtizer.Models
{
    public class RecipesModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string RecipeName { get; set; }

        public string RecipeDescription { get; set; }

        public string RecipeIngredients { get; set; }

        public string RecipeDifficulty { get; set; }

        public int RecipePrepareTime { get; set; }

        public int RecipeServings { get; set; }

        public string RecipeSteps { get; set; }

        public List<MainComment> MainComments { get; set; }
    }
}