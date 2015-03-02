using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Condition : HealthItem
    {
        public string Name;
        public string StopReason;
        public DateTime StartDate;
        public DateTime StopDate;

        public Condition(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Name = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/name/text");
            StopReason = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/stop-reason");
           
            string aStartDateDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/onset-date/structured/date/d");
            string aStartDateMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/onset-date/structured/date/m");
            string aStartDateYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/onset-date/structured/date/y");

            if (aStartDateDay != null && aStartDateMonth != null && aStartDateYear != null)
                StartDate = DateTime.Parse(aStartDateMonth + "-" + aStartDateMonth + "-" + aStartDateYear);

            string aEndDateDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/stop-date/structured/date/d");
            string aEndDateMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/stop-date/structured/date/m");
            string aEndDateYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/condition/stop-date/structured/date/y");

            if (aEndDateDay != null && aEndDateMonth != null && aEndDateYear != null)
                StopDate = DateTime.Parse(aEndDateMonth + "-" + aEndDateDay + "-" + aEndDateYear);

        }

        internal Microsoft.Health.ItemTypes.Condition HydrateMicrosoftType(Microsoft.Health.ItemTypes.Condition pCondition)
        {
            if (pCondition == null)
            {
                pCondition = new Microsoft.Health.ItemTypes.Condition();
            }

            pCondition.Name = new CodableValue(this.Name);
            pCondition.StopReason = this.StopReason;
            pCondition.OnsetDate = new ApproximateDateTime(this.StartDate);
            pCondition.StopDate = new ApproximateDateTime(this.StopDate);

            return pCondition;
        }
    }
}