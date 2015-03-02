using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class PeakFlow : HealthItem
    {
        public string ForcedExpiratoryFlow;
        public string PeakExpiratoryFlow;
        public DateTime When;

        public PeakFlow(string pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            ForcedExpiratoryFlow = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/peak-flow/pef/liters-per-second");
            PeakExpiratoryFlow = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/peak-flow/fev1/liters");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/peak-flow/when/structured/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/peak-flow/when/structured/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/peak-flow/when/structured/date/y");

            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.PeakFlow HydrateMicrosoftType(Microsoft.Health.ItemTypes.PeakFlow pPeakFlow)
        {
            double aForcedExpiratoryFlow;
            double.TryParse(this.ForcedExpiratoryFlow, out aForcedExpiratoryFlow);

            double aPeakExpiratoryFlow;
            double.TryParse(this.PeakExpiratoryFlow, out aPeakExpiratoryFlow);

            if (pPeakFlow == null)
            {
                pPeakFlow = new Microsoft.Health.ItemTypes.PeakFlow();
            }
            pPeakFlow.When = new ApproximateDateTime(this.When);
            pPeakFlow.Fev1 = new VolumeMeasurement(aForcedExpiratoryFlow);
            pPeakFlow.Pef = new FlowMeasurement(aPeakExpiratoryFlow);
            return pPeakFlow;
        }
    }
}