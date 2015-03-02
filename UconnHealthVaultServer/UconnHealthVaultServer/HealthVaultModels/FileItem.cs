using System;
using System.IO;
using System.Xml;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using UconnHealthVaultServer.Helpers;

namespace UconnHealthVaultServer.HealthVaultModels
{
    [Serializable]
    public class FileItem : HealthItem
    {
        public string FileId;
        public string Name;
        public string Size;
        public string ContentType;
        public string FileData;
        public string FileUrl;

        public FileItem(String pXmlItem)
            : base(pXmlItem)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXmlItem);

            FileId = XmlParserHelper.GetNodeValue(aDoc, "thing/thing-id");
            Name = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/file/name");
            Size = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/file/size");
            ContentType = XmlParserHelper.GetNodeValue(aDoc, "thing/data-xml/file/content-type");
            FileUrl = XmlParserHelper.GetNodeValue(aDoc, "thing/blob-payload/blob/blob-ref-url");
        }

        internal Microsoft.Health.ItemTypes.File HydrateMicrosoftType(Microsoft.Health.ItemTypes.File pFileItem, HealthRecordAccessor pAccessor)
        {
            if (pFileItem == null)
            {
                pFileItem = new Microsoft.Health.ItemTypes.File();
            }

            pFileItem.Name = this.Name;
            pFileItem.Size = Convert.ToInt64(this.Size);
            pFileItem.SetContent(pAccessor, new MemoryStream(Convert.FromBase64String(FileData)), this.Name, new CodableValue(this.ContentType));
            return pFileItem;
        }
    }
}