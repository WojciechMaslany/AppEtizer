﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AppEtizer.Models
{
    public class RecipesModel
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string RecipeId { get; set; }
        public string RecipeName { get; set; }

        public string RecipeDescription { get; set; }

        public string RecipeIngredients { get; set; }

        public string RecipeDifficulty { get; set; }

        public int RecipePrepareTime { get; set; }

        public int RecipeServings { get; set; }

        public string RecipeSteps { get; set; }
        
        public string Image { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
       
    }
}