using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Vital : HealthItem
    {
        public String Title;
        public String Unit;
        public String Value;
        public DateTime When;

        public Vital(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Title = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/vital-signs/vital-signs-results/title/text");
            Value = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/vital-signs/vital-signs-results/value");
            Unit = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/vital-signs/vital-signs-results/unit/text");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/vital-signs/when/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/vital-signs/when/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/vital-signs/when/date/y");

            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal VitalSigns HydrateMicrosoftType(VitalSigns pVital)
        {
            if (pVital == null)
            {
                pVital = new VitalSigns();
            }

            double value;
            double.TryParse(this.Value, out value);

            pVital.When = new HealthServiceDateTime(this.When);
            pVital.VitalSignsResults.Add(new VitalSignsResultType());
            pVital.VitalSignsResults[0].Title.Text = this.Title;
            pVital.VitalSignsResults[0].Value = value;
            pVital.VitalSignsResults[0].Unit = new CodableValue();
            pVital.VitalSignsResults[0].Unit.Text = this.Unit;

            return pVital;
        }
    }
}