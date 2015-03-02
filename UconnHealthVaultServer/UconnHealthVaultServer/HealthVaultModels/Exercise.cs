using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Exercise : HealthItem
    {
        public string Title;
        public string Distance;
        public string Duration;
        public DateTime When;

        public Exercise(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Title = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/activity/text");
            Distance = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/distance/m");
            Duration = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/duration");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/when/structured/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/when/structured/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/exercise/when/structured/date/y");
            
            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.Exercise HydrateMicrosoftType(Microsoft.Health.ItemTypes.Exercise pExercise)
        {
            double duration;
            double distance;

            double.TryParse(this.Duration, out duration);
            double.TryParse(this.Distance, out distance);

            if (pExercise == null)
            {
                pExercise = new Microsoft.Health.ItemTypes.Exercise();
            }

            pExercise.When = new ApproximateDateTime(this.When);
            pExercise.Activity = new CodableValue(this.Title);
            pExercise.Duration = duration;
            pExercise.Distance = new Length(distance);

            return pExercise;
        }
    }
}