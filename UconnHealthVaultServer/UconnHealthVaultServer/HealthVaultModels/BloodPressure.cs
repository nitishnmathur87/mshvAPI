using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class BloodPressure : HealthItem
    {
        public string Systolic;
        public string Diastolic;
        public string Pulse;
        public string IrregularPulse;
        public DateTime When;

        public BloodPressure(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Systolic = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/systolic");
            Diastolic = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/diastolic");
            Pulse = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/pulse");
            IrregularPulse = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/irregular-heartbeat");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/when/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/when/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/blood-pressure/when/date/y");
            
            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.BloodPressure HydrateMicrosoftType(Microsoft.Health.ItemTypes.BloodPressure pBloodPressure)
        {
            int systolic;
            int diastolic;
            int pulse;
            bool irregularpulse;

            Int32.TryParse(this.Systolic, out systolic);
            Int32.TryParse(this.Diastolic, out diastolic);
            Int32.TryParse(this.Pulse, out pulse);
            bool.TryParse(this.IrregularPulse, out irregularpulse);

            if (pBloodPressure == null)
            {
                pBloodPressure = new Microsoft.Health.ItemTypes.BloodPressure();
            }

            pBloodPressure.When = new HealthServiceDateTime(this.When);
            pBloodPressure.Systolic = systolic;
            pBloodPressure.Diastolic = diastolic;
            pBloodPressure.Pulse = pulse;
            pBloodPressure.IrregularHeartbeatDetected = irregularpulse;

            return pBloodPressure;
        }
    }
}