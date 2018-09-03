using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Story
{
    /// <summary>
    /// public interface for ArkStory items
    /// </summary>
    public interface I_ARK_STORY
    {
        List<D_ARK_STORY> SelectList (F_ARK_STORY aFilter);
        void              DeleteList (F_ARK_STORY aFilter);

        D_ARK_STORY SelectItem (K_ARK_STORY aKey);
        D_ARK_STORY InsertItem (D_ARK_STORY aDto);
        D_ARK_STORY UpdateItem (D_ARK_STORY aDto);
        void        DeleteItem (K_ARK_STORY aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ARK_STORY : Data_F_Base
    {
        public int?      authorID     { get; set; }
        public string    titleTxt     { get; set; }
        public DateTime? fromEntryDt  { get; set; }
        public DateTime? thruEntryDt  { get; set; }
        public string    narrativeTxt { get; set; }
        public string    tagTxt       { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ARK_STORY () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ARK_STORY : Data_K_Base
    {
        public int?   authorID { get; set; }
        public string titleTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public K_ARK_STORY() { }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ARK_STORY : Data_O_Base
    {
        public int      authorID     { get; set; }
        public string   authorNm     { get; set; }
        public string   titleTxt     { get; set; }
        public DateTime entryDts     { get; set; }
        public string   narrativeTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ARK_STORY () : base () { }
    }
}