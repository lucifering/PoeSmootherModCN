namespace PoeSmoother
{
    using Ionic.Zip;
    using LibGGPK;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using KUtility;
    using System.Drawing.Imaging;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string ggpkPath = string.Empty;
        private GGPK content;
        private Thread workerThread;

        
        /// <summary>
        /// Dictionary mapping ggpk file paths to FileRecords for easy lookup
        /// EG: "Scripts\foobar.mel" -> FileRecord{Foobar.mel}
        /// </summary>
        private Dictionary<string, FileRecord> RecordsByPath;

        public MainWindow()
        {
            InitializeComponent();
            
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void OutputLine(string msg)
        {
            Output(msg + Environment.NewLine);
        }
        private void Output(string msg)
        {
            textBoxOutput.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBoxOutput.Text += msg;
            }), null);
        }
        private void UpdateTitle(string newTitle)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Title = newTitle;
            }), null);
        }

        /// <summary>
        /// Reloads the entire content.ggpk, rebuilds the tree
        /// </summary>
        private void ReloadGGPK()
        {
            treeView.Items.Clear();
            ResetViewer();
            textBoxOutput.Visibility = Visibility.Visible;
            textBoxOutput.Text = string.Empty;
            content = null;

            workerThread = new Thread(() =>
            {
                content = new GGPK();
                try
                {
                    content.Read(ggpkPath, Output);
                }
                catch (Exception ex)
                {
                    Output(string.Format(Settings.Strings["ReloadGGPK_Failed"], ex.Message));
                    return;
                }
                if (content.IsReadOnly)
                {
                    Output(Settings.Strings["ReloadGGPK_ReadOnly"] + Environment.NewLine);
                    UpdateTitle(Settings.Strings["MainWindow_Title_Readonly"]);
                }
                OutputLine(Settings.Strings["ReloadGGPK_Traversing_Tree"]);

                // Collect all FileRecordPath -> FileRecord pairs for easier replacing
                RecordsByPath = new Dictionary<string, FileRecord>(content.RecordOffsets.Count);
                DirectoryTreeNode.TraverseTreePostorder(content.DirectoryRoot, null, n => RecordsByPath.Add(n.GetDirectoryPath() + n.Name, n));
                treeView.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        AddDirectoryTreeToControl(content.DirectoryRoot, null);
                    }
                    catch (Exception ex)
                    {
                        Output(string.Format(Settings.Strings["Error_Read_Directory_Tree"], ex.Message));
                        return;
                    }
                    workerThread = null;
                }), null);
                OutputLine(Settings.Strings["ReloadGGPK_Successful"]);
            });
            workerThread.Start();
        }

        /// <summary>
        /// Recursivly adds the specified GGPK DirectoryTree to the TreeListView
        /// </summary>
        /// <param name="directoryTreeNode">Node to add to tree</param>
        /// <param name="parentControl">TreeViewItem to add children to</param>
        private void AddDirectoryTreeToControl(DirectoryTreeNode directoryTreeNode, ItemsControl parentControl)
        {
            TreeViewItem rootItem = new TreeViewItem { Header = directoryTreeNode };
            if ((directoryTreeNode.ToString() == "ROOT") || (directoryTreeNode.ToString() == "")) rootItem.IsExpanded = true;

            if (parentControl == null)
            {
                treeView.Items.Add(rootItem);
            }
            else
            {
                parentControl.Items.Add(rootItem);
            }
            directoryTreeNode.Children.Sort();
            foreach (var item in directoryTreeNode.Children)
            {
                AddDirectoryTreeToControl(item, rootItem);
            }
            directoryTreeNode.Files.Sort();
            foreach (var item in directoryTreeNode.Files)
            {
                rootItem.Items.Add(item);
            }
        }

        /// <summary>
        /// Resets all of the file viewers
        /// </summary>
        private void ResetViewer()
        {
            textBoxOutput.Visibility = Visibility.Hidden;
            richTextOutput.Visibility = Visibility.Hidden;
            textBoxOutput.Clear();
            richTextOutput.Document.Blocks.Clear();
        }

        /// <summary>
        /// Updates the FileViewers to display the currently selected item in the TreeView
        /// </summary>
        private void UpdateDisplayPanel()
        {
            ResetViewer();
            if (treeView.SelectedItem == null)
            {
                return;
            }
            var item = treeView.SelectedItem as TreeViewItem;
            if (item?.Header is DirectoryTreeNode)
            {
                DirectoryTreeNode selectedDirectory = (DirectoryTreeNode)item.Header;
                if (selectedDirectory.Record == null) return;
            }
            FileRecord selectedRecord = treeView.SelectedItem as FileRecord;
            if (selectedRecord == null) return;
            try
            {
                imageOutput.Visibility = Visibility.Hidden;
                datViewerOutput.Visibility = Visibility.Hidden;
                switch (selectedRecord.FileFormat)
                {
                    case FileRecord.DataFormat.Ascii: DisplayAscii(selectedRecord); break;
                    case FileRecord.DataFormat.Unicode: DisplayUnicode(selectedRecord); break;
                    case FileRecord.DataFormat.RichText: DisplayRichText(selectedRecord); break;


                    case FileRecord.DataFormat.Image:
                        DisplayImage(selectedRecord);
                        break;
                    case FileRecord.DataFormat.TextureDDS:
                        DisplayDDS(selectedRecord);
                        break;
                    case FileRecord.DataFormat.Dat:
                        DisplayDat(selectedRecord);
                        break;
                }
            }
            catch (Exception ex)
            {
                imageOutput.Visibility = Visibility.Hidden;
                datViewerOutput.Visibility = Visibility.Hidden;
                ResetViewer();
                textBoxOutput.Visibility = Visibility.Visible;
                StringBuilder sb = new StringBuilder();
                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }
                textBoxOutput.Text = string.Format(Settings.Strings["UpdateDisplayPanel_Failed"], sb);
            }
        }


        /// <summary>
		/// Displays the contents of a FileRecord in the ImageBox
		/// </summary>
		/// <param name="selectedRecord">FileRecord to display</param>
		private void DisplayImage(FileRecord selectedRecord)
        {
            byte[] buffer = selectedRecord.ReadData(ggpkPath);
            imageOutput.Visibility = System.Windows.Visibility.Visible;

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                imageOutput.Source = bmp;
            }
        }
        /// <summary>
        /// Displays the contents of a FileRecord in the ImageBox (DDS Texture mode)
        /// </summary>
        /// <param name="selectedRecord">FileRecord to display</param>
        private void DisplayDDS(FileRecord selectedRecord)
        {
            byte[] buffer = selectedRecord.ReadData(ggpkPath);
            imageOutput.Visibility = System.Windows.Visibility.Visible;
            Console.WriteLine("DisplayDDS :" + ggpkPath);


            DDSImage dds = new DDSImage(buffer);

            Console.WriteLine("DisplayDDS DDSImage ok");


            using (MemoryStream ms = new MemoryStream())
            {

                Console.WriteLine("DisplayDDS MemoryStream ok");


                dds.images[0].Save(ms, ImageFormat.Png);

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                imageOutput.Source = bmp;

                Console.WriteLine("DisplayDDS MemoryStream end");
            }
        }
        /// <summary>
		/// Displays the contents of a FileRecord in the DatViewer
		/// </summary>
		/// <param name="selectedRecord">FileRecord to display</param>
		private void DisplayDat(FileRecord selectedRecord)
        {
            byte[] data = selectedRecord.ReadData(ggpkPath);
            datViewerOutput.Visibility = System.Windows.Visibility.Visible;

            using (MemoryStream ms = new MemoryStream(data))
            {
                datViewerOutput.Reset(selectedRecord.Name, ms);
            }
        }
        /// <summary>
        /// Displays the contents of a FileRecord in the RichTextBox
        /// </summary>
        /// <param name="selectedRecord">FileRecord to display</param>
        private void DisplayRichText(FileRecord selectedRecord)
        {
            byte[] buffer = selectedRecord.ReadData(ggpkPath);
            richTextOutput.Visibility = Visibility.Visible;
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                richTextOutput.Selection.Load(ms, DataFormats.Rtf);
            }
        }

        /// <summary>
        /// Displays the contents of a FileRecord in the TextBox as Unicode text
        /// </summary>
        /// <param name="selectedRecord">FileRecord to display</param>
        private void DisplayUnicode(FileRecord selectedRecord)
        {
            byte[] buffer = selectedRecord.ReadData(ggpkPath);
            textBoxOutput.Visibility = Visibility.Visible;
            textBoxOutput.Text = Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Displays the contents of a FileRecord in the TextBox as Ascii text
        /// </summary>
        /// <param name="selectedRecord">FileRecord to display</param>
        private void DisplayAscii(FileRecord selectedRecord)
        {
            byte[] buffer = selectedRecord.ReadData(ggpkPath);
            textBoxOutput.Visibility = Visibility.Visible;
            textBoxOutput.Text = Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Exports the specified FileRecord to disk
        /// </summary>
        /// <param name="selectedRecord">FileRecord to export</param>
        private void ExportFileRecord(FileRecord selectedRecord)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog { FileName = selectedRecord.Name };
                if (saveFileDialog.ShowDialog() != true) return;
                selectedRecord.ExtractFile(ggpkPath, saveFileDialog.FileName);
                MessageBox.Show(string.Format(Settings.Strings["ExportSelectedItem_Successful"],
                    selectedRecord.DataLength), Settings.Strings["ExportAllItemsInDirectory_Successful_Caption"],
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Settings.Strings["ExportSelectedItem_Failed"], ex.Message),
                    Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Exports entire DirectoryTreeNode to disk, preserving directory structure
        /// </summary>
        /// <param name="selectedDirectoryNode">Node to export to disk</param>
        private void ExportAllItemsInDirectory(DirectoryTreeNode selectedDirectoryNode)
        {
            List<FileRecord> recordsToExport = new List<FileRecord>();
            Action<FileRecord> fileAction = recordsToExport.Add;
            DirectoryTreeNode.TraverseTreePreorder(selectedDirectoryNode, null, fileAction);
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = Settings.Strings["ExportAllItemsInDirectory_Default_FileName"]
                };
                if (saveFileDialog.ShowDialog() != true) return;
                string exportDirectory = Path.GetDirectoryName(saveFileDialog.FileName) + Path.DirectorySeparatorChar;
                foreach (var item in recordsToExport)
                {
                    item.ExtractFileWithDirectoryStructure(ggpkPath, exportDirectory);
                }
                MessageBox.Show(string.Format(Settings.Strings["ExportAllItemsInDirectory_Successful"], recordsToExport.Count),
                    Settings.Strings["ExportAllItemsInDirectory_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Settings.Strings["ExportAllItemsInDirectory_Failed"], ex.Message),
                    Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Replaces selected file with file user selects via MessageBox
        /// </summary>
        /// <param name="recordToReplace"></param>
        private void ReplaceItem(FileRecord recordToReplace)
        {
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]);
                return;
            }
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    FileName = "",
                    CheckFileExists = true,
                    CheckPathExists = true
                };
                if (openFileDialog.ShowDialog() != true) return;
                recordToReplace.ReplaceContents(ggpkPath, openFileDialog.FileName, content.FreeRoot);
                MessageBox.Show(string.Format(
                    Settings.Strings["ReplaceItem_Successful"], recordToReplace.Name, recordToReplace.RecordBegin.ToString("X")),
                    Settings.Strings["ReplaceItem_Successful_Caption"],
                    MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateDisplayPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Settings.Strings["ReplaceItem_Failed"], ex.Message),
                    Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Extracts specified archive and replaces files in GGPK with extracted files. Files in
        /// archive must have same directory structure as in GGPK.
        /// </summary>
        /// <param name="archivePath">Path to archive containing</param>
        private void HandleDropArchive(string archivePath)
        {
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]);
                return;
            }

            OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropArchive_Info"], archivePath));

            using (ZipFile zipFile = new ZipFile(archivePath))
            {
                //var fileNames = zipFile.EntryFileNames;

                // Archive Version Check: Read version.txt and check with patch_notes.rtf's Hash
                foreach (var item in zipFile.Entries.Where(item => item.FileName.Equals("version.txt")))
                {
                    using (var reader = item.OpenReader())
                    {
                        byte[] versionData = new byte[item.UncompressedSize];
                        reader.Read(versionData, 0, versionData.Length);
                        string versionStr = Encoding.UTF8.GetString(versionData, 0, versionData.Length);
                        if (RecordsByPath.ContainsKey("patch_notes.rtf"))
                        {
                            string Hash = BitConverter.ToString(RecordsByPath["patch_notes.rtf"].Hash);
                            if (!versionStr.Substring(0, Hash.Length).Equals(Hash))
                            {
                                OutputLine(Settings.Strings["MainWindow_VersionCheck_Failed"]); return;
                            }
                        }
                    }
                    break;
                }

                foreach (var item in zipFile.Entries)
                {
                    if (item.IsDirectory) { continue; }
                    if (item.FileName.Equals("version.txt")) { continue; }
                    string fixedFileName = item.FileName;
                    if (Path.DirectorySeparatorChar != '/')
                    {
                        fixedFileName = fixedFileName.Replace('/', Path.DirectorySeparatorChar);
                        if (fixedFileName != null ) {
                            if (fixedFileName.StartsWith("ROOT\\"))
                            {
                                fixedFileName = fixedFileName.Substring("ROOT\\".Length);

                            }
                            if (fixedFileName.StartsWith("\\")) {
                                fixedFileName = fixedFileName.Substring("\\".Length);
                            }
                           

                        }
                        Console.WriteLine("fixedFileName="+ fixedFileName);

                    }
                    if (!RecordsByPath.ContainsKey(fixedFileName))
                    {
                        OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropDirectory_Failed"], fixedFileName)); continue;
                    }
                    OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropDirectory_Replace"], fixedFileName));
                    using (var reader = item.OpenReader())
                    {
                        byte[] replacementData = new byte[item.UncompressedSize];
                        reader.Read(replacementData, 0, replacementData.Length);
                        RecordsByPath[fixedFileName].ReplaceContents(ggpkPath, replacementData, content.FreeRoot);
                    }
                }
                OutputLine(Settings.Strings["All_Done"]);
            }
        }

        /// <summary>
        /// Replaces the currently selected TreeViewItem with specified file on disk
        /// </summary>
        /// <param name="fileName">Path of file to replace currently selected item with.</param>
        private void HandleDropFile(string fileName)
        {
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]); return;
            }
            FileRecord record = treeView.SelectedItem as FileRecord;
            if (record == null)
            {
                OutputLine(Settings.Strings["MainWindow_HandleDropFile_Failed"]); return;
            }
            OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropFile_Replace"], record.GetDirectoryPath(), record.Name));
            record.ReplaceContents(ggpkPath, fileName, content.FreeRoot);
        }

        /// <summary>
        /// Specified directory was dropped onto interface, attept to replace GGPK files with same directory
        /// structure with files in directory. Directory must have same directory structure as GGPK file.
        /// EG:
        /// dropping 'Art' directory containing '2DArt' directory containing 'BuffIcons' directory containing 'buffbleed.dds' will replace
        /// \Art\2DArt\BuffIcons\buffbleed.dds with buffbleed.dds from dropped directory
        /// </summary>
        /// <param name="baseDirectory">Directory containing files to replace</param>
        private void HandleDropDirectory(string baseDirectory)
        {
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]); return;
            }
            string[] filesToReplace = Directory.GetFiles(baseDirectory, "*.*", SearchOption.AllDirectories);
            var fileName = Path.GetFileName(baseDirectory);
            {
                int baseDirectoryNameLength = fileName.Length;
                OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropDirectory_Count"], filesToReplace.Length));
                foreach (var item in filesToReplace)
                {
                    string fixedFileName = item.Remove(0, baseDirectory.Length - baseDirectoryNameLength);
                    if (!RecordsByPath.ContainsKey(fixedFileName))
                    {
                        OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropDirectory_Failed"], fixedFileName)); continue;
                    }
                    OutputLine(string.Format(Settings.Strings["MainWindow_HandleDropDirectory_Replace"], fixedFileName));
                    RecordsByPath[fixedFileName].ReplaceContents(ggpkPath, item, content.FreeRoot);
                }
            }
        }

        private void initText() {
            exit.ToolTip = Settings.Strings["MainWindow_Button_Exit"];
            min.ToolTip = Settings.Strings["MainWindow_Button_Min"];
         
            Effects.Header = Settings.Strings["MainWindow_Window_Tab_Effects_Header"];
            Info.Header = Settings.Strings["MainWindow_Window_Tab_Info_Header"];
            Skills.Header = Settings.Strings["MainWindow_Window_Tab_Skills_Header"];
            tb_01.Text = Settings.Strings["About_Msg_1"];
            tb_02.Text = Settings.Strings["About_Msg_2"];
            tb_03.Text = Settings.Strings["About_Msg_3"];
            tb_04.Text = Settings.Strings["About_Msg_4"];
            tb_05.Text = Settings.Strings["About_Msg_5"];
            tb_06.Text = Settings.Strings["About_Msg_6"];
            tb_07.Content= Settings.Strings["About_Msg_7"];
            tb_08.Content = Settings.Strings["About_Msg_8"];
            tb_07.ToolTip= Settings.Strings["About_Msg_7_2"];
            tb_08.ToolTip = Settings.Strings["About_Msg_8_2"];

        }
        private void PoeSmoother_Loaded(object sender, RoutedEventArgs e)
        {


            location_us.Visibility = Visibility.Hidden;
            location_cn.Visibility = Visibility.Hidden;
            if (File.Exists("./Location.json")) {
                try
                {
                    string jsonLocal = File.ReadAllText(@"./Location.json", Encoding.UTF8);
                    JObject job = JObject.Parse(jsonLocal);
                    if (job != null && job.GetValue("location") != null) {


                        if (job.GetValue("location").ToString().Equals("CN"))
                        {
                            Settings.isEn = false;
                            location_us.Visibility = Visibility.Visible;
                        }
                        else {
                            Settings.isEn = true;
                            location_cn.Visibility = Visibility.Visible;
                        }

                    }

                }
                catch { }

                }

            initText();
            location_cn.ToolTip = Settings.Strings["MainWindow_Button_Location_CN"];
            location_us.ToolTip = Settings.Strings["MainWindow_Button_Location_US"];
            string jsonModPath = Settings.isEn ? @"./us/MOD.json" : @"./cn/MOD.json";
            string jsonEffectPath = Settings.isEn ? @"./us/Effects.json" : @"./cn/Effects.json";
            string jsonSkillsPath = Settings.isEn ? @"./us/Skills.json" : @"./cn/Skills.json";
            

            // panelShowEffect
            loadPanel(jsonModPath, panelShowBack);

            loadPanel(jsonEffectPath, panelShowEffect);
            loadPanel(jsonSkillsPath, panelShowSkills);

            /*

             */
            // System.IO.Directory.SetCurrentDirectory(System.Windows.Forms.Application.StartupPath);
            OpenFileDialog ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = Settings.Strings["Load_GGPK_Filter"]
            };
            // Get InstallLocation From RegistryKey
            if ((ofd.InitialDirectory == null) || (ofd.InitialDirectory == string.Empty))
            {
                RegistryKey start = Registry.CurrentUser;
                RegistryKey programName = start.OpenSubKey(@"Software\GrindingGearGames\Path of Exile");
                if (programName != null)
                {
                    string pathString = (string)programName.GetValue("InstallLocation");
                    if (pathString != string.Empty && File.Exists(pathString + @"\Content.ggpk"))
                    {
                        ofd.InitialDirectory = pathString;
                    }
                }
            }


            // Get Garena PoE
            if ((ofd.InitialDirectory == null) || (ofd.InitialDirectory == string.Empty))
            {
                RegistryKey start = Registry.LocalMachine;
                RegistryKey programName = start.OpenSubKey(@"SOFTWARE\Wow6432Node\Garena\PoE");
                if (programName != null)
                {
                    string pathString = (string)programName.GetValue("Path");
                    if (pathString != string.Empty && File.Exists(pathString + @"\Content.ggpk"))
                    {
                        ofd.InitialDirectory = pathString;
                    }
                }
            }


            try
            {
                if (ofd != null && ofd.ShowDialog() == true)
                {

                    if (!File.Exists(ofd.FileName))
                    {

                        Close();
                        return;
                    }
                    ggpkPath = ofd.FileName;

                    ReloadGGPK();
                }
                else
                {

                    Close();
                    return;
                }
            }
            catch (Exception e1)
            {
                ofd.InitialDirectory = "";
                if (ofd != null && ofd.ShowDialog() == true)
                {

                    if (!File.Exists(ofd.FileName))
                    {

                        Close();
                        return;
                    }
                    ggpkPath = ofd.FileName;

                    ReloadGGPK();
                }
                else
                {
                    Close();
                    return;
                }

            }

            menuItemExport.Header = Settings.Strings["MainWindow_Menu_Export"];
            menuItemReplace.Header = Settings.Strings["MainWindow_Menu_Replace"];

        }


        private void loadPanel(string jsonModPath, StackPanel panel) {

            if (File.Exists(jsonModPath))
            {

                try
                {
                    string jsonText = File.ReadAllText(jsonModPath, Encoding.UTF8);

                    // string jsonText = "[     {         \"Name\": \"测试1\",         \"Items\": [             {                 \"Name\": \"modSkill15\",                 \"Content\": \"- 电弧闪电传送·红\",                 \"Checked\": \"Arc2Red\",                 \"Unchecked\": \"Arc2Red\",                 \"ToolTip\": \"by 阿帕契公主\",                 \"Tag\": {                     \"ToolTipImg\": \"/mod/电弧闪电传送·红.preview\",                     \"NewMOD\": \"config/MOD/Arc2Red/newEffects/Art\",                     \"OldMOD\": \"config/MOD/Arc2Red/restoreDefault/Art\"                 }             }         ]     } ]";

                    //  Console.WriteLine("jsonText:" + jsonText);
                    JArray jaAlllist = JArray.Parse(jsonText);

                    if (jaAlllist != null && jaAlllist.Count > 0)
                    {
                        Console.WriteLine("jaAlllist:" + jaAlllist.Count);
                        for (int i = 0; i < jaAlllist.Count; i++)
                        {

                            JObject obData = JObject.Parse(jaAlllist[i].ToString());

                            if (obData.GetValue("Name") != null)
                            {
                                string title = obData.GetValue("Name") == null ? "" : obData.GetValue("Name").ToString();

                                Console.WriteLine("title:" + title);
                                TextBlock tb = new TextBlock();
                                tb.HorizontalAlignment = HorizontalAlignment.Left;
                                tb.TextWrapping = TextWrapping.Wrap;
                                tb.Width = 227;
                                tb.FontSize = 16;
                                tb.Height = 24;
                                tb.VerticalAlignment = VerticalAlignment.Center;
                                tb.Margin = new Thickness(5, 10, 0, 0);

                                tb.Foreground = new SolidColorBrush(Color.FromRgb(223, 207, 153));
                                tb.Text = title;
                                panel.Children.Add(tb);
                                if (obData.GetValue("Items") != null)
                                {
                                    JArray itemlist = JArray.Parse(obData.GetValue("Items").ToString());

                                    if (itemlist != null && itemlist.Count > 0)
                                    {
                                        for (int j = 0; j < itemlist.Count; j++)
                                        {
                                            // Console.WriteLine(i + "<CB>" + j);
                                            JObject obItem = JObject.Parse(itemlist[j].ToString());
                                            CheckBox cb = new CheckBox();
                                            cb.Content = obItem.GetValue("Content") == null ? "" : obItem.GetValue("Content").ToString();
                                            cb.Checked += new RoutedEventHandler(ModReplaceCheck);
                                            cb.Unchecked += new RoutedEventHandler(ModReplaceCheck);
                                            cb.IsChecked = false;
                                            cb.Background = null;
                                            cb.FontSize = 14;
                                            string tip = obItem.GetValue("ToolTip") == null ? "" : obItem.GetValue("ToolTip").ToString();
                                            if (!tip.Equals(""))
                                            {
                                                cb.ToolTip = tip;
                                            }
                                            cb.Padding = new Thickness(3, -3, 0, 0);
                                            cb.Margin = new Thickness(5, 0, 0, 0);
                                            cb.Foreground = new SolidColorBrush(Color.FromRgb(217, 210, 199));
                                            cb.BorderBrush = new SolidColorBrush(Color.FromScRgb(127, 217, 210, 199));
                                            cb.Height = 18;
                                            cb.MouseEnter += new MouseEventHandler(modChar1_MouseEnterCheck);
                                            cb.MouseLeave += new MouseEventHandler(modChar1_MouseLeaveCheck);
                                            cb.Tag = obItem.GetValue("Tag").ToString();
                                            panel.Children.Add(cb);
                                        }

                                    }

                                }

                            }

                        }

                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(Settings.Strings["JSON_Parse_Failed"]+ jsonModPath+" Error:" + e1.Message);

                }
            }
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateDisplayPanel();
            menuItemReplace.IsEnabled = treeView.SelectedItem is FileRecord;
            if (treeView.SelectedItem is FileRecord)
            {
                // Exporting file
                menuItemExport.IsEnabled = true;
            }
            else if ((treeView.SelectedItem as TreeViewItem)?.Header is DirectoryTreeNode)
            {
                // Exporting entire directory
                menuItemExport.IsEnabled = true;
            }
            else
            {
                menuItemExport.IsEnabled = false;
            }
        }
        private void menuItemExport_Click(object sender, RoutedEventArgs e)
        {
            var item = treeView.SelectedItem as TreeViewItem;
            if (item != null)
            {
                TreeViewItem selectedTreeViewItem = item;
                DirectoryTreeNode selectedDirectoryNode = selectedTreeViewItem.Header as DirectoryTreeNode;
                if (selectedDirectoryNode != null)
                {
                    ExportAllItemsInDirectory(selectedDirectoryNode);
                }
            }
            else if (treeView.SelectedItem is FileRecord)
            {
                ExportFileRecord((FileRecord)treeView.SelectedItem);
            }
        }
        private void menuItemReplace_Click(object sender, RoutedEventArgs e)
        {
            FileRecord recordToReplace = treeView.SelectedItem as FileRecord;
            if (recordToReplace == null) return;
            ReplaceItem(recordToReplace);
        }

       


        #region MOD

        private void ModReplaceCheck(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null && cb.Tag!=null ) {

                Console.WriteLine("【Check】"+cb.Content);
                Console.WriteLine("【`】" + cb.Tag.ToString());


                JObject jobtag = JObject.Parse(cb.Tag.ToString());
                if (jobtag != null) {

                    if (jobtag.GetValue("NewMOD") != null && jobtag.GetValue("OldMOD") != null) {

                        JArray janew = JArray.Parse(jobtag.GetValue("NewMOD").ToString());
                        JArray jaold = JArray.Parse(jobtag.GetValue("OldMOD").ToString());
                        if (janew != null && jaold != null && jaold.Count == janew.Count) {
                            for (int i = 0; i < janew.Count; i++) {

                                string NewEffects = janew[i] == null ? "" : janew[i].ToString();
                                string RestoreDefault = jaold[i] == null ? "" : jaold[i].ToString();
                                ModReplace(cb, NewEffects, RestoreDefault);

                            }

                        }

                    }
                    
                }
                 



            }

        }
        private void ModReplace(CheckBox cb, string NewEffects, string RestoreDefault)
        {
            
            //  Console.WriteLine(">ischecked=" + ischecked);
             Console.WriteLine(">NewEffects=" + NewEffects);
            Console.WriteLine(">RestoreDefault=" + RestoreDefault);
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]);
                return;
            }

            if (!Directory.Exists(NewEffects) || !Directory.Exists(RestoreDefault) || cb == null || (NewEffects.Equals("") && RestoreDefault.Equals(""))) return;
            string[] remove_Effects = Directory.GetFiles(NewEffects, "*.*", SearchOption.AllDirectories);
            var remove_Effects_path = Path.GetFileName(NewEffects);
            int remove_Effects_dir = remove_Effects_path.Length;

            string[] restore_Default = Directory.GetFiles(RestoreDefault, "*.*", SearchOption.AllDirectories);
            var restore_Default_path = Path.GetFileName(RestoreDefault);
            int restore_Default_dir = restore_Default_path.Length;

            string showname = cb.Content.ToString();
            bool? ischecked = cb.IsChecked;
            try
            {
                String tmpDispaly = "";
                switch (ischecked)//!!!
                {
                    case true:
                        {
                            if (remove_Effects == null || remove_Effects.Length == 0)
                            {
                                tmpDispaly = Settings.Strings["MainWindow_Error_File_Nofound"] + NewEffects;
                            }
                            else
                            {
                                foreach (var item in remove_Effects)
                                {
                                    string fileNames = item.Remove(0, NewEffects.Length - remove_Effects_dir);

                                    RecordsByPath[fileNames].ReplaceContents(ggpkPath, item, content.FreeRoot);
                                }
                                UpdateDisplayPanel();
                                tmpDispaly = showname + Settings.Strings["MainWindow_Menu_Replace_Success"];
                            }

                        }
                        break;

                    case false:
                        {
                            if (restore_Default == null || restore_Default.Length == 0)
                            {
                                tmpDispaly = Settings.Strings["MainWindow_Error_File_Nofound"] + restore_Default;
                            }
                            else
                            {
                                foreach (var item in restore_Default)
                                {
                                    string fileNames = item.Remove(0, RestoreDefault.Length - restore_Default_dir);

                                   // Console.WriteLine("now:"+ fileNames);
                                    RecordsByPath[fileNames].ReplaceContents(ggpkPath, item, content.FreeRoot);
                                }
                                UpdateDisplayPanel();
                                tmpDispaly = showname + Settings.Strings["MainWindow_Menu_Recovery_Success"];

                            }

                        }
                        break;
                }
                textBoxOutput.Visibility = Visibility.Visible;
                textBoxOutput.Text = textBoxOutput.Text + "\r\n" + tmpDispaly;
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = "<ERROR>" + showname + (ischecked == true ? Settings.Strings["MainWindow_Menu_Replace"] : Settings.Strings["MainWindow_Menu_Recovery"]) + Settings.Strings["Error_Caption"] +"\r\n" + string.Format(Settings.Strings["ReplaceItem_Failed"], 
                    ex.Message + "\r\n"+Settings.Strings["MainWindow_Error_In"] + (ischecked == true ? NewEffects : RestoreDefault));
                MessageBox.Show(string.Format(Settings.Strings["ReplaceItem_Failed"], ex.Message),
                    Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        #endregion

        private void PoeSmoother_PreviewDrop(object sender, DragEventArgs e)
        {
            if (!content.IsReadOnly)
            {
                e.Effects = DragDropEffects.Link;
            }
        }

        private void PoeSmoother_Drop(object sender, DragEventArgs e)
        {
            if (content.IsReadOnly)
            {
                MessageBox.Show(Settings.Strings["ReplaceItem_Readonly"], Settings.Strings["ReplaceItem_ReadonlyCaption"]);
                return;
            }

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            // Bring-to-front hack
            Topmost = true;
            Topmost = false;

            // reset viewer to show output message
            ResetViewer();
            textBoxOutput.Text = string.Empty;
            textBoxOutput.Visibility = Visibility.Visible;

            if (MessageBox.Show(Settings.Strings["MainWindow_Window_Drop_Confirm"],
                Settings.Strings["MainWindow_Window_Drop_Confirm_Caption"],
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileNames == null || fileNames.Length != 1)
            {
                OutputLine(Settings.Strings["MainWindow_Drop_Failed"]);
                return;
            }

            if (Directory.Exists(fileNames[0]))
            {
                HandleDropDirectory(fileNames[0]);
            }
            else if (string.Compare(Path.GetExtension(fileNames[0]), ".zip", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Zip file
                HandleDropArchive(fileNames[0]);
            }
            else
            {
                HandleDropFile(fileNames[0]);
            }
        }

        private void PoeSmoother_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            workerThread?.Abort();
        }


        

        private void Location2CN(object sender, RoutedEventArgs e)
        {
          
            File.WriteAllText("./Location.json", "{\"location\":\"CN\"}",Encoding.UTF8);
            textBoxOutput.Text = Settings.Strings["MainWindow_Location_Msg"];
            Settings.isEn = false;

        }
        private void Location2US(object sender, RoutedEventArgs e)
        {
          
            File.WriteAllText("./Location.json", "{\"location\":\"US\"}", Encoding.UTF8);
            textBoxOutput.Text = Settings.Strings["MainWindow_Location_Msg"];
            Settings.isEn = true;
        }

        private void TriggerClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TriggerMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void PoeSmoother_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
            }
        }

        private void PoeSmoother_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void LinkLabel_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

            try
            {
                System.Diagnostics.Process.Start(e.Uri.ToString());
            }
            catch { }
        }

        private void modChar1_MouseEnter(object sender, MouseEventArgs e)
        {

            try
            {
                CheckBox tmpCheck = sender as CheckBox;
                if (tmpCheck.Tag != null && !tmpCheck.Tag.ToString().Equals("") && File.Exists(System.Windows.Forms.Application.StartupPath + tmpCheck.Tag.ToString()))
                {
                    //       GridRow.Source = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + tmpCheck.Tag.ToString(), UriKind.Relative));
                    GridRow.Background = new ImageBrush(new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + tmpCheck.Tag.ToString(), UriKind.Relative)));
                }



            }
            catch { }

        }

        private void modChar1_MouseLeave(object sender, MouseEventArgs e)
        {
            //  GridRow.Source = null;
            GridRow.Background = null;
        }
        /*
           {
"ToolTipImg": "/mod/电弧闪电传送·红.preview",
"NewMOD": ["config/MOD/Arc2Red/newEffects/Art"],
"OldMOD": ["config/MOD/Arc2Red/restoreDefault/Art"]
}    
           */
        private void modChar1_MouseEnterCheck(object sender, MouseEventArgs e)
        {

            try
            {
                CheckBox tmpCheck = sender as CheckBox;
                if (tmpCheck.Tag != null && !tmpCheck.Tag.ToString().Equals(""))

                {

                    JObject jobtag = JObject.Parse(tmpCheck.Tag.ToString());
                    if (jobtag != null&& jobtag.GetValue("ToolTipImg") !=null) {
                        string ToolTipImg = jobtag.GetValue("ToolTipImg").ToString();
                       
                        //       GridRow.Source = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + tmpCheck.Tag.ToString(), UriKind.Relative));
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + ToolTipImg))
                        {
                            GridRow.Background = new ImageBrush(new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + ToolTipImg, UriKind.Relative)));

                        }
                       
                    }
                   
                }



            }
            catch { }

        }

        private void modChar1_MouseLeaveCheck(object sender, MouseEventArgs e)
        {
            //  GridRow.Source = null;
            GridRow.Background = null;
        }

    }
}