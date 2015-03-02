using System;

namespace UconnHealthVaultServer.OpenEmrModels
{
    [Serializable]
    public class Prescription
    {
        public String PatientId;
	    public String StartDate;
	    public String DateAdded;
	    public String Drug;
	    public String Dosage;
	    public String Quantity;
	    public String Size;
	    public String Substitute;
        public String Refills;
        public String PerRefill;
	    public String Note;
    }
}