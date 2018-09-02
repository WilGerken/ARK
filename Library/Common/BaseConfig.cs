using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;

namespace Library.Common
{
    /// <summary>
    /// basic application info
    /// </summary>
    public static class AppInfo
    {
        public static Version Version { get { return Assembly.GetCallingAssembly().GetName().Version; } }

        public static string Title
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string ProductName
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string Description
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string CopyrightHolder
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string CompanyName
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public static int UserID
        {
            get { return 0; }
        }
    }

    /// <summary>
    /// application configuration attributes (available at library level)
    /// </summary>
    public abstract class AppConfig
    {
        public const string APP_DATA_CONNECT_SETTINGS_KEY = "DataConnectKey";
        public const string APP_LOG_CONNECT_SETTINGS_KEY  = "LogConnectKey";

        /// <summary>
        /// ConfigurationManager.AppSettings
        /// </summary>
        private static NameValueCollection AppSettings { get { return ConfigurationManager.AppSettings; } }

        // naming

        public static string MachineName { get { return Environment.MachineName.ToUpper(); } }

        private const string APP_CONFIG_NAME_KEY = "KEY_APP_ConfigName";
        public static string ConfigName
        {
            get { return AppSettings[APP_CONFIG_NAME_KEY]; }
            set { AppSettings[APP_CONFIG_NAME_KEY] = value; }
        }

        // logging config

        private const string APP_LOG_LEVEL_KEY = "KEY_APP_LogLevelCd";
        public static string LogLevelCd
        {
            get { return AppSettings[APP_LOG_LEVEL_KEY]; }
            set { AppSettings[APP_LOG_LEVEL_KEY] = value; }
        }

        // auth config

        private const string APP_RUN_AS_ADMIN_KEY = "KEY_APP_RunAsAdminYn";
        public static bool RunAsAdminYn
        {
            get { return AppSettings[APP_RUN_AS_ADMIN_KEY].ToLower() == "true"; }
            set { AppSettings[APP_RUN_AS_ADMIN_KEY] = value ? "true" : "false"; }
        }

        private const string APP_RUN_AS_STAFF_KEY = "KEY_APP_RunAsStaffYn";
        public static bool RunAsStaffYn
        {
            get { return AppSettings[APP_RUN_AS_STAFF_KEY].ToLower() == "true"; }
            set { AppSettings[APP_RUN_AS_STAFF_KEY] = value ? "true" : "false"; }
        }

        private const string APP_RUNAS_USERID_KEY = "KEY_APP_RunAsUserID";
        public static int RunAsUserId
        {
            get { return Convert.ToInt32(AppSettings[APP_RUNAS_USERID_KEY]); }
            set { AppSettings[APP_RUNAS_USERID_KEY] = value.ToString(); }
        }

        // repository config

        private const string APP_DB_CONNECT_TXT_KEY = "KEY_APP_DbConnectionTxt";
        public static string DbConnectionTxt
        {
            get { return AppSettings[APP_DB_CONNECT_TXT_KEY]; }
            set { AppSettings[APP_DB_CONNECT_TXT_KEY] = value; }
        }

        private const string APP_LOG_CONNECT_TXT_KEY = "KEY_APP_LogConnectionTxt";
        public static string LogConnectionTxt
        {
            get { return AppSettings[APP_LOG_CONNECT_TXT_KEY]; }
            set { AppSettings[APP_LOG_CONNECT_TXT_KEY] = value; }
        }

        // eMail

        private const string APP_EMAIL_HOST_NM_KEY = "KEY_APP_EmailHostNm";
        public static string EmailHostNm
        {
            get { return AppSettings[APP_EMAIL_HOST_NM_KEY]; }
            set { AppSettings[APP_EMAIL_HOST_NM_KEY] = value; }
        }

        private const string APP_EMAIL_FROM_TXT_KEY = "KEY_APP_EmailFromTxt";
        public static string EmailFromTxt
        {
            get { return AppSettings[APP_EMAIL_FROM_TXT_KEY]; }
            set { AppSettings[APP_EMAIL_FROM_TXT_KEY] = value; }
        }

        // Error Email

        private const string APP_ERROR_EMAIL_YN_KEY = "KEY_APP_ErrorEmailYn";
        public static bool ErrorEmailYn
        {
            get { return AppSettings[APP_ERROR_EMAIL_YN_KEY].ToLower() == "true"; }
            set { AppSettings[APP_ERROR_EMAIL_YN_KEY] = value ? "true" : "false"; }
        }

        private const string APP_ERROR_EMAIL_LEVEL_KEY = "KEY_APP_ErrorEmailLevelCd";
        public static string ErrorEmailLevelCd
        {
            get { return AppSettings[APP_ERROR_EMAIL_LEVEL_KEY]; }
            set { AppSettings[APP_ERROR_EMAIL_LEVEL_KEY] = value; }
        }

        private const string APP_ERROR_EMAIL_RECIPIENT_LST_KEY = "KEY_APP_ErrorEmailRecipients";
        public static string ErrorEmailRecipientLst
        {
            get { return AppSettings[APP_ERROR_EMAIL_RECIPIENT_LST_KEY]; }
            set { AppSettings[APP_ERROR_EMAIL_RECIPIENT_LST_KEY] = value; }
        }

        private const string APP_ERROR_EMAIL_HOST_URL_KEY = "KEY_APP_ErrorEmailHostUrl";
        public static string ErrorEmailHostUrl
        {
            get { return AppSettings[APP_ERROR_EMAIL_HOST_URL_KEY]; }
            set { AppSettings[APP_ERROR_EMAIL_HOST_URL_KEY] = value; }
        }

        private const string APP_ERROR_EMAIL_SUBJ_APPEND_TXT_KEY = "KEY_APP_ErrorEmailSubjAppendTxt";
        public static string ErrorEmailSubjAppendTxt
        {
            get { return AppSettings[APP_ERROR_EMAIL_SUBJ_APPEND_TXT_KEY]; }
            set { AppSettings[APP_ERROR_EMAIL_SUBJ_APPEND_TXT_KEY] = value; }
        }

        private const string APP_XML_FILE_NM_KEY = "KEY_APP_XmlFileNm";
        public static string XmlFileNm
        {
            get { return AppSettings[APP_XML_FILE_NM_KEY]; }
            set { AppSettings[APP_XML_FILE_NM_KEY] = value; }
        }

        // Development Config

        /// <summary>
        /// proxy authentication locally instead of reaching out to LDAP or MyAk
        /// </summary>
        public static bool DebugAuthLocalYn
        {
            get { return AppSettings[APP_DEBUG_AUTH_LOCAL_KEY].ToLower() == "true"; }
            set { AppSettings[APP_DEBUG_AUTH_LOCAL_KEY] = value ? "true" : "false"; }
        }
        private const string APP_DEBUG_AUTH_LOCAL_KEY = "KEY_APP_DebugAuthLocalYn";

        /// <summary>
        /// ID of user for local authentication proxy
        /// </summary>
        public static int DebugAuthLocalId
        {
            get { return Convert.ToInt32(AppSettings[APP_DEBUG_AUTH_LOCAL_ID_KEY]); }
            set { AppSettings[APP_DEBUG_AUTH_LOCAL_ID_KEY] = value.ToString(); }
        }
        private const string APP_DEBUG_AUTH_LOCAL_ID_KEY = "KEY_APP_DebugAuthLocalId";

        /// <summary>
        /// run application on local host (includes redirect from MyAk login for ApocForms) 
        /// proxy web services (dmv, eSign, pmnt) locally as indicated by application-specific config
        /// </summary>
        public static bool DebugRunLocalYn
        {
            get { return AppSettings[APP_DEBUG_RUN_LOCAL_KEY].ToLower() == "true"; }
            set { AppSettings[APP_DEBUG_RUN_LOCAL_KEY] = value ? "true" : "false"; }
        }
        private const string APP_DEBUG_RUN_LOCAL_KEY = "KEY_APP_DebugRunLocalYn";

        /// <summary>
        /// copy values for common configuration settings
        /// </summary>
        /// <param name="aConfig"></param>
        public static void Initialize (CommonConfig aConfig)
        {
            var tmp = ConfigurationManager.ConnectionStrings;

            LogLevelCd        = aConfig.LogLevel;

            RunAsAdminYn = aConfig.RunAsAdmin;
            RunAsStaffYn = aConfig.RunAsStaff;

            DbConnectionTxt = aConfig.DbConnectionString;
            LogConnectionTxt = string.IsNullOrEmpty (aConfig.LogConnectionString) ? DbConnectionTxt : aConfig.LogConnectionString;

            UpdateDbConnectionSetting (APP_DATA_CONNECT_SETTINGS_KEY, DbConnectionTxt);
            UpdateDbConnectionSetting (APP_LOG_CONNECT_SETTINGS_KEY,  LogConnectionTxt);

            EmailHostNm  = aConfig.EmailHost;
            EmailFromTxt = aConfig.EmailFrom;

            ErrorEmailYn            = aConfig.ErrorEmail;
            ErrorEmailLevelCd       = aConfig.ErrorEmailLevel;
            ErrorEmailRecipientLst  = aConfig.ErrorEmailRecipients;
            ErrorEmailSubjAppendTxt = aConfig.ErrorEmailSubjectAppend;

            DebugAuthLocalYn = aConfig.DebugAuthLocal;
            DebugAuthLocalId = aConfig.DebugAuthLocalId;
            DebugRunLocalYn  = aConfig.DebugRunLocal;
        }

        /// <summary>
        /// update (read-only) connection string setting
        /// </summary>
        /// <param name="aName">name of connection string (from cnofig)</param>
        /// <param name="aTxt">text of connection string</param>
        private static void UpdateDbConnectionSetting (string aName, string aTxt)
        {
            var settings = ConfigurationManager.ConnectionStrings[aName];

            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

            fi.SetValue (settings, false);

            settings.ConnectionString = aTxt;
        }

        /// <summary>
        /// update (read-only) connection string setting
        /// </summary>
        /// <param name="aName">name of connection string (from cnofig)</param>
        /// <param name="aTxt">text of connection string</param>
        private static void UpdateCtxConnectionSetting (string aName, string aTxt)
        {
            var settings = ConfigurationManager.ConnectionStrings[aName];

            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

            fi.SetValue(settings, false);

            settings.ConnectionString = string.Format (settings.ConnectionString, aTxt);
        }
    }

#if (NEVER)
    /// <summary>
    /// 
    /// </summary>
    public class HostConfigSectionHandler : IConfigurationSectionHandler
    {
        public const string DataContractNamespace = @"http://soa.alaska.gov/doa/ets/config";
        public const string KEY_CFG_WEB_CONFIG_SECTION = "hostConfig";

        private const string DefaultServerXmlDesignator = "*";							//
        private const string PathConfigConvention = @"~\MachineConfigs\{0}.config";     //
        private const string ConventionDefaultConfigFilename = @"_default";				//
        private const string ConfigObjectReturnType = "returnType";						//
        private const string ElementServerKey = "server";								//
        private const string AttributeMachineNameKey = "machineName";					//
        private const string AttributeConfigFileKey = "configFile";						//

        // xpath query used to find matching element/attribute. converts values to upper case
        private const string XpathQuery = @"{0}[translate(@{1},'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='{2}']";

        public object Create (object parent, object configContext, XmlNode section)
        {
            // This will load the appropriate configuration file based on the machine name of the server its running on (from web.config <environment>)
            //
            // attempt to load config file in order of:
            //   specific machine config - ~\MachineConfigs\{configName}.config
            //   wildcard machine config - ~\MachineConfigs\default.config

            // object type to convert config file to
            Type returnType = Type.GetType(section.Attributes[ConfigObjectReturnType].Value);

            // xpathquery to find if there is a specified configuration file for this machine name
            string xpqFindThisMachineConfigFilename = string.Format (XpathQuery, ElementServerKey, AttributeMachineNameKey, AppConfig.MachineName);

            // xpathquery to find if there is a specified default configuration file
            string xpqFindDefaultConfigFilename = string.Format (XpathQuery, ElementServerKey, AttributeMachineNameKey, DefaultServerXmlDesignator);

            // attempt to load specific machine config
            XmlNode xmlNode = section.SelectSingleNode(xpqFindThisMachineConfigFilename);

            if (xmlNode != null)
            {
                String filePath = HttpContext.Current.Server.MapPath(xmlNode.Attributes[AttributeConfigFileKey].Value);

                return this.LoadConfiguration(filePath, returnType);
            }

            // attempt to load default machine config
            xmlNode = section.SelectSingleNode(xpqFindDefaultConfigFilename);

            if (xmlNode != null)
            {
                String filePath = HttpContext.Current.Server.MapPath(xmlNode.Attributes["configFile"].Value);

                return this.LoadConfiguration(filePath, returnType);
            }

            throw new HostConfigSectionHandlerException("Could not load host environment configuration. No specified files for this machine name and default files not found.");
        }

        /// <summary>
        /// read given configuration file into memory (as structured by data contract)
        /// </summary>
        /// <param name="pPathConfigFile"></param>
        /// <param name="pType"></param>
        /// <returns></returns>
        private object LoadConfiguration(string pPathConfigFile, Type pType)
        {
            Stream stream = new FileStream(pPathConfigFile, FileMode.Open, FileAccess.Read);

            HostConfigSectionHandlerInformationSingleton.Instance.LoadedConfigFile = pPathConfigFile;

            return (new DataContractSerializer(pType)).ReadObject(stream);
        }

        /// <summary>
        /// specialized exception for configuration handling
        /// </summary>
        [Serializable]
        public class HostConfigSectionHandlerException : Exception
        {
            public HostConfigSectionHandlerException() { }

            public HostConfigSectionHandlerException(string message) : base(message) { }

            public HostConfigSectionHandlerException(string message, Exception inner) : base(message, inner) { }

            protected HostConfigSectionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
    }

    /// <summary>
    /// a singleton class just to hold some information about what config file was loaded for debugging purposes
    /// </summary>
    public sealed class HostConfigSectionHandlerInformationSingleton
    {
        static readonly HostConfigSectionHandlerInformationSingleton instance = new HostConfigSectionHandlerInformationSingleton();

        public static HostConfigSectionHandlerInformationSingleton Instance { get { return instance; } }

        static HostConfigSectionHandlerInformationSingleton() { }

        public string LoadedConfigFile { get; set; }

        HostConfigSectionHandlerInformationSingleton() { }
    }
#endif

    /// <summary>
    /// data contract class for common configuration attributes (available at library level)
    /// </summary>
    //[DataContract(Namespace = HostConfigSectionHandler.DataContractNamespace)]
    public class CommonConfig
    {
#region logging

        [DataMember(IsRequired = true)]
        public string LogLevel { get; private set; }

#endregion

#region repository

        [DataMember(IsRequired = true)]
        public string DbConnectionString { get; private set; }

        public string LogConnectionString { get; private set; }

#endregion

#region Auth

        [DataMember(IsRequired = true)]
        public int AdminUserId { get; private set; }

        [DataMember(IsRequired = true)]
        public bool RunAsAdmin { get; private set; }

        [DataMember(IsRequired = true)]
        public bool RunAsStaff { get; private set; }

#endregion

#region eMail

        [DataMember(IsRequired = true)]
        public string EmailHost { get; private set; }

        [DataMember(IsRequired = true)]
        public string EmailFrom { get; private set; }

        [DataMember(IsRequired = true)]
        public bool ErrorEmail { get; private set; }

        [DataMember(IsRequired = true)]
        public string ErrorEmailLevel { get; private set; }

        [DataMember(IsRequired = true)]
        public string ErrorEmailRecipients { get; private set; }

        [DataMember(IsRequired = true)]
        public string ErrorEmailSubjectAppend { get; private set; }

#endregion

#region Development

        [DataMember]
        public bool DebugAuthLocal { get; private set; }

        [DataMember]
        public int DebugAuthLocalId { get; private set; }

        [DataMember]
        public bool DebugRunLocal { get; private set; }

#endregion

        public CommonConfig()
        {
            DebugAuthLocal = false;
            DebugRunLocal  = false;

            RunAsAdmin = false;
            RunAsStaff = false;
        }
    }
}
