using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSRS_DataSet_Query_Tool
{
    public partial class frmSSRSDataSetQueryTool : Form
    {
        internal List<Report> _reports = null;
        internal string _selectedPath = string.Empty;

        public frmSSRSDataSetQueryTool()
        {
            InitializeComponent();
            InitializeGrid();

        }

        private const string _subfolderPath = "Subfolder Path";
        private const string _reportName = "Report Name";
        private const string _datasetName = "DataSet Name";
        private const string _query = "Query";

        private void InitializeGrid()
        {
            dgvResults.ColumnCount = 4;
            dgvResults.Columns[0].Name = _subfolderPath;
            dgvResults.Columns[1].Name = _reportName;
            dgvResults.Columns[2].Name = _datasetName;
            dgvResults.Columns[3].Name = _query;
            dgvResults.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvResults.RowHeadersVisible = false;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
        }

        private void btnGetFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdReportsFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFolders.Text = fbdReportsFolder.SelectedPath;
                _selectedPath = fbdReportsFolder.SelectedPath;
                PopulateGrid(_selectedPath);
                btnExport.Enabled = true;
            }
        }

        private void PopulateGrid(string selectedPath)
        {
            _reports = new List<Report>();

            dgvResults.Rows.Clear();

            _reports = GetReportsFromDirectories(selectedPath);

            foreach (Report report in _reports)
            {
                report.ReportDataSet = GetReportDataSets(report);
                AddReportToGrid(report);                
            }
        }

        private List<Report> GetReportsFromDirectories(string selectedPath)
        {
            List<Report> reports = new List<Report>();

            DirectoryInfo directoryInfo = new DirectoryInfo(selectedPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.rdl", SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileInfos)
            {
                reports.Add(new Report(selectedPath, fileInfo));
            }

            return reports;
        }

        private List<ReportDataSet> GetReportDataSets(Report report)
        {
            List<ReportDataSet> reportDataSets = new List<ReportDataSet>();
            ReportDataSet reportDataSet = null;

            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader sr = new StreamReader(report.FullName))
            {
                xmlDocument.Load(sr);
            }
            XmlNode root = xmlDocument.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes("descendant::*");
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "DataSets")
                {
                    XmlNodeList dataSetsChildList = node.ChildNodes;

                    foreach (XmlNode dataSetsChildNode in dataSetsChildList)
                    {
                        reportDataSet = new ReportDataSet();

                        if (dataSetsChildNode.Name == "DataSet")
                        {
                            reportDataSet.DataSetName = dataSetsChildNode.Attributes["Name"].Value;

                            XmlNodeList dataSetChildList = dataSetsChildNode.ChildNodes;

                            foreach (XmlNode dataSetChildNode in dataSetChildList)
                            {
                                if (dataSetChildNode.Name == "Query")
                                {
                                    XmlNodeList queryChildList = dataSetChildNode.ChildNodes;

                                    foreach (XmlNode queryChildNode in queryChildList)
                                    {
                                        if (queryChildNode.Name == "CommandText")
                                        {
                                            reportDataSet.Query = queryChildNode.InnerText;
                                        }
                                    }
                                }
                            }
                        }

                        reportDataSets.Add(reportDataSet);
                    }
                }
            }

            return reportDataSets;
        }

        private void AddReportToGrid(Report report)
        {
            DataGridViewRow row = null;
            if (report.ReportDataSet.Count > 0)
            {
                foreach (ReportDataSet reportDataSet in report.ReportDataSet)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(dgvResults, report.Folder, report.ReportName, reportDataSet.DataSetName, reportDataSet.Query);
                    dgvResults.Rows.Add(row);
                }
            }
            else
            {
                row = new DataGridViewRow();
                row.CreateCells(dgvResults, report.Folder, report.ReportName);
                dgvResults.Rows.Add(row);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.InitialDirectory = _selectedPath;
            sfd.FileName = "SSRS_DataSet_Query_Tool.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Excel._Application app = new Excel.Application();
                Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Excel._Worksheet worksheet = null;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "SSRS_DataSet_Query_Tool";

                for (int i = 1; i < dgvResults.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgvResults.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvResults.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvResults.Columns.Count; j++)
                    {
                        if (dgvResults.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = " " + dgvResults.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }

                workbook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Close(true);
                app.Quit();

                releaseObject(worksheet);
                releaseObject(workbook);
                releaseObject(app);
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

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
        public List<ReportDataSet> ReportDataSet { get; set; }
    }

    internal class ReportDataSet
    {
        public string DataSetName { get; set; }
        public string Query { get; set; }
    }
}
