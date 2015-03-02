//author: gordon devoe
//description: quick way to retrieve prescriptions/medications for a patient out of openEMR.

using System.Collections.Generic;
using System.Web.Http;
using UconnHealthVaultServer.OpenEmrModels;

namespace UconnHealthVaultServer
{
    public class OpenEmrController : ApiController
    {
        // Get Prescriptions
        [HttpGet]
        public List<Prescription> GetPrescriptions(string PatientId)
        {

            return OpenEmrDal.GetPrescriptions(PatientId);

        }

        [HttpGet]
        public List<Vital> GetVitals(string PatientId)
        {
            return OpenEmrDal.GetVitals(PatientId);
        }

        // Add Vital
        [HttpPost]
        public List<Vital> AddVital([FromBody] Vital pVital)
        {
            return OpenEmrDal.AddVital(pVital);
        }
    }
}