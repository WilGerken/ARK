using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Story
{
    /// <summary>
    /// public interface for StoryTag items
    /// </summary>
    public interface I_STORY_TAG
    {
        List<D_STORY_TAG> SelectList (F_STORY_TAG aFilter);
        void              DeleteList (F_STORY_TAG aFilter);

        D_STORY_TAG SelectItem (K_STORY_TAG aKey);
        D_STORY_TAG InsertItem (D_STORY_TAG aDto);
        D_STORY_TAG UpdateItem (D_STORY_TAG aDto);
        void        DeleteItem (K_STORY_TAG aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_STORY_TAG : Data_F_Base
    {
        public int?   storyID { get; set; }
        public int?   tagID   { get; set; }
        public int?   typeID  { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_STORY_TAG() { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_STORY_TAG : Data_K_Base
    {
        public int? storyID { get; set; }
        public int? tagID   { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_STORY_TAG : Data_O_Base
    {
        public int    storyID  { get; set; }
        public string titleTxt { get; set; }
        public int    tagID    { get; set; }
        public string tagTxt   { get; set; }
        public int?   typeID   { get; set; }
        public string typeTxt  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_STORY_TAG() : base() { }
    }
}