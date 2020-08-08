using CodingChallenge.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingChallenge
{
    public partial class CodingChallengeForm : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private DataProcessor processor = null;
        private delegate void SafeCallDelegate(Control control, string text);
        private IBindingList displayedList;
        /// <summary>Provided a field that could be overwritten if I wanted to change 
        /// the number of closest locations.</summary>
        private int numberOfClosestLocations = 10;
        /// <summary>Provided a field that could be overwritten if I wanted to input another 
        /// file.</summary>
        protected string DataFileName = @"Data.csv";

        public CodingChallengeForm()
        {
            InitializeComponent();
        }

        /// <summary>This is the DataUpdated <see cref="EventHandler"/> that gets fired when 
        /// the processer finishes processing.  It is meant to trigger an update on the labels.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void Processor_DataUpdated(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        /// <summary>The async Load <see cref="EventHandler>"/>.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private async void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            await OnFormLoadAsync();
        }

        /// <summary>Async method to handle the Form Load <see cref="EventHandler"/>.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        /// <returns>A <see cref="Task"/> object.</returns>
        private async Task OnFormLoadAsync()
        {
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAreaEnabled;
            try
            {
                await LoadData();

                PopulateLoadedData();

                importedDataGridView.SelectionChanged -= ImportedDataGridView_SelectionChanged;
                InitializeDataGridViewProperties(importedDataGridView);
                importedDataGridView.DataSource = bindingSource;
                importedDataGridView.ClearSelection();
                importedDataGridView.SelectionChanged += ImportedDataGridView_SelectionChanged;
                SetDefaultDataGridViewColumnProperties(importedDataGridView);
                UpdateLabels();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(this, string.Concat(ex.Message, Environment.NewLine, ex.StackTrace), 
                                    string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>This method populates the data loaded from the <see cref="processor"/>.</summary>
        private void PopulateLoadedData()
        {
            displayedList = processor.ImportedLocations;
            bindingSource.DataSource = displayedList;
        }

        /// <summary>Async method that loads the data into the main Data Grid.</summary>
        /// <returns>Not used.</returns>
        private async Task LoadData()
        {
            importedDataGridView.VirtualMode = true;
            FileInfo file = GetDataFileInfo();
            InitializeDataProcessor(file);
            await processor.LoadDataAsync(file.FullName);
        }

        /// <summary>This method initializes the Data Processor. If the input file type 
        /// is not supported, <see cref="ArgumentOutOfRangeException"/> will be thrown.</summary>
        /// <param name="file">The input <see cref="FileInfo"/> object.</param>
        /// <exception cref="ArgumentOutOfRangeException">Will be thrown if the extension isn't 
        /// ".csv" or ".xlsx".</exception>
        private void InitializeDataProcessor(FileInfo file)
        {
            if (file.Extension.Equals(".csv"))
            {
                processor = new CsvDataProcessor();
            }
            else if (file.Extension.Equals("xlsx"))
            {
                processor = new ExcelDataProcessor();
            }
            else
            {
                throw new ArgumentOutOfRangeException(file.FullName, 
                    string.Format(Resources.FileTypeNotSupportedMessage, file.Extension));
            }
            processor.DataUpdated += Processor_DataUpdated;
        }

        /// <summary>This method generates the link for the Bing Maps website and uses the 
        /// input parameters to populate it.</summary>
        /// <param name="startLocation">The start <see cref="Location"/>.</param>
        /// <param name="endLocation">The <b>optional</b> end <see cref="Location"/>.</param>
        /// <returns>The fully qualified map URL.</returns>
        private string GenerateBingMapsLinkForLocation(Location startLocation, Location endLocation = null)
        {
            if (endLocation != null)
            {
                return string.Format(Resources.BingMapLocationRouteLink,
                                     startLocation.Latitude, startLocation.Longitude,
                                     endLocation.Latitude, endLocation.Longitude);
            }
            else
            {
                return string.Format(Resources.BingMapLocationLink,
                                     startLocation.Latitude, startLocation.Longitude);
            }
        }

        /// <summary>This method gets the <see cref="FileInfo"/> for the input file. If not 
        /// provided, <paramref name="inputFile"/> will be set to <see cref="DataFileName"/>.</summary>
        /// <param name="inputFile">The input file.</param>
        /// <returns>A <see cref="FileInfo"/> object.</returns>
        private FileInfo GetDataFileInfo(string inputFile = "")
        {
            FileInfo filePath = string.IsNullOrEmpty(inputFile)
                                    ? FileInputUtil.GetFileInfo("Data", DataFileName)
                                    : new FileInfo(inputFile);

            return filePath;
        }

        private Control GetToolStripItemOwner(ToolStripItem toolStripItem)
        {
            Control sourceControl = new Control();

            if (toolStripItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = toolStripItem.Owner as ContextMenuStrip;
                sourceControl = GetContextMenuStripOwner(owner);
            }

            return sourceControl;
        }

        private Control GetContextMenuStripOwner(ContextMenuStrip contextMenuStrip)
        {
            Control sourceControl = new Control();

            if (contextMenuStrip != null)
            {
                // Get the control that is displaying this context menu
                sourceControl = contextMenuStrip.SourceControl;
            }

            return sourceControl;
        }

        /// <summary>This method initializes the <paramref name="dgv"/> properties. This needs to happen 
        /// for performance purposes and so that the importedDataGridView_SelectionChanged. 
        /// <see cref="EventHandler"/> doesn't get triggered which causes a NullReferenceException.</summary>
        private void InitializeDataGridViewProperties(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
        }

    /// <summary>This method sets the default input <paramref name="dgv"/> column properties.</summary>
    /// <param name="dgv">The input <see cref="DataGridView"/>.</param>
    private void SetDefaultDataGridViewColumnProperties(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[Resources.Address].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[Resources.Address].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[Resources.City].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[Resources.City].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[Resources.State].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[Resources.State].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[Resources.State].Width = 40;
            dgv.Columns[Resources.State].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[Resources.State].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[Resources.Zip].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[Resources.Zip].Width = 52;
            dgv.Columns[Resources.Zip].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[Resources.Zip].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[Resources.Zip].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[Resources.Latitude].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[Resources.Latitude].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[Resources.Longitude].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[Resources.Longitude].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.RowHeadersVisible = false;
        }

        /// <summary>The method that loads the closest Locations based on selected Location.</summary>
        /// <returns>Not used.</returns>
        private async Task LoadSelectedData()
        {
            if (importedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = importedDataGridView.SelectedRows[0];
                await Task.Run( () => processor.LoadClosestLocations((Location)selectedRow.DataBoundItem, numberOfClosestLocations));

                InitializeDataGridViewProperties(resultsDataGridView);

                SortedDictionary<double, Location> sortedDict = processor.ClosestLocations;

                // since we only want to return numberOfClosestLocations Locations, use GetRange method on the list.
                resultsBindingSource.DataSource = sortedDict.Values;
                resultsDataGridView.DataSource = resultsBindingSource;
                resultsDataGridView.ClearSelection();

                List<double> distanceList = sortedDict.Keys.ToList();
                PopulateResultsToolTips(distanceList);
            }
        }

        /// <summary>This method populates the results <see cref="ToolTip"/> with location 
        /// distance information.  Distances are displayed in feet and inches from selected location.</summary>
        /// <param name="distanceList">The input <see cref="List{double}"/> that contains the distances.</param>
        private void PopulateResultsToolTips(List<double> distanceList)
        {
            List<double>.Enumerator count = distanceList.GetEnumerator();
            foreach (DataGridViewRow row in resultsDataGridView.Rows)
            {
                _ = count.MoveNext();
                double distance = count.Current;

                Tuple<int, int> feetInches = ConvertDoubleMetersToIntFeetInches(distance);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ToolTipText = string.Format(Resources.ResultsToolTip, feetInches.Item1, feetInches.Item2);
                }
            }
        }

        /// <summary>The method that prevents the <see cref="ContextMenuStrip"/> from opening 
        /// if there is no <see cref="Location"/> selected. Will also change the text on the 
        /// <see cref="ToolStripMenuItem"/> depending on whether a route is selected or just 
        /// a <see cref="Location"/>.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Used to cancel the Opening event if no rows are selected.</param>
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)GetContextMenuStripOwner(sender as ContextMenuStrip);

            if (importedDataGridView.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                // clear out the contents of the contextMenuStrip
                contextMenuStrip.Items.Clear();

                // repopulate the contents
                if (dgv.Equals(resultsDataGridView))
                {
                    contextMenuStrip.Items.AddRange(new[] { toolStripMenuItemOpenLocation,
                                                        toolStripMenuItemOpenRoute});
                }
                else if (dgv.Equals(importedDataGridView))
                {
                    contextMenuStrip.Items.AddRange(new[] { toolStripMenuItemOpenLocation });
                }
            }
        }

        private void DataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>This method converts <paramref name="meters"/> of type <see cref="double"/> to 
        /// feet and inches.</summary>
        /// <param name="meters">The input meters value.</param>
        /// <returns>A <see cref="Tuple{int, int}"/> where <b>Item1</b>=feet and 
        /// <b>Item2</b>=inches.</returns>
        private Tuple<int, int> ConvertDoubleMetersToIntFeetInches(double meters)
        {
            double dblFeet, value = 0.3048;
            int feet, inches;
            dblFeet = meters / value;
            feet = (int)dblFeet;
            double temp = (dblFeet - Math.Truncate(dblFeet)) / 0.08333;
            inches = (int)temp;

            return Tuple.Create(feet, inches);
        }

        /// <summary>The method to handle the DataBindingComplete <see cref="EventHandler"/>.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ImportedDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        /// <summary>The method to handle the SelectionChanged <see cref="EventHandler"/>. If there is a 
        /// row selected, the selected data gets loaded and the resultsDataGridView properties get set.</summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private async void ImportedDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (importedDataGridView.SelectedRows.Count > 0)
                {
                    await LoadSelectedData();
                    SetDefaultDataGridViewColumnProperties(resultsDataGridView);
                    resultsDataGridView.Columns[Resources.Address].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    resultsDataGridView.Columns[Resources.Address].MinimumWidth = importedDataGridView.Columns[Resources.Address].Width;
                    resultsDataGridView.Columns[Resources.City].MinimumWidth = importedDataGridView.Columns[Resources.City].Width;
                    resultsDataGridView.Columns[Resources.State].Width = importedDataGridView.Columns[Resources.State].Width;
                    resultsDataGridView.Columns[Resources.Zip].Width = importedDataGridView.Columns[Resources.Zip].Width;
                }
                else
                {
                    resultsBindingSource.DataSource = null;
                    resultsDataGridView.DataSource = resultsBindingSource;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = string.Format(Resources.SelectLocationError, ex.Message);
                MessageBox.Show(this, errorMsg, Resources.LocationSelectionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>The method that paints the rows <see cref="System.Drawing.Color.LightGray"/> when 
        /// the CellMouseMove <see cref="EventHandler"/> event is triggered.</summary>
        /// <param name="sender">The <see cref="DataGridView"/> sending the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> object.</param>
        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }
        }

        /// <summary>The method that paints the rows <see cref="System.Drawing.Color.White"/> when 
        /// the CellMouseLeave <see cref="EventHandler"/> event is triggered.</summary>
        /// <param name="sender">The <see cref="DataGridView"/> sending the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> object.</param>
        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
        }

        /// <summary>The method to handle the TextChanged <see cref="EventHandler"/>.</summary>
        /// <param name="sender">The input <see cref="TextBox"/> object.</param>
        /// <param name="e">Not used.</param>
        private void TxtAddressSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            bindingSource.DataSource = processor.GetFilteredData(tb.Text);
            UpdateLabels();
            importedDataGridView.DataSource = bindingSource;
        }

        /// <summary>The method that updates the labels in a thread-safe manner.</summary>
        private void UpdateLabels()
        {
            WriteTextSafe(lblNumRows, string.Format(Resources.NumberOfRows, importedDataGridView.Rows.Count));
        }

        /// <summary>This method allows for thread-safe calls to Windows Forms controls.</summary>
        /// <param name="control">The input <see cref="Control"/> object.</param>
        /// <param name="text">The text to be written to the control.</param>
        /// <remarks>I got this code from:
        /// <para>https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-make-thread-safe-calls-to-windows-forms-controls#example-use-the-invoke-method-with-a-delegate</para></remarks>
        private void WriteTextSafe(Control control, string text)
        {
            if (lblNumRows.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                control.Invoke(d, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        /// <summary>This method opens up a browser to the selected <see cref="Location"/> or 
        /// it will show a route between the selected <see cref="Location"/> in the <b>importDataGridView</b> 
        /// and the selected <see cref="Location"/> in the <b>resultsDataGridView</b>.</summary>
        /// <param name="sender">The source <see cref="ToolStripItem"/>.</param>
        /// <param name="e">Not used.</param>
        private void ToolStripMenuItemOpenLocation_Click(object sender, System.EventArgs e)
        {
            string url = string.Empty;
            DataGridView dgv = (DataGridView)GetToolStripItemOwner(sender as ToolStripItem);
 
            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                Location loc = (Location)dgv.SelectedRows[0].DataBoundItem;

                if (loc != null)
                {
                    url = GenerateBingMapsLinkForLocation(loc);
                    _ = Process.Start(url);
                }
            }
        }

        /// <summary>This method opens up a browser to show a route between the selected 
        /// <see cref="Location"/> in the <b>importDataGridView</b> 
        /// and the selected <see cref="Location"/> in the <b>resultsDataGridView</b>.</summary>
        /// <param name="sender">The source <see cref="ToolStripItem"/>.</param>
        /// <param name="e">Not used.</param>
        private void ToolStripMenuItemOpenRoute_Click(object sender, EventArgs e)
        {
            string url = string.Empty;
            DataGridView dgv = (DataGridView)GetToolStripItemOwner(sender as ToolStripItem);

            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                Location start = (Location)importedDataGridView.SelectedRows[0].DataBoundItem;
                Location end = (Location)resultsDataGridView.SelectedRows[0].DataBoundItem;

                if (start != null && end != null)
                {
                    url = GenerateBingMapsLinkForLocation(start, end);
                    _ = System.Diagnostics.Process.Start(url);
                }
            }
        }
    }
}
