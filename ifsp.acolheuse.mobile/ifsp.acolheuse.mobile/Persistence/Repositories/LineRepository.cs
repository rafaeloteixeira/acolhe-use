using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class LineRepository : Repository<Line>, ILineRepository
    {
        public LineRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Line";
        }
    }
}
