namespace Reader.Services
{
    using System;
    using System.Linq;
    using Reader.Data;

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
                last_login = DateTime.UtcNow
            };

            return Accounts.Entities.Insert(entity);
        }
    }
}