using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Entity
{
    /// <summary>
    /// public interface for EntityStory items
    /// </summary>
    public interface I_ENTITY_STORY
    {
        List<D_ENTITY_STORY> SelectList (F_ENTITY_STORY aFilter);
        void                 DeleteList (F_ENTITY_STORY aFilter);

        D_ENTITY_STORY SelectItem (K_ENTITY_STORY aKey);
        D_ENTITY_STORY InsertItem (D_ENTITY_STORY aDto);
        D_ENTITY_STORY UpdateItem (D_ENTITY_STORY aDto);
        void           DeleteItem (K_ENTITY_STORY aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ENTITY_STORY : Data_F_Base
    {
        public int?   entityID { get; set; }
        public int?   storyID  { get; set; }
        public int?   typeID   { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ENTITY_STORY () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ENTITY_STORY : Data_K_Base
    {
        public int? entityID { get; set; }
        public int? storyID  { get; set; }
        public int? typeID   { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ENTITY_STORY : Data_O_Base
    {
        public int    entityID { get; set; }
        public string entityNm { get; set; }
        public int    storyID  { get; set; }
        public string titleTxt { get; set; }
        public int?   typeID   { get; set; }
        public string typeTxt  { get; set; }
        public string descTxt  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ENTITY_STORY () : base () { }
    }
}