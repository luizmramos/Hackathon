using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace Core {
    public class FileManager {
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        public String[] LoadLines(String fileName) {
            String[] lines = null;
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                if (savegameStorage.FileExists(fileName)) {
                    using (IsolatedStorageFileStream fsRead = savegameStorage.OpenFile(fileName, System.IO.FileMode.Open)) {
                        if (fsRead != null) {
                            byte[] saveBytes = new byte[fsRead.Length];
                            int count = fsRead.Read(saveBytes, 0, (int)fsRead.Length);
                            String a = encoding.GetString(saveBytes, 0, saveBytes.Length);
                            if (count > 0)
                                lines = encoding.GetString(saveBytes, 0, saveBytes.Length).Split('%');
                            fsRead.Close();
                        }
                    }
                }
                if (lines == null) throw new Exception();
                return lines;
            }
        }

        public void SaveLines(String fileName, params String[] lines) {
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                IsolatedStorageFileStream fs = null;
                using (fs = savegameStorage.CreateFile(fileName)) {
                    if (fs != null) {
                        StringBuilder dataString = new StringBuilder();
                        foreach (String line in lines) {
                            dataString.Append(line);
                            dataString.Append("%");
                        }
                        Byte[] bytes = encoding.GetBytes(dataString.ToString());
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }
                }
            }
        }

    }

}
