using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Medication : HealthItem
    {
        public String Name;
	    public String Strength;
	    public String Dose;
	    public String HowTaken;
	    public String Frequency;
	    public DateTime DatePrescribed;
	    public DateTime DateStarted;
	    public DateTime DateDiscontinued;
        public String PrescribedBy;
	    public String Note;

        public Medication(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Name = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/name");
            Strength = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/strength/display");
            Dose = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/dose/display");
            HowTaken = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/prescription/instructions");
            Frequency = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/frequency");
            PrescribedBy = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/prescription/prescribed-by/name/full");
            Note = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/indication");

            var aDatePrescribedYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/prescription/date-prescribed/structured/date/y");
            var aDatePrescribedMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/prescription/date-prescribed/structured/date/m");
            var aDatePrescribedDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/prescription/date-prescribed/structured/date/d");

            if (aDatePrescribedMonth != null && aDatePrescribedDay != null && aDatePrescribedYear != null)
                DatePrescribed = DateTime.Parse(aDatePrescribedMonth + "-" + aDatePrescribedDay + "-" + aDatePrescribedYear);

            var aDateStartedYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-started/structured/date/y");
            var aDateStartedMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-started/structured/date/m");
            var aDateStartedDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-started/structured/date/d");

            if (aDateStartedMonth != null && aDateStartedDay != null && aDateStartedYear != null)
                DateStarted = DateTime.Parse(aDateStartedMonth + "-" + aDateStartedDay + "-" + aDateStartedYear);

            var aDateDiscontinuedYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-discontinued/structured/date/y");
            var aDateDiscontinuedMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-discontinued/structured/date/m");
            var aDateDiscontinuedDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/medication/date-discontinued/structured/date/d");

            if (aDateDiscontinuedMonth != null && aDateDiscontinuedDay != null && aDateDiscontinuedYear != null)
                DateDiscontinued = DateTime.Parse(aDateDiscontinuedMonth + "-" + aDateDiscontinuedDay + "-" + aDateDiscontinuedYear);
            
        }

        public Microsoft.Health.ItemTypes.Medication HydrateMicrosoftType(Microsoft.Health.ItemTypes.Medication pMedication)
        {
            if (pMedication == null)
            {
                pMedication = new Microsoft.Health.ItemTypes.Medication();
            }

            pMedication.Name = new CodableValue(this.Name);
            pMedication.Strength = new GeneralMeasurement(this.Strength);
            pMedication.Prescription = new Prescription();
            pMedication.Prescription.Instructions = new CodableValue(this.HowTaken);
            pMedication.Dose = new GeneralMeasurement(this.Dose);
            pMedication.Frequency = new GeneralMeasurement(this.Frequency);
            pMedication.Indication = new CodableValue(this.Note);
            pMedication.DateStarted = new ApproximateDateTime(this.DateStarted);
            pMedication.DateDiscontinued = new ApproximateDateTime(this.DateDiscontinued);
            pMedication.Prescription.DatePrescribed = new ApproximateDateTime(this.DatePrescribed);
            pMedication.Prescription.PrescribedBy = new PersonItem(new Name(this.PrescribedBy));

            return pMedication;

        }
    }
}