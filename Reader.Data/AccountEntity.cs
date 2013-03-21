namespace Reader.Data
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class AccountEntity : EntityBase
    {
        public decimal balance { get; set; }

        public string email_address { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime last_login { get; set; }

        [BsonIgnore]
        public override string[] index_keys
        {
            get {
                return new[] { "email_address" };
            }
        }
    }
}