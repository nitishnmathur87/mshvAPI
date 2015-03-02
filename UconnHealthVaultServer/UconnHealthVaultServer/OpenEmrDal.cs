//Author: Gordon Devoe - gordondevoe.com
//Date: 10/29/2013
//note: Moving forward I would try to using an ORM to map the mySQL objects. Since prescription is the only object relevant there is no need to 'over-engineer' this solution.

using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using UconnHealthVaultServer.OpenEmrModels;

namespace UconnHealthVaultServer
{
    public static class OpenEmrDal
    {

        public static List<Prescription> GetPrescriptions(string pPatientId)
        {

            var aPrescriptionList = new List<Prescription>();

            string aConnectionString = ConfigurationManager.ConnectionStrings["mySqlConnectionString"].ConnectionString;
            string aQuery = "SELECT * FROM openemr.prescriptions WHERE patient_id = '" + pPatientId + "'";

            try
            {
                using (var conn = new MySqlConnection(aConnectionString))
                {
                    conn.Open();
                    var aCommand = new MySqlCommand(aQuery, conn);

                    using (var dataReader = aCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var aPrescription = new Prescription
                            {
                                PatientId = dataReader["patient_id"].ToString(),
                                StartDate = dataReader["start_date"].ToString(),
                                DateAdded = dataReader["date_added"].ToString(),
                                Drug = dataReader["drug"].ToString(),
                                Dosage = dataReader["dosage"].ToString(),
                                Quantity = dataReader["quantity"].ToString(),
                                Size = dataReader["size"].ToString(),
                                Substitute = dataReader["substitute"].ToString(),
                                Refills = dataReader["refills"].ToString(),
                                PerRefill = dataReader["per_refill"].ToString(),
                                Note = dataReader["note"].ToString()
                            };

                            aPrescriptionList.Add(aPrescription);
                        }
                    }
                }
            }
            //TODO: this should be enhanced to catch and handle exceptions (return message to api caller).
            catch
            {
                return null;
            }

            return aPrescriptionList;
        }

        public static List<Vital> GetVitals(string pPatientId)
        {
            var aVitalList = new List<Vital>();

            string aConnectionString = ConfigurationManager.ConnectionStrings["mySqlConnectionString"].ConnectionString;
            string aQuery = "SELECT `pid`, `weight`, `date` FROM openemr.form_vitals WHERE pid = '" + pPatientId + "'";

            try
            {
                using (var conn = new MySqlConnection(aConnectionString))
                {
                    conn.Open();
                    var aCommand = new MySqlCommand(aQuery, conn);

                    using (var dataReader = aCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {

                            if (!dataReader.IsDBNull(0) && !dataReader.IsDBNull(1) && !dataReader.IsDBNull(2))
                            {
                                var aVital = new Vital
                                {
                                    PatientId = dataReader["pid"].ToString(),
                                    Date = dataReader["date"].ToString(),
                                    Weight = dataReader["weight"].ToString()

                                };

                                aVitalList.Add(aVital);
                            }
                        }
                    }
                }
            }
            //TODO: this should be enhanced to catch and handle exceptions (return message to api caller).
            catch(Exception e)
            {
                return null;
            }

            return aVitalList;
        }


        public static List<Vital> AddVital(Vital pVital)
        {
            string aConnectionString = ConfigurationManager.ConnectionStrings["mySqlConnectionString"].ConnectionString;
            string aVitalQuery = String.Format("INSERT INTO openemr.form_vitals(`pid`,`weight`,`date`,`activity`) VALUES({0},{1},'{2}',{3}); SELECT LAST_INSERT_ID();", Convert.ToInt64(pVital.PatientId), Convert.ToDecimal(pVital.Weight), pVital.Date, 1);
            

            try
            {
                using (var conn = new MySqlConnection(aConnectionString))
                {
                    conn.Open();
                    var aVitalCommand = new MySqlCommand(aVitalQuery, conn);
                    var vId = Convert.ToInt32(aVitalCommand.ExecuteScalar());
                    string aFormQuery = String.Format("INSERT INTO openemr.forms (`date`, `encounter`, `form_name`, `form_id`, `pid`, `user`, `groupname`, `authorized`, `deleted`, `formdir`) VALUES ({0}, '2', 'Vitals', {1}, {2}, 'admin', 'Default', '1', '0', 'vitals');", pVital.Date, vId, pVital.PatientId);
                    var aFormCommand = new MySqlCommand(aFormQuery, conn);
                    aFormCommand.ExecuteNonQuery();
                }
            }
            //TODO: this should be enhanced to catch and handle exceptions (return message to api caller).
            catch
            {
                return null;
            }

            return GetVitals(pVital.PatientId);

        }

    }
}