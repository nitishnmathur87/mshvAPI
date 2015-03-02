using System;
using System.Xml;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class HealthItem
    {
        public String Key;
        public String TypeId;

        public HealthItem(String pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            Key = XmlParserHelper.GetNodeValue(aDoc, "thing/thing-id");
            TypeId = XmlParserHelper.GetNodeValue(aDoc, "thing/type-id");

        }
    }
}