namespace Reader.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using Reader.Data;
    using Reader.StaticAnalysis;

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
            Ensure.IsNotNullOrWhitespace<ArgumentException>(email_address);
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
            email_address = email_address.ToLower();

            if (Accounts.Exists(email_address)) return false;

            var entity = new AccountEntity(email_address, plaintext_password, initial_balance);
            return Accounts.Entities.Insert(entity);
        }

        public static bool CanSignIn(string email_address, string password)
        {
            Ensure.IsNotNullOrWhitespace<ArgumentException>(email_address);
            email_address = email_address.ToLower();

            // Ensure that an account exits
            var account = Accounts.Get(email_address);
            if (account == null) return false;

            return account.IsPasswordValid(password);
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

        public static AccountEntity Get (string email_address)
        {
            Ensure.IsNotNullOrWhitespace<ArgumentException>(email_address);
            email_address = email_address.ToLower();

            return Accounts.Entities.Single(acct => acct.email_address == email_address);
        }

        public static AccountEntity Account (this IPrincipal User)
        {
            Ensure.IsNotNull<ArgumentException>(User);

            return Accounts.Entities.Single(acct => acct.email_address == User.Identity.Name);
        }
    }
}