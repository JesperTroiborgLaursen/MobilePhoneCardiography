using System;
using System.IO;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOs;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace MobilePhoneCardiography.Services.DataStore
{
    public class ControllerDatabase : IControllerDatabase
    {
        private ICosmosDBService cosmosDbService;
        public ControllerDatabase(ICosmosDBService cosmosDb)
        {
            this.cosmosDbService = cosmosDb;
        }

        public async void StoreHeartSound(Measurement measurement)
        {
            await cosmosDbService.StoreHeartSound(ConvertToJson(measurement));
        }

        #region  ConvertByteArrayToJson

            private JsonMeasurement ConvertToJson(Measurement item)
            {
                JsonMeasurement measurement = new JsonMeasurement()
                {
                    MeasurementId = item.Id.ToString(),
                    HealthProfId = item.HealthProfID,
                    HeartSound = Deserialize(item.HeartSound),
                    PatientId = item.PatientID,
                    PlacementOfDevice = (Int16)item.PlacementEnum,
                    ProbabilityPercentage = item.ProbabilityProcent,
                    StartTime = item.StartTime

                };
                return measurement;
            }
            private static readonly JsonSerializer _serializer = new JsonSerializer();
            private static string Deserialize(Stream s)
            {
                byte[] soundBytes = ReadToEnd(s);

                return JsonConvert.SerializeObject(soundBytes, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            }
            private static byte[] ReadToEnd(System.IO.Stream stream)
            {
                long originalPosition = 0;

                if (stream.CanSeek)
                {
                    originalPosition = stream.Position;
                    stream.Position = 0;
                }

                try
                {
                    byte[] readBuffer = new byte[4096];

                    int totalBytesRead = 0;
                    int bytesRead;

                    while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                    {
                        totalBytesRead += bytesRead;

                        if (totalBytesRead == readBuffer.Length)
                        {
                            int nextByte = stream.ReadByte();
                            if (nextByte != -1)
                            {
                                byte[] temp = new byte[readBuffer.Length * 2];
                                Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                                Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                                readBuffer = temp;
                                totalBytesRead++;
                            }
                        }
                    }

                    byte[] buffer = readBuffer;
                    if (readBuffer.Length != totalBytesRead)
                    {
                        buffer = new byte[totalBytesRead];
                        Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                    }
                    return buffer;
                }
                finally
                {
                    if (stream.CanSeek)
                    {
                        stream.Position = originalPosition;
                    }
                }
            }

            #endregion
        public async Task<bool> ValidateLogin(IUser user)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                 user.Password = new SecurityCyptography().GetHash(sha256, user.Password);
            }

            var todos = await cosmosDbService.GetLogin(user);
            //TODO Denne linje er under test
            if (todos != null && todos[0].UserPW == user.Password && todos[0].HealthProfID == user.Username) return true;

            //TODO Denne linje er væk fra test
            //foreach (var VARIABLE in todos)
            //{
            //    if (todos != null && VARIABLE.UserPW == user.Password && VARIABLE.HealthProfID == user.Username) return true;

            //}

            return false;
        }

        public async Task<bool> ValidatePatient(IPatient patient)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                patient.SocSec = new SecurityCyptography().GetHash(sha256, patient.SocSec);
            }
            var todos = await cosmosDbService.GetSSN(patient);
            if (todos != null && todos[0].PatientId == patient.SocSec)
            {
                patient.FirstName = todos[0].FirstName;
                patient.LastName = todos[0].LastName;
                return true;
            }

            //foreach (var VARIABLE in todos)
            //{
            //    if (VARIABLE != null && VARIABLE.PatientId == patient.SocSec)
            //    {
            //        patient.FirstName = VARIABLE.FirstName;
            //        patient.LastName = VARIABLE.LastName;
            //        return true;
            //    }


            //}
            return false;
        }

    }
}