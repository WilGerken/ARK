using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Common
{
    /// <summary>
    /// public interface for TagType items
    /// </summary>
    public interface I_TAG_TYPE
    {
        List<D_TAG_TYPE> SelectList (F_TAG_TYPE aFilter);
        void             DeleteList (F_TAG_TYPE aFilter);

        D_TAG_TYPE SelectItem (K_TAG_TYPE aKey);
        D_TAG_TYPE InsertItem (D_TAG_TYPE aDto);
        D_TAG_TYPE UpdateItem (D_TAG_TYPE aDto);
        void       DeleteItem (K_TAG_TYPE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_TAG_TYPE : Data_F_Base
    {
        public string typeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_TAG_TYPE () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_TAG_TYPE : Data_K_Base
    {
        public string typeTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_TAG_TYPE : Data_O_Base
    {
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_TAG_TYPE () : base () { }
    }
}