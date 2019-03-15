using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using Newtonsoft.Json;

namespace HeyMe
{
    class AppSettings
    {
        /// <summary>
        /// Emails we can send to
        /// </summary>
        public string[] EmailChoices { get; set; }

        private string _fileName;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// ctor
        /// </summary>
        //--------------------------------------------------------------------------------------
        private AppSettings(string fileName)
        {
            _fileName = fileName;
            EmailChoices = new string[] { "eric@thejcrew.net" };
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// LoadData
        /// </summary>
        //--------------------------------------------------------------------------------------
        public static AppSettings Load(string settingsFileName)
        {
            var storage = IsolatedStorageFile.GetUserStoreForAssembly();

            if (!storage.FileExists(settingsFileName))
            {
                return new AppSettings(settingsFileName);
            }

            try
            {
                using (var isolatedFileStream = storage.OpenFile(settingsFileName, FileMode.Open, FileAccess.Read))
                {
                    var textReader = new StreamReader(isolatedFileStream);
                    return JsonConvert.DeserializeObject<AppSettings>(textReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("There was a loading error: " + e.Message);
                return new AppSettings(settingsFileName);
            }
        }


        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Save data to disk
        /// </summary>
        //--------------------------------------------------------------------------------------
        public void Save()
        {
            var storage = IsolatedStorageFile.GetUserStoreForAssembly();

            // Open the file using the established file stream.
            if (storage.FileExists(_fileName))
            {
                storage.DeleteFile(_fileName);
            }
            using (var isolatedFileStream = storage.OpenFile(_fileName, FileMode.Create, FileAccess.Write))
            {
                var textWriter = new StreamWriter(isolatedFileStream);
                textWriter.Write(JsonConvert.SerializeObject(this));
                textWriter.Flush();
                textWriter.Dispose();
            }
        }

    }
}
