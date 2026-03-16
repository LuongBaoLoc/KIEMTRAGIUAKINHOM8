using KTGKNhom8.Debugging;

namespace KTGKNhom8
{
    public class KTGKNhom8Consts
    {
        public const string LocalizationSourceName = "KTGKNhom8";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3f6adba9e4f6415eb11c308cf55b5382";
    }
}
