using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class ServidorRepository : Repository<Servidor>, IServidorRepository
    {
        public ServidorRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
    }
}
