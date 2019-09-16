using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class LinhaRepository : Repository<Linha>, ILinhaRepository
    {
        public LinhaRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
    }
}
