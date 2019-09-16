using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class EstagiarioRepository : Repository<Estagiario>, IEstagiarioRepository
    {
        public EstagiarioRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
    }
}
