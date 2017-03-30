namespace LibGGPK
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Helper class to generate directory trees
    /// </summary>
    internal static class DirectoryTreeMaker
    {
        /// <summary>
        /// Builds a directory tree by traversing the list of record headers found in the pack file
        /// </summary>
        /// <param name="recordOffsets">Map of record offsets and headers to create directory tree from</param>
        /// <returns>Root node of directory tree</returns>
        internal static DirectoryTreeNode BuildDirectoryTree(Dictionary<long, BaseRecord> recordOffsets)
        {
            // This offset is a directory, add it as a child of root and process all of it's entries
            GGPKRecord currentDirectory = recordOffsets[0] as GGPKRecord;

            DirectoryTreeNode root = new DirectoryTreeNode()
            {
                Children = new List<DirectoryTreeNode>(),
                Files = new List<FileRecord>(),
                Name = "",
                Parent = null,
                Record = null
            };

            // First level only contains a empty string name directory record and a free record
            if (currentDirectory == null) return root;
            foreach (var item in from Offset in currentDirectory.RecordOffsets
                                 where recordOffsets.ContainsKey(Offset)
                                 where recordOffsets[Offset] is DirectoryRecord
                                 select recordOffsets[Offset] as DirectoryRecord
                                 into firstDirectory
                                 from item in firstDirectory.Entries
                                 select item)
            {
                BuildDirectoryTree(item, root, recordOffsets);
            }
            return root;
        }

        /// <summary>
        /// Recursivly creates a directory tree by traversing PDIR records. Adds FILE records to the current directory
        /// tree node. Recursivly traverses PDIR records and adds them to the current directory tree node's children.
        /// </summary>
        /// <param name="directoryEntry">Directory Entry</param>
        /// <param name="root">Parent node</param>
        /// <param name="recordOffsets">Map of record offsets and headers to create directory tree from</param>
        private static void BuildDirectoryTree(DirectoryRecord.DirectoryEntry directoryEntry, DirectoryTreeNode root, IReadOnlyDictionary<long, BaseRecord> recordOffsets)
        {
            if (!recordOffsets.ContainsKey(directoryEntry.Offset))
            {
                return;
            }

            var record = recordOffsets[directoryEntry.Offset] as DirectoryRecord;
            if (record != null)
            {
                // This offset is a directory, add it as a child of root and process all of it's entries
                DirectoryRecord currentDirectory = record;
                DirectoryTreeNode child = new DirectoryTreeNode
                {
                    Name = currentDirectory.Name,
                    Parent = root,
                    Children = new List<DirectoryTreeNode>(),
                    Files = new List<FileRecord>(),
                    Record = currentDirectory,
                };

                root.Children.Add(child);

                foreach (var entry in currentDirectory.Entries)
                {
                    BuildDirectoryTree(entry, child, recordOffsets);
                }
            }
            else
            {
                var file = recordOffsets[directoryEntry.Offset] as FileRecord;
                if (file == null) return;
                FileRecord currentFile = file;
                currentFile.ContainingDirectory = root;
                root.Files.Add(currentFile);
            }
        }
    }
}