using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Story
{
    /// <summary>
    /// public interface for ArkStory items
    /// </summary>
    public interface I_ARK_BLOG
    {
        List<D_ARK_BLOG> SelectList (F_ARK_BLOG aFilter);
        void             DeleteList (F_ARK_BLOG aFilter);

        D_ARK_BLOG SelectItem (K_ARK_BLOG aKey);
        D_ARK_BLOG InsertItem (D_ARK_BLOG aDto);
        D_ARK_BLOG UpdateItem (D_ARK_BLOG aDto);
        void       DeleteItem (K_ARK_BLOG aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ARK_BLOG : Data_F_Base
    {
        public int?      entityID     { get; set; }
        public DateTime? fromEntryDt  { get; set; }
        public DateTime? thruEntryDt  { get; set; }
        public string    titleTxt     { get; set; }
        public string    narrativeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ARK_BLOG () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ARK_BLOG : Data_K_Base
    {
        public int?      entityID { get; set; }
        public DateTime? entryDts { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ARK_BLOG : Data_O_Base
    {
        public int      entityID     { get; set; }
        public DateTime entryDts     { get; set; }
        public string   titleTxt     { get; set; }
        public string   narrativeTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ARK_BLOG () : base () { }
    }
}