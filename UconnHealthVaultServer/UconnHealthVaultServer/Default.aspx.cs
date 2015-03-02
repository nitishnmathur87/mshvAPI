using System;
using Microsoft.Health.Web;

namespace UconnHealthVaultServer
{
    public partial class Default : HealthServicePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RecordIdLbl.Text = String.Format("{0},{1}", PersonInfo.PersonId, PersonInfo.SelectedRecord.Id);
        }
    }
}
