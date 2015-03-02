using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class Emotion : HealthItem
    {
        public String Mood;
        public String Stress;
        public String WellBeing;
        public DateTime When;

        public Emotion(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Mood = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/mood");
            Stress = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/stress");
            WellBeing = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/wellbeing");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/when/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/when/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/emotion/when/date/y");


            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.Emotion HydrateMicrosoftType(Microsoft.Health.ItemTypes.Emotion pEmotion)
        {


            int mood;
            int stress;
            int wellbeing;

            int.TryParse(this.Mood, out mood);
            int.TryParse(this.Stress, out stress);
            int.TryParse(this.WellBeing, out wellbeing);

            if (pEmotion == null)
            {
                pEmotion = new Microsoft.Health.ItemTypes.Emotion();
            }

            pEmotion.When = new HealthServiceDateTime(this.When);
            pEmotion.Mood = (Mood)mood;
            pEmotion.Stress = (RelativeRating)stress;
            pEmotion.Wellbeing = (Wellbeing)wellbeing;

            return pEmotion;
        }
    }
}