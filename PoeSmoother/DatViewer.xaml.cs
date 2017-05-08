using LibDat;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PoeSmoother.Properties;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using LibGGPK;
using System.Collections;
using LibDat.Files;
using System.Globalization;

namespace PoeSmoother
{
    /// <summary>
    /// Interaction logic for DatViewer.xaml
    /// </summary>
    public partial class DatViewer : UserControl
    {
        private DatWrapper data;
        public string FileName { get; set; }
        public List<UnicodeString> DataStrings
        {
            get
            {
                if (data == null) return new List<UnicodeString>();
                return data.Strings;
            }
        }

        public System.Collections.IEnumerable Entries
        {
            get
            {
                return data.Entries;
            }
        }

        public DatViewer(string filename, Stream inStream)
        {
            this.FileName = filename;
            data = new DatWrapper(inStream, filename);
            InitializeComponent();
            DataContext = this;

            //buttonSave.Content = Settings.Strings["DatViewer_Button_Save"];
            buttonExportCSV.Content = Settings.Strings["DatViewer_Button_Export"];
        }

        public DatViewer(string filename)
        {
            this.FileName = filename;
            data = new DatWrapper(filename);
            InitializeComponent();
            DataContext = this;

            //buttonSave.Content = Settings.Strings["DatViewer_Button_Save"];
            buttonExportCSV.Content = Settings.Strings["DatViewer_Button_Export"];
        }

        public DatViewer()
        {
            InitializeComponent();
            DataContext = this;

            //buttonSave.Content = Settings.Strings["DatViewer_Button_Save"];
            buttonExportCSV.Content = Settings.Strings["DatViewer_Button_ExportCSV"];
        }

        public void Reset(string filename, Stream inStream)
        {
            this.FileName = filename;
            data = new DatWrapper(inStream, filename);
            DataContext = null;
            dataGridEntries.ItemsSource = null;
            dataGridEntries.Columns.Clear();

            // 
            if (data.Entries.Count <= 0)
            {
                return;
            }
            foreach (BaseDat bd in data.Entries)
            {
                bd.ResolveReferences();
            }
            BuildGrid(data.Entries[0].GetType());

            DataContext = this;
        }

        private void SaveDat()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".dat";
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + "_NEW.dat";

            if (sfd.ShowDialog() == true)
            {
                data.Save(sfd.FileName);
            }
        }

        private void ExportCSVOld()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".csv";

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, data.Dat.GetCSV());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportCSV()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".csv";
            StringBuilder sb = new StringBuilder(@"");

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<BaseDat> maps = new List<BaseDat>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as BaseDat);
                            maps.Add(one);
                        }
                        Type datType = maps[0].GetType();
                        foreach (var propInfo in datType.GetProperties())
                        {
                            if (propInfo.GetCustomAttributes(false).Any(n => n is Hidden)) continue;
                            DataGridTextColumn col1 = new DataGridTextColumn();
                            string col1Header = propInfo.Name
                                .Replace("StringData", "")
                                .Replace("ListData", "")
                                .Replace("ListRef", "");
                            if (col1Header.EndsWith("Ref")) col1Header = col1Header.Substring(0, col1Header.Length - 3);
                            if (propInfo.PropertyType == typeof(bool)) col1Header += "\n[Bool]";
                            else if (propInfo.PropertyType == typeof(Int64) || propInfo.PropertyType == typeof(UInt64)) col1Header += "\n[Int64]";
                            else if (propInfo.PropertyType == typeof(Int32) || propInfo.PropertyType == typeof(UInt32)) col1Header += "\n[Int32]";
                            else if (propInfo.PropertyType == typeof(UInt64List)) col1Header += "\n[ListInt64]";
                            else if (propInfo.PropertyType == typeof(Int32List) || propInfo.PropertyType == typeof(UInt32List)) col1Header += "\n[ListInt32]";
                            else if (propInfo.PropertyType == typeof(IndirectStringList)) col1Header += "\n[ListStr]";
                            else if (propInfo.PropertyType == typeof(UnicodeString)) col1Header += "\n[Str]";
                            else if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReferenceList)) col1Header += "\n[ListRef to " + propInfo.PropertyType.GetGenericArguments()[0].Name + "]";
                            else if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReference)) col1Header += "\n[Ref to " + propInfo.PropertyType.Name + "]";
                            sb.Append("\"").Append(col1Header).Append("\"").Append(";");
                        }
                        sb.AppendLine();
                        foreach (BaseDat entry in maps)
                        {
                            foreach (var propInfo in datType.GetProperties())
                            {
                                if (propInfo.GetCustomAttributes(false).Any(n => n is Hidden)) continue;
                                DataGridTextColumn col1 = new DataGridTextColumn();
                                string col1Header = "";
                                object val = propInfo.GetValue(entry, null);
                                if (val != null) col1Header = val.ToString();
                                if (col1Header.Contains("\""))
                                {
                                    col1Header = col1Header.Replace("\"", "\"\"");
                                }
                                sb.Append("\"").Append(col1Header).Append("\"").Append(";");
                            }
                            sb.AppendLine();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWiki()
        {
            ExportWikiSkills();
        }

        #region Hidden ExportWiki
        private void ExportWikiDescent2Reward()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"{| class=""wikitable""
|-
! Area !! Marauder Reward !! Templar Reward !! Witch Reward !! Shadow Reward !! Ranger Reward !! Duelist Reward !! Scion Reward
|-
");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<DescentRewardChests> ldrc = new List<DescentRewardChests>();
                        foreach (var item in itemsSource)
                        {
                            ldrc.Add(item as DescentRewardChests);
                        }
                        foreach (DescentRewardChests drc in ldrc.OrderBy(x => x.AreaKey))
                        {
                            if (!drc.CodeStringData.ToString().StartsWith("Descent2_")) continue;
                            sb.AppendLine("| ").AppendLine(drc.AreaRef.ToStringWiki()).Append(":(Level ").Append(drc.AreaRef.MonsterLevel.ToString()).Append(")").AppendLine();
                            sb.AppendLine("| ").Append(drc.Marauder1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Templar1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Witch1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Shadow1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Ranger1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Duelist1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Scion1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("|-");
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiDescent1Reward()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"{| class=""wikitable""
|-
! Area !! Chest !! Marauder Reward !! Templar Reward !! Witch Reward !! Shadow Reward !! Ranger Reward !! Duelist Reward !! Scion Reward
|-
");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<DescentRewardChests> ldrc = new List<DescentRewardChests>();
                        foreach (var item in itemsSource)
                        {
                            ldrc.Add(item as DescentRewardChests);
                        }
                        foreach (DescentRewardChests drc in ldrc.OrderBy(x => x.AreaKey))
                        {
                            if (drc.CodeStringData.ToString().StartsWith("Descent2_")) continue;
                            sb.AppendLine(@"| rowspan=""2"" | ").AppendLine(drc.AreaRef.ToStringWiki()).Append(":(Level ").Append(drc.AreaRef.MonsterLevel.ToString()).Append(")").AppendLine();
                            sb.AppendLine("| ").Append("Curious").AppendLine();
                            sb.AppendLine("| ").Append(drc.Marauder1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Templar1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Witch1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Shadow1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Ranger1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Duelist1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Scion1ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("|-");
                            sb.AppendLine("| ").Append("Aluring").AppendLine();
                            sb.AppendLine("| ").Append(drc.Marauder2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Templar2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Witch2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Shadow2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Ranger2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Duelist2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("| ").Append(drc.Scion2ListRef.ToStringWiki()).AppendLine();
                            sb.AppendLine("|-");
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiDescent1Starter()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"{| class=""wikitable""
|-
! Area !! Marauder !! Templar !! Witch !! Shadow !! Ranger !! Duelist !! Scion
|-
");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<DescentStarterChest> li = new List<DescentStarterChest>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as DescentStarterChest);
                            if (one.AreaKey == 300) continue;
                            li.Add(one);
                        }
                        sb.AppendLine("| ").AppendLine(li[0].AreaRef.ToStringWiki()).Append(":(Level ").Append(li[0].AreaRef.MonsterLevel.ToString()).Append(")").AppendLine();
                        foreach (long prof in new long[] { 0, 6, 1, 5, 3, 4, 2 })
                        {
                            sb.AppendLine("| ");
                            foreach (DescentStarterChest drc in li.Where(x => x.ClassKey == prof).OrderBy(x => x.ItemRef.InheritsFromStringData.ToString()))
                            {
                                sb.Append(": ").Append(drc.ItemRef.ToStringWiki());
                                if (!string.IsNullOrWhiteSpace(drc.SocketsStringData.ToString()))
                                {
                                    sb.Append("\n:*(Socket: ").Append(drc.SocketsStringData.ToString()).Append(")");
                                }
                                sb.AppendLine();
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiDescent2Starter()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"{| class=""wikitable""
|-
! Area !! Marauder !! Templar !! Witch !! Shadow !! Ranger !! Duelist !! Scion
|-
");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<DescentStarterChest> li = new List<DescentStarterChest>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as DescentStarterChest);
                            if (one.AreaKey == 285) continue;
                            li.Add(one);
                        }
                        sb.AppendLine("| ").AppendLine(li[0].AreaRef.ToStringWiki()).Append(":(Level ").Append(li[0].AreaRef.MonsterLevel.ToString()).Append(")").AppendLine();
                        foreach (long prof in new long[] { 0, 6, 1, 5, 3, 4, 2 })
                        {
                            sb.AppendLine("| ");
                            foreach (DescentStarterChest drc in li.Where(x => x.ClassKey == prof).OrderBy(x => x.ItemRef.InheritsFromStringData.ToString()))
                            {
                                sb.Append(": ").Append(drc.ItemRef.ToStringWiki());
                                if (!string.IsNullOrWhiteSpace(drc.SocketsStringData.ToString()))
                                {
                                    sb.Append("\n:*(Socket: ").Append(drc.SocketsStringData.ToString()).Append(")");
                                }
                                sb.AppendLine();
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiQuestRewards()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder();
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<QuestRewards> li = new List<QuestRewards>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as QuestRewards);
                            //if (one.AreaKey == 300) continue;
                            li.Add(one);
                        }
                        int act = 0;
                        foreach (long difficultyId in new int[] { 1, 2, 3 })
                        {
                            sb.Append("=== ").Append(difficultyId == 1 ? "Normal" : difficultyId == 2 ? "Cruel" : "Merciless").AppendLine(" ===");
                            sb.AppendLine(@"{| class=""wikitable"" width=""100%""").AppendLine("|-");
                            foreach (BaseDat questId in ReferenceManager.Instance.AllDats["Quest.dat"])
                            {
                                // ignore test quests
                                if (String.IsNullOrWhiteSpace((questId as Quest).TitleStringData.ToString())) continue;
                                // ignore repetition of Victario's Secrets
                                if (new string[] { "a3q11b", "a3q11c" }.Contains((questId as Quest).CodeStringData.ToString())) continue;
                                var myCount = li.Where(x => x.DifficultyKey == difficultyId && x.QuestKey == (questId as Quest).Key);
                                // ignore quests with no reward
                                if (myCount.Count() == 0) continue;
                                // repeat header after each act
                                if ((questId as Quest).Act != act) sb.AppendLine(@"! Quest !! Marauder !! Templar !! Witch !! Shadow !! Ranger !! Duelist !! Scion").AppendLine("|-");
                                act = (questId as Quest).Act;
                                sb.AppendLine(@"! scope=""row"" | ").AppendLine((questId as Quest).ToStringWiki()).Append("<br/>Act ").Append((questId as Quest).Act.ToString()).Append("<br/>")
                                    .Append(difficultyId == 1 ? "Normal" : difficultyId == 2 ? "Cruel" : "Merciless").Append("").AppendLine();
                                StringBuilder[] allSbs = new StringBuilder[7];
                                foreach (long prof in new long[] { 0, 6, 1, 5, 3, 4, 2 })
                                {
                                    allSbs[prof] = new StringBuilder();
                                    allSbs[prof].AppendLine("| ");
                                    foreach (QuestRewards drc in li.Where(x => (x.CharacterKey == prof || x.CharacterKey < 0) && x.DifficultyKey == difficultyId && x.QuestKey == (questId as Quest).Key)
                                        .OrderBy(x => x.ItemRef.InheritsFromStringData.ToString().Contains("Metadata/Items/Gems") ? (ReferenceManager.Instance.AllDats["ItemVisualIdentity.dat"].Where(y => y.Key == x.ItemRef.VisualIdentityKey).FirstOrDefault() as ItemVisualIdentity).AnimatedObjectStringData.ToString().Replace("Str", "Aaa") : "-1").ThenBy(z => z.ItemRef.NameStringData.ToString()))
                                    {
                                        allSbs[prof].Append("");
                                        if (drc.ItemRef.ItemTypeStringData.ToString().Contains("/SkillBooks/"))
                                        {
                                            allSbs[prof].Append("{{il|").Append(drc.ItemRef.NameStringData.ToString()).Append(" (").Append((questId as Quest).TitleStringData.ToString())
                                                .Append(")|").Append(drc.ItemRef.NameStringData.ToString()).AppendLine("}}").Append(": ");
                                            //Extracted from Metadata : todo, read metadata directly
                                            switch (drc.ItemRef.ItemTypeStringData.ToString())
                                            {
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a3q11v2":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point and two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q6":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q7":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q8":
                                                    allSbs[prof].AppendLine("Grants two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q9":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a2q5":
                                                    allSbs[prof].AppendLine("Grants two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a3q9":
                                                    allSbs[prof].AppendLine("Grants two Passive Skill Points");
                                                    break;
                                                default:
                                                    allSbs[prof].AppendLine();
                                                    break;
                                            }
                                        }
                                        else
                                        {                                            
                                            if (drc.ItemLevel > 1) allSbs[prof].Append(drc.ItemRef.ToStringWiki()).Append(" (iLvl ").Append(drc.ItemLevel.ToString()).Append(", ")
                                                .Append(drc.ItemRarity == 1 ? @"<span style=""color:#C8C8C8;"">Normal</span>" : drc.ItemRarity == 2 ? @"<span style=""color:#8888FF;"">Magic</span>" : @"<span style=""color:#FFFF77;"">Rare</span>")
                                                .Append(")");
                                            else
                                                allSbs[prof].Append(drc.ItemRef.ToStringWiki());
                                            allSbs[prof].AppendLine("<br/>");
                                        }
                                    }
                                }
                                if ((allSbs[0].ToString() == allSbs[1].ToString())
                                    && (allSbs[0].ToString() == allSbs[2].ToString())
                                    && (allSbs[0].ToString() == allSbs[3].ToString())
                                    && (allSbs[0].ToString() == allSbs[4].ToString())
                                    && (allSbs[0].ToString() == allSbs[5].ToString())
                                    && (allSbs[0].ToString() == allSbs[6].ToString()))
                                {
                                    sb.Append(@"| colspan=""7"" align=""center"" ").Append(allSbs[0].ToString());
                                    if ((questId as Quest).CodeStringData.ToString() == "a2q7")
                                    {
                                        //Deal with the bandits
                                    }
                                }
                                else
                                {
                                    sb.Append(allSbs[0].ToString()).Append(allSbs[6].ToString()).Append(allSbs[1].ToString()).Append(allSbs[5].ToString())
                                        .Append(allSbs[3].ToString()).Append(allSbs[4].ToString()).Append(allSbs[2].ToString());
                                }
                                sb.AppendLine("|-");
                            }
                            sb.AppendLine("|}").AppendLine();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiQuestRewardGems()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"{| class=""wikitable""
|-
");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<QuestRewards> liqr = new List<QuestRewards>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as QuestRewards);
                            liqr.Add(one);
                        }
                        List<BaseItemTypes> libit = new List<BaseItemTypes>();
                        foreach (var item in ReferenceManager.Instance.AllDats["BaseItemTypes.dat"])
                        {
                            var one = (item as BaseItemTypes);
                            libit.Add(one);
                        }
                        int lineCount = 0;
                        foreach (BaseItemTypes gemId in libit.Where(y => y.InheritsFromStringData.ToString().Contains("/Gems/")).OrderBy(x => x.NameStringData.ToString()))
                        {
                            if (liqr.Where(x => x.ItemRef.Key == gemId.Key).Count() == 0) continue;
                            // repeat header every 5 lines
                            if (lineCount % 5 == 0) sb.AppendLine(@"! width=""10%"" | Gem !! Marauder !! Templar !! Witch !! Shadow !! Ranger !! Duelist !! Scion").AppendLine("|-");
                            sb.AppendLine(@"! scope=""row"" | ").AppendLine(gemId.ToStringWiki());
                            StringBuilder[] allSbs = new StringBuilder[7];
                            foreach (long prof in new long[] { 0, 6, 1, 5, 3, 4, 2 })
                            {
                                allSbs[prof] = new StringBuilder();
                                allSbs[prof].AppendLine("| ");
                                foreach (QuestRewards drc in liqr.Where(x => (x.CharacterKey == prof || x.CharacterKey < 0) && x.ItemRef.Key == gemId.Key)
                                    .OrderBy(o1 => o1.DifficultyKey).ThenBy(o2 => o2.QuestRef.CodeStringData.ToString()))
                                {
                                    allSbs[prof].Append("");
                                    allSbs[prof].Append(drc.DifficultyKey == 1 ? "N" : drc.DifficultyKey == 2 ? "C" : "M").Append(":A").Append(drc.QuestRef.Act.ToString())
                                .Append(" - ").Append(drc.QuestRef.ToStringWiki()).AppendLine("<br/>");
                                }
                            }
                            if ((allSbs[0].ToString() == allSbs[1].ToString())
                                && (allSbs[0].ToString() == allSbs[2].ToString())
                                && (allSbs[0].ToString() == allSbs[3].ToString())
                                && (allSbs[0].ToString() == allSbs[4].ToString())
                                && (allSbs[0].ToString() == allSbs[5].ToString())
                                && (allSbs[0].ToString() == allSbs[6].ToString()))
                            {
                                sb.Append(@"| colspan=""7"" align=""center"" valign=""top"" ").Append(allSbs[0].ToString());
                            }
                            else
                            {
                                sb.Append(@"| valign=""top"" ").Append(allSbs[0].ToString()).Append(@"| valign=""top"" ").Append(allSbs[6].ToString())
                                    .Append(@"| valign=""top"" ").Append(allSbs[1].ToString()).Append(@"| valign=""top"" ").Append(allSbs[5].ToString())
                                    .Append(@"| valign=""top"" ").Append(allSbs[3].ToString()).Append(@"| valign=""top"" ").Append(allSbs[4].ToString())
                                    .Append(@"| valign=""top"" ").Append(allSbs[2].ToString());
                            }
                            lineCount++;
                            sb.AppendLine("|-");
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiMonsterXp()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<WorldAreas> gameAreas = new List<WorldAreas>();
                        foreach (var item in ReferenceManager.Instance.AllDats["WorldAreas.dat"])
                        {
                            var one = (item as WorldAreas);
                            gameAreas.Add(one);
                        }
                        gameAreas.Sort();
                        int lineCount = 0;
                        string oldDifficulty = "0";
                        foreach (WorldAreas gameArea in gameAreas)
                        {
                            if (!(gameArea.CodeStringData.ToString().StartsWith("1_") || gameArea.CodeStringData.ToString().StartsWith("2_") || gameArea.CodeStringData.ToString().StartsWith("3_")))
                                continue;
                            List<MonsterPacks> packsInArea = new List<MonsterPacks>();
                            List<Int64> monstersInAreaKey = new List<Int64>();
                            foreach (var item in ReferenceManager.Instance.AllDats["MonsterPacks.dat"])
                            {
                                var one = (item as MonsterPacks);
                                string areaName = gameAreas.Where(x=>x.Key == one.AreaKey).FirstOrDefault().LabelStringData.ToString();
                                if (areaName == "The Eternal Laboratory") continue;
                                if (areaName == gameArea.LabelStringData.ToString())
                                {
                                    packsInArea.Add(one);
                                    foreach (var item2 in ReferenceManager.Instance.AllDats["MonsterPackEntries.dat"])
                                    {
                                        var one2 = (item2 as MonsterPackEntries);
                                        if ((one2.PackKey == one.Key) && (!monstersInAreaKey.Contains(one2.VarietyKey)))
                                        {
                                            monstersInAreaKey.Add(one2.VarietyKey);
                                        }
                                    }
                                }
                            }
                            if (monstersInAreaKey.Count == 0) continue; // probably a town
                            string newDifficulty = gameArea.CodeStringData.ToString().Split('_').First();
                            if (newDifficulty != oldDifficulty)
                            {
                                if (oldDifficulty != "0") sb.AppendLine("|}");
                                sb.Append("== ").Append(newDifficulty == "1" ? "Normal" : newDifficulty == "2" ? "Cruel" : "Merciless").AppendLine(" ==");
                                sb.AppendLine(@"{| class=""wikitable""").AppendLine("|-");
                                oldDifficulty = newDifficulty;
                                lineCount = 0;
                            }
                            if (lineCount % 5 == 0) sb.AppendLine(@"! Area !! Level !! Monster !! colspan=""3"" | Xp !! Resist<br/>Fire !! Resist<br/>Cold !! Resist<br/>Lightning !! Resist<br/>Chaos").AppendLine("|-");
                            lineCount++;
                            List<MonsterVarieties> monstersInArea = new List<MonsterVarieties>();
                            foreach (var item in ReferenceManager.Instance.AllDats["MonsterVarieties.dat"])
                            {
                                var one = (item as MonsterVarieties);
                                if (gameArea.MapbossListData.Data.Contains(one.Key)) monstersInArea.Add(one);
                                if (!monstersInAreaKey.Contains(one.Key)) continue;
                                if (monstersInArea.Where(y => y.NameStringData.ToString() == one.NameStringData.ToString()).Where(z => z.XpMultiplier == one.XpMultiplier).Count() > 0) continue;
                                monstersInArea.Add(one);
                            }
                            sb.Append(@"! scope=""row"" rowspan=""").Append(monstersInArea.Count).Append(@""" | ").AppendLine(gameArea.ToStringWiki());
                            //sb.Append(@"| rowspan=""").Append(monstersInArea.Count).Append(@""" | ");
                            //sb.AppendLine(gameArea.Difficulty == 1 ? "Normal" : gameArea.Difficulty == 2 ? "Cruel" : "Merciless");
                            sb.Append(@"| rowspan=""").Append(monstersInArea.Count).Append(@""" | ").AppendLine(gameArea.MonsterLevel.ToString());
                            foreach (var monsterInArea in monstersInArea.OrderBy(y => gameArea.MapbossListData.Data.Contains(y.Key)).ThenBy(x => x.NameStringData.ToString()))
                            {
                                sb.Append(@"| ").AppendLine(monsterInArea.ToStringWiki());
                                string metadata = "";
                                if (monstersInArea.Where(y => y.NameStringData.ToString() == monsterInArea.NameStringData.ToString()).Count() > 1)
                                {
                                    metadata = monsterInArea.MonsterMetadataStringData.ToString().Split('/').Last();
                                }
                                //sb.Append(@"| ").AppendLine(metadata);
                                long defaultXp = 0;
                                long defaultBaseResist = 0;
                                foreach (var item in ReferenceManager.Instance.AllDats["DefaultMonsterStats.dat"])
                                {
                                    var one = (item as DefaultMonsterStats);
                                    if (one.LevelStringData.ToString() == gameArea.MonsterLevel.ToString())
                                    {
                                        defaultXp = one.Xp;
                                        defaultBaseResist = one.BaseResist;
                                    }
                                }
                                long modifiedMonster = defaultXp * monsterInArea.XpMultiplier / 100;
                                MonsterTypes thisMonsterType = ReferenceManager.Instance.AllDats["MonsterTypes.dat"].Where(x => x.Key == monsterInArea.MonsterTypesKey).FirstOrDefault() as MonsterTypes;
                                if (gameArea.MapbossListData.Data.Contains(monsterInArea.Key))
                                {
                                    sb.Append(@"| colspan=""3"" align=""right"" | ").Append(@"<span style=""color:#AF6025;"">").Append(modifiedMonster * 550 / 100).Append("</span>").AppendLine();
                                }
                                else
                                {
                                    sb.Append(@"| align=""right"" | ").Append(@"<span style=""color:#C8C8C8;"">").Append(modifiedMonster * 100 / 100).Append("</span>").AppendLine();
                                    sb.Append(@"| align=""right"" | ").Append(@"<span style=""color:#8888FF;"">").Append(modifiedMonster * 350 / 100).Append("</span>").AppendLine();
                                    sb.Append(@"| align=""right"" | ").Append(@"<span style=""color:#FFFF77;"">").Append(modifiedMonster * 550 / 100).Append("</span>").AppendLine();
                                }
                                sb.Append(@"| align=""right"" | ").Append(defaultBaseResist * thisMonsterType.ResistFire / 100).AppendLine();
                                sb.Append(@"| align=""right"" | ").Append(defaultBaseResist * thisMonsterType.ResistCold / 100).AppendLine();
                                sb.Append(@"| align=""right"" | ").Append(defaultBaseResist * thisMonsterType.ResistLightning / 100).AppendLine();
                                sb.Append(@"| align=""right"" | ").Append(defaultBaseResist * thisMonsterType.ResistChaos / 100).AppendLine();
                                sb.AppendLine("|-");
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiMapGraphviz()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<Maps> maps = new List<Maps>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as Maps);
                            maps.Add(one);
                        }
                        foreach (Maps map in maps)
                        {
                            sb.Append(map.ItemKey).Append(" [label=\"")
                                .Append(map.AreaNormalRef.LabelStringData.ToString())
                                //.Append(" (\"").Append(
                                .Append("\"];").AppendLine();
                            sb.Append(map.ItemKey).Append(" -> ").Append(map.UpgradesToKey).Append(";").AppendLine();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiRewardsByQuest()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder();
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<QuestRewards> li = new List<QuestRewards>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as QuestRewards);
                            //if (one.AreaKey == 300) continue;
                            li.Add(one);
                        }
                        int act = 0;
                        foreach (BaseDat questId in ReferenceManager.Instance.AllDats["Quest.dat"])
                        {
                            sb.AppendLine().AppendLine(@"--- ").AppendLine((questId as Quest).ToStringWiki()).Append("").AppendLine(" ---")
                                    .AppendLine("").AppendLine(); 
                            foreach (long difficultyId in new int[] { 1, 2, 3 })
                            {
                                // ignore test quests
                                if (String.IsNullOrWhiteSpace((questId as Quest).TitleStringData.ToString())) continue;
                                // ignore repetition of Victario's Secrets
                                if (new string[] { "a3q11b", "a3q11c" }.Contains((questId as Quest).CodeStringData.ToString())) continue;
                                var myCount = li.Where(x => x.DifficultyKey == difficultyId && x.QuestKey == (questId as Quest).Key);
                                // ignore quests with no reward
                                if (myCount.Count() == 0) continue;
                                // repeat header after each act
                                sb.Append("=== ").Append(difficultyId == 1 ? "Normal" : difficultyId == 2 ? "Cruel" : "Merciless").AppendLine(" ===");
                                sb.AppendLine(@"{| class=""wikitable"" width=""100%""").AppendLine("|-");
                                if (true) sb.AppendLine(@"! width=""14%"" | Marauder !! width=""14%"" | Templar !! width=""14%"" | Witch !! width=""14%"" | Shadow !! width=""14%"" | Ranger !! width=""14%"" | Duelist !! width=""14%"" | Scion").AppendLine("|-");
                                act = (questId as Quest).Act;
                                StringBuilder[] allSbs = new StringBuilder[7];
                                foreach (long prof in new long[] { 0, 6, 1, 5, 3, 4, 2 })
                                {
                                    allSbs[prof] = new StringBuilder();
                                    allSbs[prof].AppendLine("| ");
                                    foreach (QuestRewards drc in li.Where(x => (x.CharacterKey == prof || x.CharacterKey < 0) && x.DifficultyKey == difficultyId && x.QuestKey == (questId as Quest).Key)
                                        .OrderBy(x => x.ItemRef.InheritsFromStringData.ToString().Contains("Metadata/Items/Gems") ? (ReferenceManager.Instance.AllDats["ItemVisualIdentity.dat"].Where(y => y.Key == x.ItemRef.VisualIdentityKey).FirstOrDefault() as ItemVisualIdentity).AnimatedObjectStringData.ToString().Replace("Str", "Aaa") : "-1").ThenBy(z => z.ItemRef.NameStringData.ToString()))
                                    {
                                        allSbs[prof].Append("");
                                        if (drc.ItemRef.ItemTypeStringData.ToString().Contains("/SkillBooks/"))
                                        {
                                            allSbs[prof].Append("{{il|").Append(drc.ItemRef.NameStringData.ToString()).Append(" (").Append((questId as Quest).TitleStringData.ToString())
                                                .Append(")|").Append(drc.ItemRef.NameStringData.ToString()).AppendLine("}}").Append(": ");
                                            //Extracted from Metadata : todo, read metadata directly
                                            switch (drc.ItemRef.ItemTypeStringData.ToString())
                                            {
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a3q11v2":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point and two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q6":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q7":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q8":
                                                    allSbs[prof].AppendLine("Grants two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a1q9":
                                                    allSbs[prof].AppendLine("Grants a Passive Skill Point");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a2q5":
                                                    allSbs[prof].AppendLine("Grants two Passive Respec Points");
                                                    break;
                                                case "Metadata/Items/QuestItems/SkillBooks/Book-a3q9":
                                                    allSbs[prof].AppendLine("Grants two Passive Skill Points");
                                                    break;
                                                default:
                                                    allSbs[prof].AppendLine();
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            if (drc.ItemLevel > 1) allSbs[prof].Append(drc.ItemRef.ToStringWiki()).Append(" (iLvl ").Append(drc.ItemLevel.ToString()).Append(", ")
                                                .Append(drc.ItemRarity == 1 ? @"<span style=""color:#C8C8C8;"">Normal</span>" : drc.ItemRarity == 2 ? @"<span style=""color:#8888FF;"">Magic</span>" : @"<span style=""color:#FFFF77;"">Rare</span>")
                                                .Append(")");
                                            else
                                                allSbs[prof].Append(drc.ItemRef.ToStringWiki());
                                            allSbs[prof].AppendLine("<br/>");
                                        }
                                    }
                                }
                                if ((allSbs[0].ToString() == allSbs[1].ToString())
                                    && (allSbs[0].ToString() == allSbs[2].ToString())
                                    && (allSbs[0].ToString() == allSbs[3].ToString())
                                    && (allSbs[0].ToString() == allSbs[4].ToString())
                                    && (allSbs[0].ToString() == allSbs[5].ToString())
                                    && (allSbs[0].ToString() == allSbs[6].ToString()))
                                {
                                    sb.Append(@"| colspan=""7"" align=""center"" ").Append(allSbs[0].ToString());
                                    if ((questId as Quest).CodeStringData.ToString() == "a2q7")
                                    {
                                        //Deal with the bandits
                                    }
                                }
                                else
                                {
                                    sb.Append(allSbs[0].ToString()).Append(allSbs[6].ToString()).Append(allSbs[1].ToString()).Append(allSbs[5].ToString())
                                        .Append(allSbs[3].ToString()).Append(allSbs[4].ToString()).Append(allSbs[2].ToString());
                                }
                                sb.AppendLine("|}").AppendLine();
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiGuildTags()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<Maps> maps = new List<Maps>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as Maps);
                            maps.Add(one);
                        }
                        foreach (Maps map in maps)
                        {
                            sb.Append("| ").Append(map.GuildLetterNormalStringData.ToString()).Append(" || {{il|")
                                .Append(map.ItemRef.NameStringData.ToString())
                                .Append("}}").AppendLine();
                            sb.Append("|-").AppendLine();
                            if (map.AreaUniqueRef != null)
                            {
                                sb.Append("| ").Append(map.GuildLetterUniqueStringData.ToString()).Append(" || {{il|")
                                   .Append(map.AreaUniqueRef.LabelStringData.ToString())
                                   .Append("}}").AppendLine();
                                sb.Append("|-").AppendLine();
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiPassivesList()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<PassiveSkills> maps = new List<PassiveSkills>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as PassiveSkills);
                            maps.Add(one);
                        }
                        string oldName = "-";
                        foreach (PassiveSkills passive in maps.OrderBy(x=>x.NameStringData.ToString()))
                        {
                            if (oldName != passive.NameStringData.ToString())
                            {
                                if (passive.StatsListData.Data[0] == 0) continue;
                                sb.Append(":[[").Append(passive.NameStringData.ToString()).Append("]]")
                                    .Append("").AppendLine();
                            }
                            oldName = passive.NameStringData.ToString();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiNpcDialogs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<NPCTalk> maps = new List<NPCTalk>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as NPCTalk);
                            maps.Add(one);
                        }
                        string oldName = "-";
                        foreach (NPCTalk talkOption in maps
                            .OrderBy(x => x.NPCRef.ToString())
                            .ThenBy(x => x.U12)
                            .ThenBy(x => x.DialogueOptionStringData.ToString())
                            .ThenBy(x => x.DialogPriority))
                        {
                            if (oldName != talkOption.NPCRef.ToString())
                            {
                                sb.AppendLine("|}");
                                sb.AppendLine()
                                    .Append("=== [[").Append(talkOption.NPCRef.NameStringData.ToString()).Append("]] ===")
                                    .Append("").AppendLine().AppendLine();
                                sb.AppendLine(@"{| class=""wikitable"" width=""100%""").AppendLine("|-");
                                sb.AppendLine(@"! width=""10%"" | Dialog option !! Text").AppendLine("|-");
                            }
                            if (talkOption.TextAudioListRef.Data == null || talkOption.TextAudioListRef.Data.Count == 0) continue;
                            sb.Append(@"| ").Append(talkOption.DialogueOptionStringData.ToString())
                                .AppendLine(" || ");
                            string[] characterShortcut = new string[7]
                            {
                                "Marauder",
                                "Witch",
                                "Scion",
                                "Ranger",
                                "Duelist",
                                "Shadow",
                                "Templar",
                            };
                            foreach (var oneTxt in talkOption.TextAudioListRef.Data)
                            {                                
                                NPCTextAudio nta = oneTxt as NPCTextAudio;
                                if (nta.CharacterKey < 0 || nta.CharacterKey > 6)
                                {
                                    sb.Append(Utils.ConvertGggFormatedText(nta.TextStringData.ToString()))
                                        .AppendLine().AppendLine();
                                }
                                else
                                {
                                    sb.AppendLine().AppendLine()
                                        .Append("<b>To ").Append(characterShortcut[nta.CharacterKey])
                                        .AppendLine("</b>").AppendLine()
                                        .Append(Utils.ConvertGggFormatedText(nta.TextStringData.ToString()))
                                        .AppendLine();
                                }
                            }
                            sb.AppendLine("|-");
                            oldName = talkOption.NPCRef.ToString();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportWikiMapMonsters()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "MapMonster.txt";

            StringBuilder sb = new StringBuilder(@"");
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var itemsSource = dataGridEntries.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        List<WorldAreas> maps = new List<WorldAreas>();
                        foreach (var item in ReferenceManager.Instance.AllDats["WorldAreas.dat"])
                        {
                            var one = (item as WorldAreas);
                            maps.Add(one);
                        }
                        List<MonsterVarieties> allMonsters = new List<MonsterVarieties>();
                        foreach (var item in itemsSource)
                        {
                            var one = (item as MonsterVarieties);
                            allMonsters.Add(one);
                        }
                        int lineCount = 0;
                        sb.AppendLine(@"{| class=""wikitable sortable"" width=""100%""").AppendLine("|-");
                        foreach (WorldAreas area in maps.OrderBy(x=>x.MonsterLevel).ThenBy(x => x.CodeStringData.ToString()))
                        {
                            if (!area.IsMap) continue;
                            if (area.Key == 212) continue; //Untainted Paradise
                            if (area.Key == 275) continue; //Wraeclast Pantheon
                            if (area.Key == 227) continue; //Mao Kun
                            if (area.Key == 251) continue; //Blackguard Salute
                            if (area.Key == 253) continue; //Oba's Map
                            List<ulong> missingBosses = area.MapbossListData.Data;
                            if (area.Key == 205) missingBosses.Add(578); //The Coward's Trial
                            foreach (ulong oneBossId in area.MapbossListData.Data)
                            {
                                if (lineCount == 0) sb.AppendLine(@"! Map !! Level !! Boss !! Type !! Same appearance !! Similar appearance !! Same skills !! Similar skills ").AppendLine("|-");
                                lineCount++;
                                MonsterVarieties boss = allMonsters.Where(x => x.Key == oneBossId).FirstOrDefault();
                                sb.Append(@"| {{il|").Append(area.LabelStringData.ToString()).Append(area.CodeStringData.ToString().Contains("Unique") ? "" : " Map").AppendLine(@"}}");
                                sb.Append(@"| ").Append(area.MonsterLevel.ToString()).AppendLine(@"");
                                sb.Append(@"| ").Append(boss.ToStringWiki()).AppendLine(@"");
                                sb.Append(@"| ").Append(boss.MonsterTypesRef.ToStringWiki()).AppendLine(@"");
                                sb.Append(@"| ");
                                IEnumerable<MonsterVarieties> sameSkins = allMonsters.Where(x =>
                                    x.AnimatedObjectStringData.ToString() == boss.AnimatedObjectStringData.ToString()
                                    && x.MainHandListRef.ToString() == boss.MainHandListRef.ToString()
                                    && x.OffHandListRef.ToString() == boss.OffHandListRef.ToString()
                                    && x.QuiverKey == boss.QuiverKey
                                    && !x.NameStringData.ToString().Contains("@")
                                    && x.NameStringData.ToString() != boss.NameStringData.ToString());
                                if (sameSkins.Count() == 0) sb.Append("{{n/a}}");
                                else if (sameSkins.Count() > 10) sb.Append("More than 10 monsters");
                                else
                                {
                                    foreach (string sameSkin in sameSkins.Select(x => x.ToStringWiki()).Distinct())
                                    {
                                        sb.Append(sameSkin).Append("<br/>");
                                    }
                                }
                                sb.AppendLine(@"");
                                sb.Append(@"| ");
                                IEnumerable<MonsterVarieties> similarSkins = allMonsters.Where(x =>
                                    x.ActorStringData.ToString() == boss.ActorStringData.ToString()
                                    && x.NameStringData.ToString() != boss.NameStringData.ToString()
                                    && !x.NameStringData.ToString().Contains("@")
                                    && !sameSkins.Select(y => y.NameStringData.ToString()).Contains(x.NameStringData.ToString()));
                                if (similarSkins.Count() == 0) sb.Append("{{n/a}}");
                                else if (similarSkins.Count() > 10) sb.Append("More than 10 monsters");
                                else
                                {
                                    foreach (string similarSkin in similarSkins.Select(x => x.ToStringWiki()).Distinct())
                                    {
                                        sb.Append(similarSkin).Append("<br/>");
                                    }
                                }
                                sb.AppendLine(@"");
                                sb.Append(@"| ");
                                IEnumerable<MonsterVarieties> sameSkills = allMonsters.Where(x =>
                                    x.EffectsListRef.ToString() == boss.EffectsListRef.ToString()
                                    && x.ModsListRef.ToStringNoKey().Replace("MonsterMapBoss\n", "") == boss.ModsListRef.ToStringNoKey().Replace("MonsterMapBoss\n", "")
                                    && !x.NameStringData.ToString().Contains("@")
                                    && x.NameStringData.ToString() != boss.NameStringData.ToString());
                                if (sameSkills.Count() == 0) sb.Append("{{n/a}}");
                                else if (sameSkills.Count() > 10) sb.Append("More than 10 monsters");
                                else
                                {
                                    foreach (string sameSkill in sameSkills.Select(x => x.ToStringWiki()).Distinct())
                                    {
                                        sb.Append(sameSkill).Append("<br/>");
                                    }
                                }
                                sb.AppendLine(@"");
                                sb.Append(@"| ");
                                IEnumerable<MonsterVarieties> similarSkills = allMonsters.Where(x =>
                                    x.EffectsListRef.ToStringNoKey()
                                        .Replace("Melee\n", "")
                                        .Replace("MapBoss", "")
                                    == boss.EffectsListRef.ToStringNoKey()
                                        .Replace("Melee\n", "")
                                        .Replace("MapBoss", "")
                                    && x.ModsListRef.ToStringNoKey()
                                        .Replace("MonsterMapBoss\n", "")
                                        .Replace("MonsterActBoss\n", "")
                                        .Replace("MonsterDescent2Boss\n", "")
                                        .Replace("MonsterDescent2FinalBoss\n", "")
                                        .Replace("MapMonsterReducedCurseEffect\n", "")
                                        .Replace("MonsterImplicitGainsPowerChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitGainsFrenzyChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitGainsEnduranceChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitIncreasedCastSpeedPerPowerCharge\n", "")
                                        .Replace("MonsterMercilessDrops1\n", "")
                                        .Replace("MonsterMercilessDrops2\n", "")
                                        .Replace("MonsterMercilessDrops3\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupComplete\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupLarge\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupSmall\n", "")
                                        .Replace("MonsterNecromancerRaisable\n", "")
                                    == boss.ModsListRef.ToStringNoKey()
                                        .Replace("MonsterMapBoss\n", "")
                                        .Replace("MonsterActBoss\n", "")
                                        .Replace("MonsterDescent2Boss\n", "")
                                        .Replace("MonsterDescent2FinalBoss\n", "")
                                        .Replace("MapMonsterReducedCurseEffect\n", "")
                                        .Replace("MonsterImplicitGainsPowerChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitGainsFrenzyChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitGainsEnduranceChargeOnHit1\n", "")
                                        .Replace("MonsterImplicitIncreasedCastSpeedPerPowerCharge\n", "")
                                        .Replace("MonsterMercilessDrops1\n", "")
                                        .Replace("MonsterMercilessDrops2\n", "")
                                        .Replace("MonsterMercilessDrops3\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupComplete\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupLarge\n", "")
                                        .Replace("MonsterSpeedAndDamageFixupSmall\n", "")
                                        .Replace("MonsterNecromancerRaisable\n", "")
                                    && x.NameStringData.ToString() != boss.NameStringData.ToString()
                                    && !x.NameStringData.ToString().Contains("@")
                                    && !sameSkills.Select(y => y.NameStringData.ToString()).Contains(x.NameStringData.ToString()));
                                if (similarSkills.Count() == 0) sb.Append("{{n/a}}");
                                else if (similarSkills.Count() > 10) sb.Append("More than 10 monsters");
                                else
                                {
                                    foreach (string similarSkill in similarSkills.Select(x => x.ToStringWiki()).Distinct())
                                    {
                                        sb.Append(similarSkill).Append("<br/>");
                                    }
                                }
                                sb.AppendLine().AppendLine("|-");
                            }
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.AppendLine("|}").ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], sfd.FileName), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        private void ExportWikiSkills()
        {
            try
            {
                List<WorldAreas> gameAreas = new List<WorldAreas>();
                foreach (var item in ReferenceManager.Instance.AllDats["WorldAreas.dat"])
                {
                    var one = (item as WorldAreas);
                    gameAreas.Add(one);
                }
                gameAreas.Sort();
                List<ActiveSkills> activeSkills = new List<ActiveSkills>();
                foreach (var item in ReferenceManager.Instance.AllDats["ActiveSkills.dat"])
                {
                    var one = (item as ActiveSkills);
                    activeSkills.Add(one);
                }
                ActiveSkills activeSkill = activeSkills.Where(x => x.NameStringData.ToString() == "Fireball").Last();


                //---------------------
                //GrantedEffects grantedEffect = ReferenceManager.Instance.AllDats["GrantedEffects.dat"].Where(x => x.Key == activeSkill.GrantedEffectKey).FirstOrDefault() as GrantedEffects;
                IEnumerable<BaseDat> grantedEffectsPerLevel = (dataGridEntries.ItemsSource as IEnumerable<BaseDat>).Where(x => (x as GrantedEffectsPerLevel).GrantedEffectsKey == activeSkill.GrantedEffectKey).OrderBy(y => (y as GrantedEffectsPerLevel).Level);
                SkillGems skillGem = ReferenceManager.Instance.AllDats["SkillGems.dat"].Where(x => (x as SkillGems).EffectKey == activeSkill.GrantedEffectKey).FirstOrDefault() as SkillGems;
                //IEnumerable<BaseDat> itemExperiencePerLevel = ReferenceManager.Instance.AllDats["ItemExperiencePerLevel.dat"].Where(x => (x as ItemExperiencePerLevel).ItemKey == skillGem.ItemKey);
                if (grantedEffectsPerLevel.Count() < 20) return;
                StringBuilder sb = new StringBuilder(activeSkill.NameStringData.ToString()).AppendLine().AppendLine();
                sb.AppendLine(@"{{GemLevelTable");
                if (skillGem.Str != 0) sb.AppendLine(@"| str = yes");
                if (skillGem.Dex != 0) sb.AppendLine(@"| dex = yes");
                if (skillGem.Int != 0) sb.AppendLine(@"| int = yes");
                sb.AppendLine(@"| c1 = Mana<br>Cost");
                int effectCounter = 0;
                foreach (var one in (grantedEffectsPerLevel.Last() as GrantedEffectsPerLevel).StatsListListRef.Data)
                {
                    sb.Append(@"| c").Append((effectCounter + 2).ToString()).Append(@" = {{abbr|").Append((one as Stats).CodeStringData.ToString()).Append(@"|");
                    sb.Append((one as Stats).CodeStringData.ToString());
                    sb.Append("}}").AppendLine();
                    ++effectCounter;
                }
//| c3 = {{abbr|Item<br>Level|Can use Items requiring up to level X}}
//| c4 = {{abbr|Melee Physical<br>Damage|Minions deal x% increased Physical Damage with Melee Attacks}}
                sb.AppendLine(@"}}");
                int previousXp = 0;
                foreach (var one in grantedEffectsPerLevel)
                {
                    GrantedEffectsPerLevel grantedEffectPerLevel = one as GrantedEffectsPerLevel;
                    sb.AppendLine("|-");
                    sb.Append("! ").AppendLine(grantedEffectPerLevel.Level.ToString());
                    // ACTIVE 100 : =2,1*A3+8,2
                    // ACTIVE 60 : =1,3251*A2+5.397
                    // ACTIVE 40 : =0,92*A3+3,9
                    // SUPPORT 100 : =1,5*A3+6
                    // SUPPORT 60 : =0,947*A3+4
                    // SUPPORT 40 : =0,656*A2+3

                    double effectivegeplLevel = (double)grantedEffectPerLevel.RequiredLevel1;
                    if (grantedEffectPerLevel.Level > 20) effectivegeplLevel = (double)((grantedEffectsPerLevel.Where(x=>(x as GrantedEffectsPerLevel).Level == 20).Last() as GrantedEffectsPerLevel).RequiredLevel1);

                    sb.Append("| ").Append(((int)effectivegeplLevel).ToString());

                    #region required attributes
                    if (skillGem.Str == 100)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(2.1 * effectivegeplLevel + 8.2));
                    }
                    else if (skillGem.Str == 60)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(1.3251 * effectivegeplLevel + 5.397));
                    }
                    else if (skillGem.Str == 40)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(0.92 * effectivegeplLevel + 3.9));
                    }
                    if (skillGem.Dex == 100)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(2.1 * effectivegeplLevel + 8.2));
                    }
                    else if (skillGem.Dex == 60)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(1.3251 * effectivegeplLevel + 5.397));
                    }
                    else if (skillGem.Dex == 40)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(0.92 * effectivegeplLevel + 3.9));
                    }
                    if (skillGem.Int == 100)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(2.1 * effectivegeplLevel + 8.2));
                    }
                    else if (skillGem.Int == 60)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(1.3251 * effectivegeplLevel + 5.397));
                    }
                    else if (skillGem.Int == 40)
                    {
                        sb.Append(" || ").Append((int)Math.Floor(0.92 * effectivegeplLevel + 3.9));
                    }
                    #endregion

                    sb.Append(" || ").Append(grantedEffectPerLevel.ManaCost.ToString());
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat1Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat2Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat3Amount);
                    //sb.Append(" || ").Append((Math.Floor(grantedEffectPerLevel.Stat4Amount / 6.0) / 10.0).ToString("N1", CultureInfo.InvariantCulture));
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat4Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat5Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat6Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat7Amount);
                    sb.Append(" || ").Append(grantedEffectPerLevel.Stat8Amount);
                    IEnumerable<BaseDat> gemXp = ReferenceManager.Instance.AllDats["ItemExperiencePerLevel.dat"].Where(x => (x as ItemExperiencePerLevel).ItemKey == skillGem.ItemKey && (x as ItemExperiencePerLevel).Level == grantedEffectPerLevel.Level + 1);
                    if (gemXp.Count() == 0)
                        sb.Append(" || ").Append("{{n/a}}");
                    else
                    {
                        sb.Append(" || ").Append(((gemXp.FirstOrDefault() as ItemExperiencePerLevel).XpAmount - previousXp).ToString("N0", CultureInfo.InvariantCulture));
                        previousXp = (gemXp.FirstOrDefault() as ItemExperiencePerLevel).XpAmount;
                    }
                    sb.AppendLine();
                }
                
                File.WriteAllText(@"D:\Ccou\Poe\ggpk\ExtractGgpk\temp\skills\" + "test.txt", sb.AppendLine("|}").ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Failed"], ex.Message), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(string.Format(Settings.Strings["DatViewer_ExportCSV_Successful"], "test.txt"), Settings.Strings["DatViewer_ExportCSV_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BuildGrid(Type datType)
        {
            // Add columns
            DataGridTextColumn col0 = new DataGridTextColumn();
            col0.Header = "Key\n[Int32]";
            Binding b0 = new Binding("Key");
            col0.Binding = b0;
            col0.Foreground = Brushes.Black;
            dataGridEntries.Columns.Add(col0);
            //dataGridEntries.
            foreach (var propInfo in datType.GetProperties())
            {
                if (propInfo.GetCustomAttributes(false).Any(n => n is Hidden)) continue;
                DataGridTextColumn col1 = new DataGridTextColumn();
                string col1Header = propInfo.Name
                    .Replace("StringData", "")
                    .Replace("ListData", "")
                    .Replace("ListRef", "");
                if (col1Header.EndsWith("Ref")) col1Header = col1Header.Substring(0, col1Header.Length - 3);
                if (propInfo.PropertyType == typeof(bool)) col1Header += "\n[Bool]";
                else if (propInfo.PropertyType == typeof(Int64) || propInfo.PropertyType == typeof(UInt64)) col1Header += "\n[Int64]";
                else if (propInfo.PropertyType == typeof(Int32) || propInfo.PropertyType == typeof(UInt32)) col1Header += "\n[Int32]";
                else if (propInfo.PropertyType == typeof(UInt64List)) col1Header += "\n[ListInt64]";
                else if (propInfo.PropertyType == typeof(Int32List) || propInfo.PropertyType == typeof(UInt32List)) col1Header += "\n[ListInt32]";
                else if (propInfo.PropertyType == typeof(IndirectStringList)) col1Header += "\n[ListStr]";
                else if (propInfo.PropertyType == typeof(UnicodeString)) col1Header += "\n[Str]";
                else if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReferenceList)) col1Header += "\n[ListRef to " + propInfo.PropertyType.GetGenericArguments()[0].Name + "]";
                else if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReference)) col1Header += "\n[Ref to " + propInfo.PropertyType.Name + "]";
                col1.Header = col1Header;
                Binding b1 = new Binding(propInfo.Name);
                if (propInfo.Name.StartsWith("Hex")) b1.Converter = new HexDisplayConverter();
                else b1.Converter = new NullableDisplayConverter();
                col1.Binding = b1;
                col1.Width = DataGridLength.SizeToHeader;
                col1.Foreground = Brushes.Black;
                dataGridEntries.Columns.Add(col1);
            }

            dataGridEntries.ItemsSource = Entries;
        }

        private void buttonSave_Click_1(object sender, RoutedEventArgs e)
        {
            SaveDat();
        }

        private void buttonExportCSV_Click_1(object sender, RoutedEventArgs e)
        {
            ExportCSV();
        }

        private void buttonExportWiki_Click_1(object sender, RoutedEventArgs e)
        {
            ExportWiki();
        }

        private void dataGridEntries_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridCell) &&
                    !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep is DataGridColumnHeader)
            {
                DataGridTextColumn dgtc = (dep as DataGridColumnHeader).Column as DataGridTextColumn;
                dgtc.Foreground = (dgtc.Foreground == Brushes.LightGray) ? Brushes.Black : Brushes.LightGray;
            }
        }

        private void dataGridEntries_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            string externalType = "does not exist";

            DependencyObject dep = (DependencyObject)e.OriginalSource;
            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridCell) &&
                    !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep is DataGridCell)
            {
                //((System.Windows.Data.Binding)(((System.Windows.Controls.DataGridBoundColumn)(x)).Binding)).Path.Path
                DataGridTextColumn dgtc = (dep as DataGridCell).Column as DataGridTextColumn;
                string prop = (dgtc.Binding as Binding).Path.Path;
                Type datType = data.Entries[0].GetType();
                foreach (var propInfo in datType.GetProperties())
                {
                    if (propInfo.Name == prop)
                    {
                        if (!new String[] { "Int32", "Int64", "Boolean",
                            "Int32List", "UInt32List", "UInt64List", 
                            "IndirectStringList", "UnicodeString", 
                             }.Contains(propInfo.PropertyType.Name))
                        {
                            externalType = propInfo.PropertyType.Name;
                            if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReferenceList)) externalType = propInfo.PropertyType.GetGenericArguments()[0].Name;
                        }
                        break;
                    }
                }
            }
            /*
            DependencyObject dep2 = (DependencyObject)this;
            while ((dep2 != null) && !(dep2 is MainWindow))
            {
                dep2 = VisualTreeHelper.GetParent(dep2);
            }
            if (dep2 is MainWindow)
            {
                TreeView treeView1 = (dep2 as MainWindow).treeView1 as TreeView;
                TreeViewItem tvi = (treeView1.Items[0] as TreeViewItem); //ROOT
                tvi = (tvi.Items[0] as TreeViewItem); //""
                foreach (var child1 in tvi.Items)
                {
                    if ((child1 as TreeViewItem).Header.ToString() == "Data")
                    {
                        foreach (var child2 in (child1 as TreeViewItem).Items)
                        {
                            TreeViewItem finalTvi = (child2 as TreeViewItem);
                            if (finalTvi.Header.ToString().StartsWith(externalType + ".") && finalTvi.Header.ToString().EndsWith(".dat"))
                            {
                                finalTvi.Foreground = Brushes.Red;
                                finalTvi.FontWeight = FontWeights.Bold;
                            }
                            else
                            {
                                finalTvi.Foreground = Brushes.Black;
                                finalTvi.FontWeight = FontWeights.Normal;
                            }
                        }
                        return;
                    }
                }
            }

            */
        }
    }

    public class HexDisplayConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Int32)
            {
                return (value as Int32?).GetValueOrDefault().ToString("x8");
            }
            else
                return value.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    public class NullableDisplayConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Int32)
            {
                Int32 nullable = (value as Int32?).GetValueOrDefault();
                if (nullable == -16843010) return "null";
                return nullable;
            }
            else if (value is Int64)
            {
                Int64 nullable = (value as Int64?).GetValueOrDefault();
                if (nullable == -72340172838076674) return "null";
                return nullable;
            }
            else
                return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    public class DatWrapper
    {
        private string fileName;
        private string datName;
        private readonly List<UnicodeString> _dataStrings = new List<UnicodeString>();

        public DatContainer Dat { get; protected set; }
        public List<BaseDat> Entries
        {
            get { return Dat.Entries; }
        }
        public Dictionary<int, BaseData> DataEntries
        {
            get { return Dat.DataEntries; }
        }

        public List<UnicodeString> Strings
        {
            get
            {
                return _dataStrings;
            }
        }

        public DatWrapper(string fileName)
        {
            this.fileName = fileName;
            this.datName = Path.GetFileNameWithoutExtension(fileName);

            byte[] fileBytes = File.ReadAllBytes(fileName);

            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                ParseDatFile(ms);
            }
        }

        public DatWrapper(Stream inStream, string fileName)
        {
            this.fileName = fileName;
            this.datName = Path.GetFileNameWithoutExtension(fileName);
            ParseDatFile(inStream);
        }


        private void ParseDatFile(Stream inStream)
        {
            Dat = new DatContainer(inStream, datName);

            try
            {
                var containerData = DataEntries.ToList();

                foreach (var keyValuePair in containerData)
                {
                    if (keyValuePair.Value is UnicodeString)
                    {
                        Strings.Add((UnicodeString)keyValuePair.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Settings.Strings["DatWrapper_ParseDatFile_Failed"], ex.Message), ex);
            }
        }

        public void Save(string savePath)
        {
            try
            {
                Dat.Save(savePath);
            }
            catch (Exception ex)
            {
                StringBuilder errorString = new StringBuilder();

                Exception temp = ex;
                while (temp != null)
                {
                    errorString.AppendLine(temp.Message);
                    temp = temp.InnerException;
                }

                MessageBox.Show(string.Format(Settings.Strings["DatWrapper_Save_Failed"], errorString), Settings.Strings["Error_Caption"], MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show(string.Format(Settings.Strings["DatWrapper_Save_Successful"], savePath), Settings.Strings["DatWrapper_Save_Successful_Caption"], MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}