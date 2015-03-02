using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Height : HealthItem
    {
        public String Value;
        public DateTime When;

        public Height(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Value = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/height/value/m");

            string aBirthDay = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/height/when/date/d");
            string aBirthMonth = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/height/when/date/m");
            string aBirthYear = XmlParserHelper.GetNodeValue(aDoc,"thing/data-xml/height/when/date/y");

            if (aBirthDay != null && aBirthMonth != null && aBirthYear != null)
            When = DateTime.Parse(aBirthMonth + "-" + aBirthDay + "-" + aBirthYear);

        }

        internal Microsoft.Health.ItemTypes.Height HydrateMicrosoftType(Microsoft.Health.ItemTypes.Height pHeight)
        {
            double value;
            double.TryParse(this.Value, out value);

            if (pHeight == null)
            {
                pHeight = new Microsoft.Health.ItemTypes.Height();
            }

            pHeight.When = new HealthServiceDateTime(this.When);
            pHeight.Value = new Length(value);

            return pHeight;
        }
    }
}