namespace LibGGPK
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Handles parsing the GGPK pack file.
    /// </summary>
    public class GGPK
    {
        /// <summary>
        /// Map of every records offsets in the pack file
        /// </summary>
        public readonly Dictionary<long, BaseRecord> RecordOffsets;

        /// <summary>
        /// Root of the directory tree
        /// </summary>
        public DirectoryTreeNode DirectoryRoot;

        /// <summary>
        /// Root of the Free record list
        /// </summary>
        public LinkedList<FreeRecord> FreeRoot;

        /// <summary>
        /// An estimation of the number of records in the Contents.GGPK file. This is only
        /// used to inform the users of the parsing progress.
        /// </summary>
        private const int EstimatedFileCount = 60000;

        public bool IsReadOnly => isReadOnly;
        private bool isReadOnly;

        public GGPK()
        {
            RecordOffsets = new Dictionary<long, BaseRecord>(EstimatedFileCount);
        }

        /// <summary>
        /// Iterates through the pack file and records the offsets and headers of each record found
        /// </summary>
        /// <param name="pathToGgpk">Path to pack file to read</param>
        /// <param name="output">Output function</param>
        private void ReadRecordOffsets(string pathToGgpk, Action<string> output)
        {
            float previousPercentComplete = 0.0f;
            using (FileStream fs = Utils.OpenFile(pathToGgpk, out isReadOnly))
            {
                BinaryReader br = new BinaryReader(fs);
                long streamLength = br.BaseStream.Length;

                while (br.BaseStream.Position < streamLength)
                {
                    long currentOffset = br.BaseStream.Position;
                    BaseRecord record = RecordFactory.ReadRecord(br);
                    RecordOffsets.Add(currentOffset, record);
                    float percentComplete = currentOffset / (float)streamLength;
                    if (!(percentComplete - previousPercentComplete >= 0.10f)) continue;
                    output?.Invoke($"{100.0 * percentComplete:00.00}%{Environment.NewLine}");
                    previousPercentComplete = percentComplete;
                }
                output?.Invoke($"{100.0f * br.BaseStream.Position / br.BaseStream.Length:00.00}%{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Creates a directory tree using the parsed record headers and offsets
        /// </summary>
        /// <param name="output">Output function</param>
        private void CreateDirectoryTree(Action<string> output)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));
            DirectoryRoot = DirectoryTreeMaker.BuildDirectoryTree(RecordOffsets);
            FreeRoot = FreeListMaker.BuildFreeList(RecordOffsets);
        }

        /// <summary>
        /// Parses the GGPK pack file and builds a directory tree from it.
        /// </summary>
        /// <param name="pathToGgpk">Path to pack file to read</param>
        /// <param name="output">Output function</param>
        public void Read(string pathToGgpk, Action<string> output)
        {
            output?.Invoke("GGPK:"+ pathToGgpk + Environment.NewLine);
            ReadRecordOffsets(pathToGgpk, output);
            if (output != null)
            {
                output(Environment.NewLine);
                output(">>>" + Environment.NewLine);
            }
            CreateDirectoryTree(output);
        }
    }
}