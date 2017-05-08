using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibDat
{
    public sealed class ReferenceManager
    {
        #region Singleton
        private static readonly ReferenceManager instance = new ReferenceManager();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ReferenceManager()
        {
        }

        private ReferenceManager()
        {
        }

        public static ReferenceManager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public Dictionary<string, List<BaseDat>> AllDats = new Dictionary<string, List<BaseDat>>();

        public void UpdateReferences()
        {
            if (AllDats.Count != 0x55)
                throw new Exception("Invalid dat count："+ AllDats.Count);
        }
    }
}
