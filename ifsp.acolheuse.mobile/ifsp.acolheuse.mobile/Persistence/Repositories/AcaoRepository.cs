using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class AcaoRepository : Repository<Acao>, IAcaoRepository
    {
        public AcaoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }

        public Acao GetCompleteAcao(int id)
        {
            throw new NotImplementedException();
        }
    }
}
