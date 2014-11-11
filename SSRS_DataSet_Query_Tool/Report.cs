using System.Collections.Generic;
using System.IO;

namespace SSRS_DataSet_Query_Tool
{
    internal class Report
    {
        private string _selectedPath;
        private FileInfo _fileInfo;
        public Report(string selectedPath, FileInfo fileInfo)
        {
            _selectedPath = selectedPath;
            _fileInfo = fileInfo;
        }
        public string Folder { get { return _fileInfo.DirectoryName.Replace(_selectedPath, string.Empty); } }
        public string ReportName { get { return _fileInfo.Name; } }
        public string FullName { get { return _fileInfo.FullName; } }
        public string DirectoryName { get { return _fileInfo.DirectoryName; } }
        public List<ReportDataSet> ReportDataSet { get; set; }
    }
}
