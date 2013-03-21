namespace Reader.Services
{
    using System;
    using System.Linq;
    using Reader.Data;
    using System.Collections.Generic;
    using System.Security.Principal;

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

        public static bool Create(string email_address, decimal initial_balance)
        {
            if (Accounts.Exists(email_address)) 
            {
                return false;
            }

            var entity = new AccountEntity()
            {
                balance = initial_balance,
                email_address = email_address,
                last_login = DateTime.UtcNow,
                feeds = new SortedDictionary<string,List<string>>()
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