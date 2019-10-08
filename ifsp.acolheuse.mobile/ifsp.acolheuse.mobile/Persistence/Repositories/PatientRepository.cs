using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Patient";
        }
    }
}
