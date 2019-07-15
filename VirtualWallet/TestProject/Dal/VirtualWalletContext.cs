using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;

namespace VirtualWallet
{
    class VirtualWalletContext : DbContext
    {
        public VirtualWalletContext() : base("name=VWConnexionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VirtualWalletContext, VirtualWallet.Migrations.Configuration>());
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
