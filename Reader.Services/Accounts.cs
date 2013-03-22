namespace Reader.Services
{
    using System;
    using System.Linq;
    using Reader.Data;
    using System.Collections.Generic;
    using System.Security.Principal;
    using Reader.StaticAnalysis;
    using System.Security.Cryptography;

    public static class Accounts
    {
        private static EntityRepository<AccountEntity> Entities
        {
            get
            {
                return new EntityRepository<AccountEntity>();
            }
        }

        public static bool Exists (string email_address)
        {
            email_address = email_address.ToLower();

            return Accounts.Entities.SingleOrDefault(
                acct => acct.email_address == email_address
            ) != null;
        }

        public static bool Register(string email_address, string plaintext_password, decimal initial_balance)
        {
            Ensure.IsNotNullOrWhitespace<ArgumentException>(email_address);
            Ensure.IsNotNullOrWhitespace<ArgumentException>(plaintext_password);
            Ensure.IsTrue<ArgumentException>(initial_balance >= 0.0M);

            if (Accounts.Exists(email_address)) 
            {
                return false;
            }

            var entity = new AccountEntity()
            {
                balance = initial_balance,
                email_address = email_address,
                last_login = DateTime.UtcNow,
                feeds = new SortedDictionary<string,List<string>>(),
                password_salt = Guid.NewGuid().ToByteArray()
            };

           

            return Accounts.Entities.Insert(entity);
        }

        public static bool AddFeedTo(IPrincipal User, string feed_id)
        {
            var account = Entities.Single(acct => acct.email_address == User.Identity.Name);
            if (!account.feeds.ContainsKey(feed_id))
            {
                account.feeds.Add(feed_id, new List<string>());
            }

            return Accounts.Entities.Save(account);
        }

        public static AccountEntity Get(IPrincipal User)
        {
            return Accounts.Entities.Single(acct => acct.email_address == User.Identity.Name);
        }
    }
}