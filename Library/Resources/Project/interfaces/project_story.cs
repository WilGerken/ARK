using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ProjectStory items
    /// </summary>
    public interface I_PROJECT_STORY
    {
        List<D_PROJECT_STORY> SelectList (F_PROJECT_STORY aFilter);
        void                  DeleteList (F_PROJECT_STORY aFilter);

        D_PROJECT_STORY SelectItem (K_PROJECT_STORY aKey);
        D_PROJECT_STORY InsertItem (D_PROJECT_STORY aDto);
        D_PROJECT_STORY UpdateItem (D_PROJECT_STORY aDto);
        void            DeleteItem (K_PROJECT_STORY aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_STORY : Data_F_Base
    {
        public int?   projectID { get; set; }
        public int?   storyID   { get; set; }
        public int?   roleID    { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_STORY () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_STORY : Data_K_Base
    {
        public int? projectID { get; set; }
        public int? storyID   { get; set; }
        public int? roleID    { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_STORY : Data_O_Base
    {
        public int    projectID { get; set; }
        public string projectNm { get; set; }
        public int    storyID   { get; set; }
        public string titleTxt  { get; set; }
        public int?   roleID    { get; set; }
        public string roleTxt   { get; set; }
        public string descTxt   { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_STORY () : base () { }
    }
}