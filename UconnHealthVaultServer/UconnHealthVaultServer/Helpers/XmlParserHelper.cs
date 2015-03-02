using System;
using System.Xml;

namespace UconnHealthVaultServer.Helpers
{
    public static class XmlParserHelper
    {
        public static String GetNodeValue(XmlDocument pDoc, String pNodeIndex)
        {
            var selectSingleNode = pDoc.SelectSingleNode(pNodeIndex);
            if (selectSingleNode != null)
                return selectSingleNode.InnerText;
            return null;
        }

        public static void SetNodeValue(XmlDocument pDoc, String pNodeIndex, String pNodeInnerText)
        {
            var selectSingleNode = pDoc.SelectSingleNode(pNodeIndex);
            if (selectSingleNode != null)
                selectSingleNode.InnerText = pNodeInnerText;
            
        }
    }
}