using System;
using System.Xml;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Demographic : HealthItem
    {
        public String Gender;
        public int BirthYear;
        public String Country;
        public String PostalCode;
        public String State;

        public Demographic(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Gender = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/basic/gender");
            BirthYear = Int32.Parse(XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/basic/birthyear"));
            Country = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/basic/country");
            PostalCode = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/basic/postcode");
            State = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/basic/state");

        }
    }
}