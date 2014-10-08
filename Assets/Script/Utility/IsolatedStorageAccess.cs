using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EZ_Iso
{
    public static class IsolatedStorageAccess
    {
        /// <summary>
        /// Pass in a unique name along with the object you want to save. Your object MUST have a [DataContractAttribute] and every veriable inside you want saved MUST have [DataMember]
        /// </summary>
        /// <param name="FileName">File Name - Must Be Unique. Do NOT include a file extention</param>
        /// <param name="obj">Dictionary Name Values To Save to Isolated Storage. Names MUST be unique</param>
        /// <returns>If there is an error it will return else the string will be String.Empty</returns>
        public static String SaveFile(String FileName, Object obj)
        {
            String err = String.Empty;
            if (!Attribute.IsDefined(obj.GetType(), typeof(DataContractAttribute)))
            {
                err = "Object must have DataContractAttribute";
            }
            if (FileName.Length < 1)
            {
                err = "File Name Must Not Be Empty";
            }
            if (IsolatedStorageFile.GetUserStoreForApplication().AvailableFreeSpace <= 0)
            {
                err = "Isolated Storage Out of Memory - Please free up space.";
            }
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(FileName))
            {
                err = "File Already Exists - Please choose a unique name.";
            }

            try
            {
                if (err == "")
                {
                    DataContractSerializer dsc = new DataContractSerializer(obj.GetType());


                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                    StreamWriter writer;

                    writer = new StreamWriter(new IsolatedStorageFileStream(FileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite, file));

                    dsc.WriteObject(writer.BaseStream, obj);

                    writer.Close();
                    file.Dispose();
                }
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }

            return err;

        }

        /// <summary>
        /// Allows you to overwrite existing saved files
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="obj"></param>
        /// <returns>If there is an error it will return else the string will be String.Empty</returns>
        public static String OverwriteFile(String FileName, Object obj)
        {
            String err = String.Empty;
            if (!Attribute.IsDefined(obj.GetType(), typeof(DataContractAttribute)))
            {
                err = "Object must have DataContractAttribute";
            }
            if (FileName.Length < 1)
            {
                err = "File Name Must Not Be Empty";
            }
            if (IsolatedStorageFile.GetUserStoreForApplication().AvailableFreeSpace <= 0)
            {
                err = "Isolated Storage Out of Memory - Please free up space.";
            }

            try
            {
                if (err == "")
                {
                    DataContractSerializer dsc = new DataContractSerializer(obj.GetType());


                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                    StreamWriter writer;

                    writer = new StreamWriter(new IsolatedStorageFileStream(FileName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite, file));

                    dsc.WriteObject(writer.BaseStream, obj);

                    file.Dispose();
                    writer.Close();
                }
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }
            return err;
        }

        /// <summary>
        /// Retrieve a file that you privously created from Isolated Storage
        /// </summary>
        /// <param name="FileName">The name of the file (EXACTLY how it was saved)</param>
        /// <returns>If there is an error it will return else the string will be String.Empty</returns>
        public static Object GetFile(String FileName, Type yourObjectsType)
        {
            string err = String.Empty;
            if (!IsolatedStorageFile.GetUserStoreForApplication().FileExists(FileName))
            {
                err = "File Doesn't Exist In Isoloated Storage";
            }

            Object ret = new Object();
            try
            {
                if (err == "")
                {
                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();

                    IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                    IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(@"\" + FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    DataContractSerializer dsc = new DataContractSerializer(yourObjectsType);
                    ret = dsc.ReadObject(fileStream);

                    file.Dispose();
                    myIsolatedStorage.Dispose();
                    fileStream.Close();
                }
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }

            return err == "" ? ret : err;
        }

        /// <summary>
        /// Delete A File From Isolated Storage
        /// </summary>
        /// <param name="FileName">Name of the file to be deleted</param>
        /// <returns>If there is an error it will return else the string will be String.Empty</returns>
        public static String DeleteFile(String FileName)
        {
            string err = "";
            if (FileName.Length < 1)
            {
                err = "File Name Must Not Be Empty";
            }
            if (!IsolatedStorageFile.GetUserStoreForApplication().FileExists(FileName))
            {
                err = "The File " + FileName + " Doesn't Exist";
            }
            try
            {
                if (err == "")
                {
                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                    file.DeleteFile(FileName);

                    file.Dispose();
                }
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }
            return err;
        }

        /// <summary>
        /// Create a new DIR
        /// </summary>
        /// <param name="DIRName">This is what the DIR will be named (Must be Unique)</param>
        /// <returns>If there is an error it will return else the string will be String.Empty</returns>
        public static String CreateDIR(String DIRName)
        {
            string err = String.Empty;
            if (DIRName.Length < 1)
            {
                err = "DIR Name Must Not Be Empty";
            }
            if (IsolatedStorageFile.GetUserStoreForApplication().DirectoryExists(DIRName))
            {
                err = "The DIR " + DIRName + " Already Exists";
            }

            try
            {
                if (err == "")
                    IsolatedStorageFile.GetUserStoreForApplication().CreateDirectory(DIRName);
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }
            return err;
        }
        /// <summary>
        /// Check to see if a DIR Exists
        /// </summary>
        /// <param name="DIRName"></param>
        /// <returns></returns>
        public static Boolean DIRExists(String DIRName)
        {
            return IsolatedStorageFile.GetUserStoreForApplication().DirectoryExists(DIRName);
        }

        /// <summary>
        /// Get a list of files in the directory
        /// </summary>
        /// <param name="dir">(OPTIONAL) Get files within this DIR</param>
        /// <returns>List of Files</returns>
        public static List<String> GetFiles(String dir = "")
        {
            try
            {
                if (dir.Length > 0)
                    return IsolatedStorageFile.GetUserStoreForApplication().GetFileNames("/" + dir + "/").ToList();
                else
                    return IsolatedStorageFile.GetUserStoreForApplication().GetFileNames().ToList();
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }
        }

        /// <summary>
        /// Get a list of DIRS in the root directory
        /// </summary>
        /// <returns>List of DIRS</returns>
        public static List<String> GetDIRS()
        {
            try
            {
                return IsolatedStorageFile.GetUserStoreForApplication().GetDirectoryNames().ToList();
            }
            catch (Exception error) { throw new System.ArgumentException(error.Message); }
        }

        /// <summary>
        /// Pass in a unique filename and a Bitmap image you want saved
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="bitmap"></param>
        public static void SaveImage(String FileName, BitmapImage bitmap, int orientation = 0, int quality = 100)
        {
            // Create virtual store and file stream. Check for duplicate tempJPEG files.
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(FileName);

                WriteableBitmap wb = new WriteableBitmap(bitmap);

                // Encode WriteableBitmap object to a JPEG stream.
                Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality);

                fileStream.Close();
            }
        }

        /// <summary>
        /// Retrieve a Bitmap Image. Orientation and Quality are optional parameters.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="orientation">Optional Param, default value 0</param>
        /// <param name="quality">Optional Param, default value 100</param>
        /// <returns></returns>
        public static BitmapImage GetImage(String FileName, int height, int width, int orientation = 0, int quality = 100)
        {
            BitmapImage bi = new BitmapImage();

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(FileName, FileMode.Open, FileAccess.Read))
                {
                    WriteableBitmap wb = new WriteableBitmap(width, height);

                    Extensions.LoadJpeg(wb, fileStream);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveJpeg(ms, width, height, orientation, quality);
                        bi.SetSource(ms);
                    }

                    fileStream.Close();
                    return bi;
                }
            }
        }

        /// <summary>
        /// Determine if a file exists or not
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Boolean FileExists(String FileName)
        {
            return (IsolatedStorageFile.GetUserStoreForApplication().FileExists(FileName));
        }
    }
}
