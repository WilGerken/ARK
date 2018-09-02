using System;

namespace Library.Common
{
    /// <summary>
    /// base class for filter objects
    /// </summary>
    public abstract class Data_F_Base
    {
        public int?  objectID   { get; set; }
        public int?  selectedID { get; set; }
        public bool? activeYn   { get; set; }

        public int?  userID     { get; set; }
    }

    /// <summary>
    /// base class for keys
    /// </summary>
    public abstract class Data_K_Base
    {
        public int? objectID { get; set; }
    }

    /// <summary>
    /// base class for data objects
    /// </summary>
    public abstract class Data_O_Base
    {
        public int      objectID     { get; set; }
        public bool     activeYn     { get; set; }
        public int      createByUid  { get; set; }
        public DateTime createOnDts  { get; set; }
        public int      updateByUid  { get; set; }
        public DateTime updateOnDts  { get; set; }
        public byte[]   versionKey   { get; set; }

        protected Data_O_Base ()
        {
            objectID    = -1;
            activeYn    = true;
            createByUid = updateByUid = AppInfo.UserID;
            createOnDts = updateOnDts = DateTime.Now;
        }
    }
}
