namespace DemoProject.Services.Constants
{
    public static class EntryStatus
    {
        //  const and readonly perform a similar function on data members, but they have a few important differences.
        //  A constant member is defined at compile time and cannot be changed at runtime.
        //  Constants are declared as a field, using the const keyword and must be initialized as they are declared.

        //  The static modifier is used to declare a static member, this means that the member is no longer tied to a specific object. 
        //  The value belongs to the class, additionally the member can be accessed without creating an instance of the class. 
        //  Only one copy of static fields and events exists, and static methods and properties can only access static fields and static events

        public const string Amended = "AMN";
        public const string Created = "CRT";
        public const string Deleted = "DEL";
        public const string Modified = "MDF";
        public const string Rejected = "REJ";
        public const string Verified = "VRF";
        public const string Unverified = "UVF";
    }
}
