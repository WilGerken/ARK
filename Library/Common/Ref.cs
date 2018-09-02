using System;

namespace Library.Common
{
    public class Ref
    {
        // ID for new domain objects
        public const int NewID   = -1;
        public const int AdminID = 1;
        public const int GuestID = 2;

        public enum AppUser { Admin = 0, Staff }
        public enum AppRole { Any   = 0, Admin, Staff, Guild, Player, Guest }

        // select options
        public int eNone = -1;
        public int eAny  =  0;
    }
}
