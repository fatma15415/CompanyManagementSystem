using Microsoft.AspNetCore.Http;
using System;
using System.IO;
namespace Demo.PL.Helper
{
    public class DocumentSetting
    {
        public static string  Uploadfile(IFormFile file, string foldername)
        {
            //1.Get located file Path
            // string folderpath= "G:\\ROUTE\\MVC\\S 03\\MVCDemo03Sol\\Demo.PL\\wwwroot\\Files\\" + foldername;
            //  string folderpath = Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\" + foldername;
            string folderpath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + foldername);

            //2.Get file Name and make it unique
            string filename=$"{Guid.NewGuid()}{file.FileName}" ;


            //3.Get File Path
             string FilePath = Path.Combine(folderpath, filename);

            //4.how to save file as streams [Data per time]
            using var filestream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(filestream);

            return filename;

        }

        public static void DeleteFile(string filename , string foldername)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\Files\\", foldername, filename);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);  
            }
        }
    }
}
