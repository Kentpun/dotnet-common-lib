using HKSH.Common.ShareModel.Mdm;
using System.Text.Json;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// MdmExtension
    /// </summary>
    public static class MdmExtension
    {
        /// <summary>
        /// Deserializes the data json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="masters">The masters.</param>
        /// <returns></returns>
        public static List<T> DeserializeMdmDataJson<T>(this List<DictMasterResponse>? masters) where T : class
        {
            if (masters == null)
            {
                return new List<T>();
            }

            masters = masters.Where(s => s.IsActive).ToList();

            if (masters == null || !masters.Any())
            {
                return new List<T>();
            }

            List<T> datas = new();
            foreach (DictMasterResponse item in masters)
            {
                if (string.IsNullOrEmpty(item.DataJson))
                {
                    continue;
                }

                T? obj = JsonSerializer.Deserialize<T>(item.DataJson);
                if (obj == null)
                {
                    continue;
                }

                datas.Add(obj);
            }

            return datas;
        }
    }
}