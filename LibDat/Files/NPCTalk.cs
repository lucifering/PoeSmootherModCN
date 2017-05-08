using System;
using System.IO;

namespace LibDat.Files
{
    public class NPCTalk : BaseDat
    {
        [Hidden]
        public Int64 NPCKey { get; set; }
        [ExternalReference]
        public NPCs NPCRef { get; set; }
        public int DialogPriority { get; set; }
        [Hidden]
        public int DialogueOptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DialogueOptionStringData { get; set; }
        [Hidden]
        public int U04ListLength { get; set; }
        [Hidden]
        public int U04ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U04ListData { get; set; }
        [Hidden]
        public int U06ListLength { get; set; }
        [Hidden]
        public int U06ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U06ListData { get; set; }
        [Hidden]
        public int U08ListLength { get; set; }
        [Hidden]
        public int U08ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U08ListData { get; set; }
        [Hidden]
        public int ScriptStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ScriptStringData { get; set; }
        public int U11 { get; set; }
        public int U12 { get; set; }
        [Hidden]
        public int RewardedQuestKey { get; set; }
        [ExternalReference]
        public Quest RewardedQuestRef { get; set; }
        public int U14 { get; set; }
        public int U15 { get; set; }
        [Hidden]
        public int DifficultyListLength { get; set; }
        [Hidden]
        public int DifficultyListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List DifficultyListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Difficulties> DifficultyListRef { get; set; }
        [Hidden]
        public int TextAudioListLength { get; set; }
        [Hidden]
        public int TextAudioListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TextAudioListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<NPCTextAudio> TextAudioListRef { get; set; }
        [Hidden]
        public int U20StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U20StringData { get; set; }
        public bool Flag0 { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }

        public NPCTalk(BinaryReader inStream)
        {
            NPCKey = inStream.ReadInt64();
            DialogPriority = inStream.ReadInt32();
            DialogueOptionStringOffset = inStream.ReadInt32();
            U04ListLength = inStream.ReadInt32();
            U04ListOffset = inStream.ReadInt32();
            U06ListLength = inStream.ReadInt32();
            U06ListOffset = inStream.ReadInt32();
            U08ListLength = inStream.ReadInt32();
            U08ListOffset = inStream.ReadInt32();
            ScriptStringOffset = inStream.ReadInt32();
            U11 = inStream.ReadInt32();
            U12 = inStream.ReadInt32();
            RewardedQuestKey = inStream.ReadInt32();
            U14 = inStream.ReadInt32();
            U15 = inStream.ReadInt32();
            DifficultyListLength = inStream.ReadInt32();
            DifficultyListOffset = inStream.ReadInt32();
            TextAudioListLength = inStream.ReadInt32();
            TextAudioListOffset = inStream.ReadInt32();
            U20StringOffset = inStream.ReadInt32();
            Flag0 = inStream.ReadBoolean();
            Flag1 = inStream.ReadBoolean();
            Flag2 = inStream.ReadBoolean();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(NPCKey);
            outStream.Write(DialogPriority);
            outStream.Write(DialogueOptionStringOffset);
            outStream.Write(U04ListLength);
            outStream.Write(U04ListOffset);
            outStream.Write(U06ListLength);
            outStream.Write(U06ListOffset);
            outStream.Write(U08ListLength);
            outStream.Write(U08ListOffset);
            outStream.Write(ScriptStringOffset);
            outStream.Write(U11);
            outStream.Write(U12);
            outStream.Write(RewardedQuestKey);
            outStream.Write(U14);
            outStream.Write(U15);
            outStream.Write(DifficultyListLength);
            outStream.Write(DifficultyListOffset);
            outStream.Write(TextAudioListLength);
            outStream.Write(TextAudioListOffset);
            outStream.Write(U20StringOffset);
            outStream.Write(Flag0);
            outStream.Write(Flag1);
            outStream.Write(Flag2);
        }

        public override int GetSize()
        {
            return 0x57;
        }
    }
}