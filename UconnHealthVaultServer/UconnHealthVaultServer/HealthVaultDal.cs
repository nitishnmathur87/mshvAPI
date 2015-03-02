//Author: Gordon Devoe - gordondevoe.com
//Date: 10/10/2013

using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Health;
using UconnHealthVaultServer.HealthVaultModels;
using Allergy = UconnHealthVaultServer.HealthVaultModels.Allergy;
using BloodPressure = UconnHealthVaultServer.HealthVaultModels.BloodPressure;
using Condition = UconnHealthVaultServer.HealthVaultModels.Condition;
using DiabeticProfile = UconnHealthVaultServer.HealthVaultModels.DiabeticProfile;
using Emotion = UconnHealthVaultServer.HealthVaultModels.Emotion;
using Exercise = UconnHealthVaultServer.HealthVaultModels.Exercise;
using Height = UconnHealthVaultServer.HealthVaultModels.Height;
using Medication = UconnHealthVaultServer.HealthVaultModels.Medication;
using PeakFlow = UconnHealthVaultServer.HealthVaultModels.PeakFlow;
using Weight = UconnHealthVaultServer.HealthVaultModels.Weight;

namespace UconnHealthVaultServer
{
    public static class HealthVaultDal
    {
        public static String GetSingleValue<T>(string pPublicId, string pRecordId, string pTypeId) where T : HealthRecordItem
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var searcher = accessor.CreateSearcher();
            var filter = new HealthRecordFilter(new Guid(pTypeId));
            searcher.Filters.Add(filter);
            var items = searcher.GetMatchingItems()[0];

            if (items != null && items.Count > 0)
            {
                var item = items[0] as T;
                if(item != null)
                return item.Serialize();
            }
            return null;
        }

        public static List<string> GetValues<T>(string pPublicId, string pRecordId, string pTypeId) where T : HealthRecordItem
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var searcher = accessor.CreateSearcher();
            var filter = new HealthRecordFilter(new Guid(pTypeId));
            filter.View.Sections = HealthRecordItemSections.All;
            searcher.Filters.Add(filter);
            var items = searcher.GetMatchingItems()[0];
            var listOfItems = new List<string>();
            foreach (var item in items)
            {
                listOfItems.Add(item.Serialize());
            }
            return listOfItems;
        }

        public static void DeleteHealthItem(string pPublicId, string pRecordId, string pKey)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pKey));
            accessor.RemoveItem(hri);
        }

        public static void AddHealthItem(string pPublicId, string pRecordId, string pTypeId, string pXml)
        {
            var aDoc = new XmlDocument();
            aDoc.LoadXml(pXml);
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = new HealthRecordItem(new Guid(pTypeId), aDoc);
            accessor.NewItem(hri);
        }

        public static void AddMedication(string pPublicId, string pRecordId, Medication pMedication)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pMedication.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateMedication(string pPublicId, string pRecordId, Medication pMedication)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pMedication.Key));
            pMedication.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Medication)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddHeight(string pPublicId, string pRecordId, Height pHeight)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pHeight.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateHeight(string pPublicId, string pRecordId, Height pHeight)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pHeight.Key));
            pHeight.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Height)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddWeight(string pPublicId, string pRecordId, Weight pWeight)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pWeight.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateWeight(string pPublicId, string pRecordId, Weight pWeight)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pWeight.Key));
            pWeight.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Weight)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddEmotion(string pPublicId, string pRecordId, Emotion pEmotion)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pEmotion.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateEmotion(string pPublicId, string pRecordId, Emotion pEmotion)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pEmotion.Key));
            pEmotion.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Emotion)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddVital(string pPublicId, string pRecordId, Vital pVital)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pVital.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateVital(string pPublicId, string pRecordId, Vital pVital)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pVital.Key));
            pVital.HydrateMicrosoftType((Microsoft.Health.ItemTypes.VitalSigns)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddExercise(string pPublicId, string pRecordId, Exercise pExercise)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pExercise.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateExercise(string pPublicId, string pRecordId, Exercise pExercise)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pExercise.Key));
            pExercise.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Exercise)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddCondition(string pPublicId, string pRecordId, Condition pCondition)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pCondition.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateCondition(string pPublicId, string pRecordId, Condition pCondition)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pCondition.Key));
            pCondition.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Condition)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddBloodPressure(string pPublicId, string pRecordId, BloodPressure pBloodPressure)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pBloodPressure.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateBloodPressure(string pPublicId, string pRecordId, BloodPressure pBloodPressure)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pBloodPressure.Key));
            pBloodPressure.HydrateMicrosoftType((Microsoft.Health.ItemTypes.BloodPressure)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddDiabeticProfile(string pPublicId, string pRecordId, DiabeticProfile pDiabeticProfile)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pDiabeticProfile.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateDiabeticProfile(string pPublicId, string pRecordId, DiabeticProfile pDiabeticProfile)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pDiabeticProfile.Key));
            pDiabeticProfile.HydrateMicrosoftType((Microsoft.Health.ItemTypes.DiabeticProfile)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddAllergy(string pPublicId, string pRecordId, Allergy pAllergy)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pAllergy.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdateAllergy(string pPublicId, string pRecordId, Allergy pAllergy)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pAllergy.Key));
            pAllergy.HydrateMicrosoftType((Microsoft.Health.ItemTypes.Allergy)hri);
            accessor.UpdateItem(hri);
        }

        public static void AddPeakFlow(string pPublicId, string pRecordId, PeakFlow pPeakFlow)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pPeakFlow.HydrateMicrosoftType(null);
            accessor.NewItem(hri);
        }

        public static void UpdatePeakFlow(string pPublicId, string pRecordId, PeakFlow pPeakFlow)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pPeakFlow.Key));
            pPeakFlow.HydrateMicrosoftType((Microsoft.Health.ItemTypes.PeakFlow) hri);
            accessor.UpdateItem(hri);
        }

        public static void AddFileItem(string pPublicId, string pRecordId, FileItem pFileItem)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = pFileItem.HydrateMicrosoftType(null, accessor);
            accessor.NewItem(hri);
        }

        public static void UpdateFileItem(string pPublicId, string pRecordId, FileItem pFileItem)
        {
            var accessor = HealthAuth.Authenticate(pPublicId, pRecordId);
            var hri = accessor.GetItem(new Guid(pFileItem.Key));
            pFileItem.HydrateMicrosoftType((Microsoft.Health.ItemTypes.File)hri, accessor);
            accessor.UpdateItem(hri);
        }
    }
}