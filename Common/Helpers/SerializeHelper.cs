using System;
using System.IO;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Common.Helpers
{
    /// <summary>
    /// Class SerializeHelper.
    /// </summary>
    public static class SerializeHelper
    {
        /// <summary>
        /// Serializes the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <param name="filePath">The file path.</param>
        public static void XmlSerialize<T>(T o, string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    formatter.Serialize(sw, o);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to Serialize to path.", ex);
            }
        }

        /// <summary>
        /// Des the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// T.
        /// </returns>
        public static T XmlDeserialize<T>(string filePath)
        {
            try
            {
                T obj;
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (Stream reader = new FileStream(filePath, FileMode.Open))
                {
                    obj = (T)serializer.Deserialize(reader);
                }

                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to deserialize in path.", ex);
            }

            return default(T);
        }

        /// <summary>
        /// Jsons the serialize.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string JsonSerialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Jsons the deserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
