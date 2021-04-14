namespace Data.Repositores
{
    using Data.Contexts;
    using Data.Repositores.Abstraction;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityContext identityContext;

        public RefreshTokenRepository(IdentityContext identityContext)
        {
            this.identityContext = identityContext;
        }

        public async Task<RefreshToken> GetById(int id)
        {
            return await identityContext.RefreshTokens.FindAsync(id);
        }

        public virtual void Save()
        {
            identityContext.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await identityContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetBytoken(Guid token)
        {
            return await identityContext.RefreshTokens.FirstOrDefaultAsync(p => p.Token == token);
        }

        public async Task Create(RefreshToken refreshToken)
        {
            await identityContext.RefreshTokens.AddAsync(refreshToken);
            await SaveAsync();
        }
    }
}
