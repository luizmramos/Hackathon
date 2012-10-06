using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;

namespace Core {
    public class StorageNamedLines {
        Dictionary<String, String> myData = new Dictionary<String, String>();
        FileManager fileManager = new FileManager();

        String fileName = null;

        public StorageNamedLines(String fileName) {
            this.fileName = fileName;
            Init();
        }

        public void Set(String ParameterName, String ParameterValue) {
            if (myData.ContainsKey(ParameterName)) {
                myData[ParameterName] = ParameterValue;
            } else {
                myData.Add(ParameterName, ParameterValue);
            }
        }

        public void Save(String parameterName, String parameterValue) {
            Set(parameterName, parameterValue);
            Save();
        }

        public String Load(String ParameterName) {
            if (myData.ContainsKey(ParameterName)) {
                return myData[ParameterName];
            }
            throw new Exception();
        }

        public void SaveListString(List<String> list, String name) {
            String listString = "";
            foreach (String elem in list) {
                listString += elem + ";";
            }
            Save(name, listString);
        }

        public void SaveListInt(List<int> list, String name) {
            String listString = "";
            foreach (int elem in list) {
                listString += elem + ";";
            }
            Save(name, listString);
        }

        public void SaveDictionaryStringInt(Dictionary<String, int> dict, String name) {
            String dictString = "";
            foreach (String key in dict.Keys) {
                dictString += key + ":" + dict[key] + ";";
            }
            Save(name, dictString);
        }

        public void SaveDictionaryIntInt(Dictionary<int, int> dict, String name) {
            String dictString = "";
            foreach (int key in dict.Keys) {
                dictString += key + ":" + dict[key] + ";";
            }
            Save(name, dictString);
        }

        public List<int> LoadListInt(String name) {
            String data = Load(name);
            char[] delimiter = { ';' };
            String[] dataSplit = data.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            List<int> loadedList = new List<int>();
            foreach (String indexString in dataSplit) {
                loadedList.Add(int.Parse(indexString));
            }
            return loadedList;
        }

        public List<String> LoadListString(String name) {
            String data = Load(name);
            char[] delimiter = { ';' };
            String[] dataSplit = data.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            List<String> loadedList = new List<String>();
            foreach (String indexString in dataSplit) {
                loadedList.Add(indexString);
            }
            return loadedList;
        }

        public Dictionary<String, int> LoadDictionaryStringInt(String name) {
            String data = Load(name);
            char[] delimiter = { ';' };
            String[] dataSplit = data.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<String, int> dict = new Dictionary<String, int>();
            foreach (String elem in dataSplit) {
                char[] delimiter2 = { ':' };
                String[] dataSplit2 = elem.Split(delimiter2, StringSplitOptions.RemoveEmptyEntries);
                dict.Add(dataSplit2[0], int.Parse(dataSplit2[1]));
            }
            return dict;
        }

        public Dictionary<int, int> LoadDictionaryIntInt(String name) {
            String data = Load(name);
            char[] delimiter = { ';' };
            String[] dataSplit = data.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (String elem in dataSplit) {
                char[] delimiter2 = { ':' };
                String[] dataSplit2 = elem.Split(delimiter2, StringSplitOptions.RemoveEmptyEntries);
                dict.Add(int.Parse(dataSplit2[0]), int.Parse(dataSplit2[1]));
            }
            return dict;
        }

        public void Save() {
            if (myData.Count == 0) return;

            String[] lines = new String[myData.Count];
            int i = 0;
            foreach (String key in myData.Keys) {
                StringBuilder data = new StringBuilder();
                data.Append(key);
                data.Append('=');
                data.Append(myData[key]);
                lines[i] = data.ToString();
                i++;
            }
            fileManager.SaveLines(fileName, lines);
        }

        private void Init() {
            try {
                String[] lines = fileManager.LoadLines(fileName);
                myData.Clear();
                char[] sep = new char[1]; sep[0] = '=';
                foreach (String line in lines) {
                    String[] data = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    myData.Add(data[0], data[1]);
                }
            } catch { }
        }
    }
}
