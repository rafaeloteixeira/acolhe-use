using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class HorarioAcaoRepository : Repository<HorarioAcao>, IHorarioAcaoRepository
    {
        public HorarioAcaoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
    }
}
