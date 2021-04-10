namespace Data.Repositores
{
    using Data.Contexts;
    using Data.Repositores.Abstraction;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityContext identityContext;
        private DbSet<RefreshToken> DbSet { get; set; }

        public RefreshTokenRepository(IdentityContext identityContext)
        {
            this.identityContext = identityContext;
            DbSet = this.identityContext.Set<RefreshToken>();
        }


        public async Task<RefreshToken> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Save()
        {
            identityContext.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await identityContext.SaveChangesAsync();
        }

    }
}
