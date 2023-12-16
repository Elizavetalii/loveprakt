using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static Будущий_10.Administrator;

namespace Будущий_10
{
    
    public interface ICRUD
    {
        void Create();
        void Read(int index);
        void Update();
        void Delete();
        
    }
    public class General
    {

        public static rowFromList Search<rowFromList>(List<rowFromList> table, string column, string searchtext)
        {
            rowFromList Object = default(rowFromList);
            
            foreach (rowFromList row in table)
            {
                //Возвращает объект Type для текущего экземпляра.Получает указанное свойство текущего класса Type.Если ключ существует, получает значение для ключа в указанном файле.
                string findText = row.GetType().GetProperty(column).GetValue(row, null).ToString();
                if (findText != null && findText == searchtext)
                {
                    Object = row;
                }
            }
            return Object;
        }

        public static List<rowFromList> SearchList<rowFromList>(List<rowFromList> table, string column, string searchtext)
        {
            List<rowFromList> listObjects = new();
            for (int i = 0; i< table.Count; i++)
            {
                var row = table[i];
                //Возвращает объект Type для текущего экземпляра.Получает указанное свойство текущего класса Type.Если ключ существует, получает значение для ключа в указанном файле.
                string findText = row.GetType().GetProperty(column).GetValue(row, null).ToString();
                if (findText != null && findText == searchtext)
                {
                    listObjects.Add(row);
                }
            }
            return listObjects;
        }

        public static List<listFromJson> MyDeserialize<listFromJson>(string filePath)
        {
            List<listFromJson> listObjects = new();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                listObjects = JsonConvert.DeserializeObject<List<listFromJson>>(json);
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
