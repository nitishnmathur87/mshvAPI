using System;
using System.Collections.Generic;
using System.Xml;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Person : HealthItem
    {
        public String FirstName;
        public String LastName;
        public DateTime BirthDate;
        public List<Height> Heights;
        public List<Weight> Weights;
        public Demographic Demograph;

        public Person(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            FirstName = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/personal/name/first");
            LastName = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/personal/name/last");

            string aBirthDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/personal/birthdate/date/d");
            string aBirthMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/personal/birthdate/date/m");
            string aBirthYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/personal/birthdate/date/y");

            if(aBirthDay != null && aBirthMonth != null && aBirthYear != null)
            BirthDate = DateTime.Parse(aBirthMonth + "-" + aBirthDay + "-" + aBirthYear);

        }
    }
}