using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Story
{
    /// <summary>
    /// public interface for StoryEntity items
    /// </summary>
    public interface I_STORY_ENTITY
    {
        List<D_STORY_ENTITY> SelectList (F_STORY_ENTITY aFilter);
        void                 DeleteList (F_STORY_ENTITY aFilter);

        D_STORY_ENTITY SelectItem (K_STORY_ENTITY aKey);
        D_STORY_ENTITY InsertItem (D_STORY_ENTITY aDto);
        D_STORY_ENTITY UpdateItem (D_STORY_ENTITY aDto);
        void           DeleteItem (K_STORY_ENTITY aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_STORY_ENTITY : Data_F_Base
    {
        public int?   storyID  { get; set; }
        public int?   entityID { get; set; }
        public int?   roleID   { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_STORY_ENTITY () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_STORY_ENTITY : Data_K_Base
    {
        public int? storyID  { get; set; }
        public int? entityID { get; set; }
        public int? roleID   { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_STORY_ENTITY : Data_O_Base
    {
        public int    storyID  { get; set; }
        public string titleTxt { get; set; }
        public int    entityID { get; set; }
        public string entityNm { get; set; }
        public int?   roleID   { get; set; }
        public string roleTxt  { get; set; }
        public string descTxt  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_STORY_ENTITY () : base () { }
    }
}