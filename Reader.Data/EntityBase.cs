namespace Reader.Data
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;

    /// <summary>
    /// Base class for all objects stored in the database.
    /// </summary>
    public abstract class EntityBase
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string id { get; set; }

        [BsonIgnore]
        public DateTime created
        {
            get
            {
                return ObjectId.Parse(this.id).CreationTime;
            }
        }
    }
}