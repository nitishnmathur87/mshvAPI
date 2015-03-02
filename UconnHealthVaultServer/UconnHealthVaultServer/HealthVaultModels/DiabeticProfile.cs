using System;
using System.Xml;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class DiabeticProfile : HealthItem
    {
        public string GlucoseLowerBound;
        public string GlucoseUpperBound;
        public DateTime When;

        public DiabeticProfile(String pXmlItem) : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            GlucoseLowerBound = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/diabetic-profile/target-glucose-zone-group/target-glucose-zone/lower-bound/absolute-glucose/mmolPerL");
            GlucoseUpperBound = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/diabetic-profile/target-glucose-zone-group/target-glucose-zone/upper-bound/absolute-glucose/mmolPerL");

            string aWhenDay = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/diabetic-profile/when/date/d");
            string aWhenMonth = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/diabetic-profile/when/date/m");
            string aWhenYear = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/diabetic-profile/when/date/y");
            
            if (aWhenDay != null && aWhenMonth != null && aWhenYear != null)
                When = DateTime.Parse(aWhenMonth + "-" + aWhenDay + "-" + aWhenYear);

        }

        internal Microsoft.Health.ItemTypes.DiabeticProfile HydrateMicrosoftType(Microsoft.Health.ItemTypes.DiabeticProfile pDiabeticProfile)
        {
            double glucoseupperbound;
            double glucoselowerbound;

            double.TryParse(this.GlucoseLowerBound, out glucoselowerbound);
            double.TryParse(this.GlucoseUpperBound, out glucoseupperbound);

            if (pDiabeticProfile == null)
            {
                pDiabeticProfile = new Microsoft.Health.ItemTypes.DiabeticProfile();
            }

            pDiabeticProfile.When = new HealthServiceDateTime(this.When);
            pDiabeticProfile.TargetGlucoseZoneGroups.Add(new TargetGlucoseZoneGroup());

            pDiabeticProfile.TargetGlucoseZoneGroups[0].TargetZones.Add(new TargetGlucoseZone());
            pDiabeticProfile.TargetGlucoseZoneGroups[0].TargetZones[0].AbsoluteLowerBoundary = new BloodGlucoseMeasurement(glucoselowerbound);
            pDiabeticProfile.TargetGlucoseZoneGroups[0].TargetZones[0].AbsoluteUpperBoundary = new BloodGlucoseMeasurement(glucoseupperbound);

            return pDiabeticProfile;
        }
    }
}