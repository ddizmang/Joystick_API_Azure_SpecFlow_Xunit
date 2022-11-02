using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Domain.Enums;
using AventStack.ExtentReports.Utils;

namespace Automation.Domain.Operations
{
    public class IOOperations
    {
        public static async Task<string> WaitForFile(string filePath, string filePrefix, TimeSpan timeout)
        {
            DateTimeOffset timeOutAt = DateTimeOffset.UtcNow + timeout;
            var path = $"{filePath}\\{filePrefix}*.hl7";
            var hl7file = string.Empty;
            while (true)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        string[] hl7files = Directory.GetFiles(filePath, $"{filePrefix}*.hl7");
                        try
                        {
                            var sort = from file in hl7files
                                orderby new FileInfo(file).CreationTime ascending
                                select file;
                            hl7file = sort.First();
                            return hl7file;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"IOOperations.WaitForFile Exception {e.Message} : {e.Message}");
                        }
                    }
                    if (DateTimeOffset.UtcNow >= timeOutAt) throw new TimeoutException("WaitForFile timed out waiting for file");
                        await Task.Delay(10);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        //TODO: Marked for Removal
        public static string GetFirstHL7FileByCreateDate(string pathToSearch, string searchCriteria = null, string fileToSkip = null)
        {
            string[] fns;
            string first = "";
            string fileCriteria = string.Empty;
            try
            {
                if (pathToSearch.IsNullOrEmpty())
                {
                    throw new DirectoryNotFoundException("The path is null or empty");
                }

                if (!searchCriteria.IsNullOrEmpty())
                {
                    fileCriteria = $"{searchCriteria}*.hl7";
                }
                else
                {
                    fileCriteria = "*.hl7";
                }
                var fileList = System.IO.Directory.GetFiles(pathToSearch, fileCriteria);
                var sort = from file in fileList
                           where (fileToSkip == null || file != fileToSkip)
                           orderby new FileInfo(file).CreationTime ascending
                           select file;
                first = sort.First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return first;
        }

        public static string GetFirstXmlFileByCreateDate(string pathToSearch, string searchCriteria = null, string fileToSkip = null)
        {
            //string[] fns;
            string first = "";
            string fileCriteria = string.Empty;
            try
            {
                if (pathToSearch.IsNullOrEmpty())
                {
                    throw new DirectoryNotFoundException("The path is null or empty");
                }

                if (!searchCriteria.IsNullOrEmpty())
                {
                    fileCriteria = $"{searchCriteria}*.xml";
                }
                else
                {
                    fileCriteria = "*.xml";
                }
                var fileList = System.IO.Directory.GetFiles(pathToSearch, fileCriteria);
                var sort = from file in fileList
                    where (fileToSkip == null || file != fileToSkip)
                    orderby new FileInfo(file).CreationTime ascending
                    select file;
                first = sort.First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return first;
        }

        public static string GetFirstFileByFileExtAndCreateDate(string pathToSearch, FileTypes.FileType fileType,
            string searchCriteria = null, string fileToSkip = null)
        {
            string first = "";
            string fileCriteria = string.Empty;
            try
            {
                if (pathToSearch.IsNullOrEmpty())
                {
                    throw new DirectoryNotFoundException("The path is null or empty");
                }

                switch (fileType)
                {
                    case FileTypes.FileType.XML:
                        fileCriteria = !searchCriteria.IsNullOrEmpty() ? $"{searchCriteria}*.xml" : "*.xml";
                        break;
                    case FileTypes.FileType.HL7:
                        fileCriteria = !searchCriteria.IsNullOrEmpty() ? $"{searchCriteria}*.hl7" : "*.hl7";
                        break;
                    case FileTypes.FileType.CSV:
                        fileCriteria = !searchCriteria.IsNullOrEmpty() ? $"{searchCriteria}*.csv" : "*.csv";
                        break;
                }

                var fileList = System.IO.Directory.GetFiles(pathToSearch, fileCriteria);
                var sort = from file in fileList
                    where (fileToSkip == null || file != fileToSkip)
                    orderby new FileInfo(file).CreationTime ascending
                    select file;
                first = sort.First();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return first;
        }
    }
}
