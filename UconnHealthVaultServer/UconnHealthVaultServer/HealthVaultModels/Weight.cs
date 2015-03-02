using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Weight : HealthItem
    {
        public String Value;
        public DateTime When;

        public Weight(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Value = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/weight/value/kg");

            string aBirthDay = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/weight/when/date/d");
            string aBirthMonth = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/weight/when/date/m");
            string aBirthYear = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/weight/when/date/y");


            if (aBirthDay != null && aBirthMonth != null && aBirthYear != null)
            When = DateTime.Parse(aBirthMonth + "-" + aBirthDay + "-" + aBirthYear);

        }

        internal Microsoft.Health.ItemTypes.Weight HydrateMicrosoftType(Microsoft.Health.ItemTypes.Weight pWeight)
        {
            double value;
            double.TryParse(this.Value, out value);

            if (pWeight == null)
            {
                pWeight = new Microsoft.Health.ItemTypes.Weight();
            }

            pWeight.When = new HealthServiceDateTime(this.When);
            pWeight.Value = new WeightValue(){Kilograms = value};

            return pWeight;
        }
    }
}