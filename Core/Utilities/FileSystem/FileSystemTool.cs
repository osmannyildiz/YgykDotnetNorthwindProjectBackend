using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileSystem {
    public class FileSystemTool {
        public static void SaveFormFile(IFormFile file, string filePath) {
            using (FileStream fs = File.Create(filePath)) {
                file.CopyTo(fs);
            }
        }

        public static void DeleteFileIfExists(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}
