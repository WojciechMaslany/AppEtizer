using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppEtizer.Databases
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }
        //var result = collection.ReplaceOne(
        //    new BsonDocument("_id", id),
        //    record,
        //    new UpdateOptions { IsUpsert = true });

        public T LoadRecordById<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("RecipeId", id);

            return collection.Find(filter).FirstOrDefault();
        }

        public void UpsertRecord<T>(string table, string recipeName, T record)
        {
            var collection = db.GetCollection<T>(table);
            var filter = new BsonDocument("RecipeName", recipeName);
            collection.ReplaceOne(filter, record, new ReplaceOptions { IsUpsert = true });
               
        }

        public void DeleteRecord<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.FindOneAndDelete(filter);
        }
    }
}