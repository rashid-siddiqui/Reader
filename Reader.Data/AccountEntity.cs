namespace Reader.Data
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;

    public sealed class AccountEntity : EntityBase
    {
        #region Properties

        public decimal balance { get; set; }

        public string email_address { get; set; }

        public SortedDictionary<string, List<string>> feeds { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime last_login { get; set; }

        public byte[] password_hash { get; set; }

        public byte[] password_salt { get; set; }

        #endregion

        #region Derived Properties

        [BsonIgnore]
        public override string[] index_keys
        {
            get {
                return new[] { "email_address" };
            }
        }

        #endregion

        #region Constructor(s)

        public AccountEntity() { }

        public AccountEntity(string email_address, string password, decimal balance)
        {
            this.balance = balance;
            this.email_address = email_address;
            this.feeds = new SortedDictionary<string, List<string>>();
            this.last_login = DateTime.UtcNow;
            this.password_salt = Guid.NewGuid().ToByteArray();
            this.password_hash = SHA512.Create().ComputeHash(
                Encoding.Unicode.GetBytes(password).Concat(this.password_salt).ToArray()
            );
        }

        #endregion
    }
}