using System;
using Microsoft.Health;
using Microsoft.Health.Web;

namespace UconnHealthVaultServer
{
    public static class HealthAuth
    {
        public static HealthRecordAccessor Authenticate(string pPublicId, string pRecordId)
        {
            var personId = new Guid(pPublicId);
            var recordGuid = new Guid(pRecordId);

            var offlineConn =
                new OfflineWebApplicationConnection(personId);

            offlineConn.Authenticate();

            var accessor =
                new HealthRecordAccessor(offlineConn, recordGuid);
            return accessor;
        }
    }
}