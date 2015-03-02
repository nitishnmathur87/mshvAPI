using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Health;
using UconnHealthVaultServer.Enums;
using UconnHealthVaultServer.HealthVaultModels;

namespace UconnHealthVaultServer
{
    public class HealthController : ApiController
    {
        // Get HealthItem
        public List<string> Get(string PublicId, string RecordId, string TypeId)
        {
            return HealthVaultDal.GetValues<HealthRecordItem>(PublicId, RecordId, TypeId);
        }

        // Delete HealthItem
        public void Delete(string PublicId, string RecordId, string Key)
        {
            HealthVaultDal.DeleteHealthItem(PublicId, RecordId, Key);
        }

        // Add HealthItem
        public void Add(string PublicId, string RecordId, string TypeId, [FromBody]string XmlString)
        {
            HealthVaultDal.AddHealthItem(PublicId, RecordId, TypeId, XmlString);
        }

        // Get Person
        public Person GetPerson(string PublicId, string RecordId)
        {
            var aListOfPeople = Get(PublicId, RecordId, HealthTypeEnum.Person);
            
            var aPerson = new Person(aListOfPeople[0]);

            var aListOfHeights = GetHeights(PublicId, RecordId);

            aPerson.Heights = aListOfHeights;

            var aListOfWeights = GetWeights(PublicId, RecordId);

            aPerson.Weights = aListOfWeights;

            var aDemographic = GetDemographic(PublicId, RecordId);

            aPerson.Demograph = aDemographic;

            return aPerson;
        }

        // Get Demographic
        private Demographic GetDemographic(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Demographic);
            return new Demographic(listOfItems[0]);
        }

        // Get Heights
        private List<Height> GetHeights(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Height);
            return listOfItems.Select(item => new Height(item)).ToList();
        }

        // Add Height
        public List<Height> AddHeight(string PublicId, string RecordId, [FromBody]Height pHeight)
        {
            HealthVaultDal.AddHeight(PublicId, RecordId, pHeight);
            return GetHeights(PublicId, RecordId);
        }

        // Update Height
        public List<Height> UpdateHeight(string PublicId, string RecordId, [FromBody]Height pHeight)
        {
            HealthVaultDal.UpdateHeight(PublicId, RecordId, pHeight);
            return GetHeights(PublicId, RecordId);
        }

        // Get Weights
        private List<Weight> GetWeights(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Weight);
            return listOfItems.Select(item => new Weight(item)).ToList();
        }

        // Add Weight
        public List<Weight> AddWeight(string PublicId, string RecordId, [FromBody]Weight pWeight)
        {
            HealthVaultDal.AddWeight(PublicId, RecordId, pWeight);
            return GetWeights(PublicId, RecordId);
        }

        // Update Weight
        public List<Weight> UpdateWeight(string PublicId, string RecordId, [FromBody]Weight pWeight)
        {
            HealthVaultDal.UpdateWeight(PublicId, RecordId, pWeight);
            return GetWeights(PublicId, RecordId);
        }

        // Get Medications
        public List<Medication> GetMedications(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Medication);
            return listOfItems.Select(item => new Medication(item)).ToList();
        }

        // Add Medication
        public List<Medication> AddMedication(string PublicId, string RecordId, [FromBody]Medication pMedication)
        {
            HealthVaultDal.AddMedication(PublicId, RecordId, pMedication);
            return GetMedications(PublicId, RecordId);
        }

        //Update Medications
        public List<Medication> UpdateMedication(string PublicId, string RecordId, [FromBody]Medication pMedication)
        {
            HealthVaultDal.UpdateMedication(PublicId, RecordId, pMedication);
            return GetMedications(PublicId, RecordId);
        }

        // Get Emotions
        public List<Emotion> GetEmotions(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Emotion);
            return listOfItems.Select(item => new Emotion(item)).ToList();
        }

        // Add Emotion
        public List<Emotion> AddEmotion(string PublicId, string RecordId, [FromBody]Emotion pEmotion)
        {
            HealthVaultDal.AddEmotion(PublicId, RecordId, pEmotion);
            return GetEmotions(PublicId, RecordId);
        }

        // Update Emotion
        public List<Emotion> UpdateEmotion(string PublicId, string RecordId, [FromBody]Emotion pEmotion)
        {
            HealthVaultDal.UpdateEmotion(PublicId, RecordId, pEmotion);
            return GetEmotions(PublicId, RecordId);
        }

        // Get Vitals
        public List<Vital> GetVitals(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Vital);
            return listOfItems.Select(item => new Vital(item)).ToList();
        }

        // Add Vital
        public List<Vital> AddVital(string PublicId, string RecordId, [FromBody]Vital pVital)
        {
            HealthVaultDal.AddVital(PublicId, RecordId, pVital);
            return GetVitals(PublicId, RecordId);
        }

        // Update Vital
        public List<Vital> UpdateVital(string PublicId, string RecordId, [FromBody]Vital pVital)
        {
            HealthVaultDal.UpdateVital(PublicId, RecordId, pVital);
            return GetVitals(PublicId, RecordId);
        }

        // Get Exercises
        public List<Exercise> GetExercises(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Exercise);
            return listOfItems.Select(item => new Exercise(item)).ToList();
        }

        // Add Exercise
        public List<Exercise> AddExercise(string PublicId, string RecordId, [FromBody]Exercise pExercise)
        {
            HealthVaultDal.AddExercise(PublicId, RecordId, pExercise);
            return GetExercises(PublicId, RecordId);
        }

        // Update Exercise
        public List<Exercise> UpdateExercise(string PublicId, string RecordId, [FromBody]Exercise pExercise)
        {
            HealthVaultDal.UpdateExercise(PublicId, RecordId, pExercise);
            return GetExercises(PublicId, RecordId);
        }

        // Get Conditions
        public List<Condition> GetConditions(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Condition);
            return listOfItems.Select(item => new Condition(item)).ToList();
        }

        // Add Condition
        public List<Condition> AddCondition(string PublicId, string RecordId, [FromBody]Condition pCondition)
        {
            HealthVaultDal.AddCondition(PublicId, RecordId, pCondition);
            return GetConditions(PublicId, RecordId);
        }

        // Update Condition
        public List<Condition> UpdateCondition(string PublicId, string RecordId, [FromBody]Condition pCondition)
        {
            HealthVaultDal.UpdateCondition(PublicId, RecordId, pCondition);
            return GetConditions(PublicId, RecordId);
        }

        // Get BloodPressures
        public List<BloodPressure> GetBloodPressures(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.BloodPressure);
            return listOfItems.Select(item => new BloodPressure(item)).ToList();
        }

        // Add BloodPressure
        public List<BloodPressure> AddBloodPressure(string PublicId, string RecordId, [FromBody]BloodPressure pBloodPressure)
        {
            HealthVaultDal.AddBloodPressure(PublicId, RecordId, pBloodPressure);
            return GetBloodPressures(PublicId, RecordId);
        }

        // Update BloodPressure
        public List<BloodPressure> UpdateBloodPressure(string PublicId, string RecordId, [FromBody]BloodPressure pBloodPressure)
        {
            HealthVaultDal.UpdateBloodPressure(PublicId, RecordId, pBloodPressure);
            return GetBloodPressures(PublicId, RecordId);
        }

        // Get DiabeticProfiles
        public List<DiabeticProfile> GetDiabeticProfiles(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.DiabeticProfile);
            return listOfItems.Select(item => new DiabeticProfile(item)).ToList();
        }

        // Add DiabeticProfile
        public List<DiabeticProfile> AddDiabeticProfile(string PublicId, string RecordId, [FromBody]DiabeticProfile pDiabeticProfile)
        {
            HealthVaultDal.AddDiabeticProfile(PublicId, RecordId, pDiabeticProfile);
            return GetDiabeticProfiles(PublicId, RecordId);
        }

        // Update DiabeticProfile
        public List<DiabeticProfile> UpdateDiabeticProfile(string PublicId, string RecordId, [FromBody]DiabeticProfile pDiabeticProfile)
        {
            HealthVaultDal.UpdateDiabeticProfile(PublicId, RecordId, pDiabeticProfile);
            return GetDiabeticProfiles(PublicId, RecordId);
        }

        // Get Allergies
        public List<Allergy> GetAllergies(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.Allergy);
            return listOfItems.Select(item => new Allergy(item)).ToList();
        }

        // Add Allergy
        public List<Allergy> AddAllergy(string PublicId, string RecordId, [FromBody]Allergy pAllergy)
        {
            HealthVaultDal.AddAllergy(PublicId, RecordId, pAllergy);
            return GetAllergies(PublicId, RecordId);
        }

        // Update Allergy
        public List<Allergy> UpdateAllergy(string PublicId, string RecordId, [FromBody]Allergy pAllergy)
        {
            HealthVaultDal.UpdateAllergy(PublicId, RecordId, pAllergy);
            return GetAllergies(PublicId, RecordId);
        }

        // Get PeakFlows
        public List<PeakFlow> GetPeakFlows(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.PeakFlow);
            return listOfItems.Select(item => new PeakFlow(item)).ToList();
        }

        // Add PeakFlow
        public List<PeakFlow> AddPeakFlow(string PublicId, string RecordId, [FromBody]PeakFlow pPeakFlow)
        {
            HealthVaultDal.AddPeakFlow(PublicId, RecordId, pPeakFlow);
            return GetPeakFlows(PublicId, RecordId);
        }

        // Update PeakFlow
        public List<PeakFlow> UpdatePeakFlow(string PublicId, string RecordId, [FromBody]PeakFlow pPeakFlow)
        {
            HealthVaultDal.UpdatePeakFlow(PublicId, RecordId, pPeakFlow);
            return GetPeakFlows(PublicId, RecordId);
        }

        // Get File Items
        public List<FileItem> GetFileItems(string PublicId, string RecordId)
        {
            var listOfItems = Get(PublicId, RecordId, HealthTypeEnum.FileItem);
            return listOfItems.Select(item => new FileItem(item)).ToList();
        }

        // Add FileItem
        public List<FileItem> AddFileItem(string PublicId, string RecordId, [FromBody]FileItem pFileItem)
        {
            HealthVaultDal.AddFileItem(PublicId, RecordId, pFileItem);
            return GetFileItems(PublicId, RecordId);
        }

        // Update FileItem
        public List<FileItem> UpdateFileItem(string PublicId, string RecordId, [FromBody]FileItem pFileItem)
        {
            HealthVaultDal.UpdateFileItem(PublicId, RecordId, pFileItem);
            return GetFileItems(PublicId, RecordId);
        }
    }
}