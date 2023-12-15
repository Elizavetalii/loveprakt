using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Будущий_10
{
    
    public interface IDataRepository<T>
    {
        void Create(T item);
        T Read(int id);
        void Update(T item);
        void Delete(int id);
        List<T> GetAll();
    }
    public class General
    {
        public static rowFromList Search<rowFromList>(List<rowFromList> table, string column, string searchtext)
        {
            rowFromList Object = default(rowFromList);
            foreach (rowFromList row in table)
            {
                //Возвращает объект Type для текущего экземпляра.Получает указанное свойство текущего класса Type.Если ключ существует, получает значение для ключа в указанном файле.
                string findText = row.GetType().GetProperty(column).GetValue(row, null) as string;
                if (findText != null && findText == searchtext)
                {
                    Object = row;
                }
            }
            return Object;
        }

        public static listFromJson MyDeserialize<listFromJson>(string filePath)
        {
            listFromJson listObjects = default(listFromJson);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                listObjects = JsonConvert.DeserializeObject<listFromJson>(json);

            }
            return listObjects;

        }
        public static void MySerialize<JsonFromlist>(JsonFromlist fromlist,string filePath)
        {         
            string json = JsonConvert.SerializeObject(fromlist);
            File.WriteAllText(filePath, json);
        }
    }
}
