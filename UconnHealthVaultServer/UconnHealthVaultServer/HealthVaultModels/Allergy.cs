using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Allergy : HealthItem
    {
        public string AllergenType;
        public DateTime FirstObserved;
        public string Name;
        public string Reaction;

        public Allergy(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            AllergenType = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/allergen-type/text");
            Name = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/name/text");
            Reaction = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/reaction/text");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/first-observed/structured/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/first-observed/structured/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/allergy/first-observed/structured/date/y");

            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                FirstObserved = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.Allergy HydrateMicrosoftType(Microsoft.Health.ItemTypes.Allergy pAllergy)
        {
            if (pAllergy == null)
            {
                pAllergy = new Microsoft.Health.ItemTypes.Allergy();
            }

            pAllergy.Name = new CodableValue(this.Name);
            pAllergy.FirstObserved = new ApproximateDateTime(this.FirstObserved);
            pAllergy.AllergenType = new CodableValue(this.AllergenType);
            pAllergy.Reaction = new CodableValue(this.Reaction);
            return pAllergy;
        }
    }
}