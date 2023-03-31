﻿/*******************************************
 Initially Generated by SSoT.me - codee42 & odxml42
 Created By: EJ Alexandra - 2017
             An Abstract Level, llc
 License:    Mozilla Public License 2.0
 *******************************************/
using Newtonsoft.Json;
using SassyMQ.Lib.RabbitMQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.SassySDK.Derived
{
    public class SSOTMEKey
    {
        public string EmailAddress { get; set; }
        public string Secret { get; set; }
        public static SSOTMEKey CurrentKey
        {
            get { return GetSSoTmeKey(); }
            set { SetSSoTmeKey(value); }
        }

        private static List<SSOTMEKey> _allKeys;
        public static List<SSOTMEKey> AllKeys
        {
            get
            {
                if (ReferenceEquals(_allKeys, null))
                {
                    _allKeys = new List<SSOTMEKey>();
                    foreach (var keyFile in SSoTmeDir.GetFiles("*.key"))
                    {
                        var keyFileName = $"{keyFile.Name.Split('_').Skip(1).FirstOrDefault()}";
                        var username = keyFileName.ToLower().Replace(".key", "");
                        var newKey = GetSSoTmeKey(username);
                        _allKeys.Add(newKey);
                    }
                }
                return _allKeys;
            }
            set { _allKeys = value; }
        }

        public static void SetSSoTmeKey(SSOTMEKey value, string account = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(account);
            String ssotmeJson = JsonConvert.SerializeObject(value, Formatting.Indented);
            File.WriteAllText(ssotmeKeyFI.FullName, ssotmeJson);
        }

        public static SSOTMEKey GetSSoTmeKey(string runAs = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(runAs);
            var ssotmeKey = default(SSOTMEKey);

            if (ssotmeKeyFI.Exists) ssotmeKey = JsonConvert.DeserializeObject<SSOTMEKey>(File.ReadAllText(ssotmeKeyFI.FullName));
            else ssotmeKey = new SSOTMEKey();

            return ssotmeKey;
        }

        public static DirectoryInfo SSoTmeDir
        {
            get
            {
                //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssotme");
                //if (path.IsReadOnlyFileSystem())
                //{
                var path = Path.Combine("/tmp", ".ssotme");
                //}
                var ssoTmeDir = new DirectoryInfo(path);
                if (!ssoTmeDir.Exists) ssoTmeDir.Create();
                return ssoTmeDir;
            }

        }

        private Dictionary<string, string> _APIKeys;
        public Dictionary<string, string> APIKeys
        {
            get
            {
                if (ReferenceEquals(_APIKeys, null)) _APIKeys = new Dictionary<string, string>();
                return _APIKeys;
            }
            set { _APIKeys = value; }
        }

        private static FileInfo GetKeyForAccount(string accountUsername)
        {
            var ssotmeKeyFI = default(FileInfo);
            if (String.IsNullOrEmpty(accountUsername)) ssotmeKeyFI = new FileInfo(Path.Combine(SSoTmeDir.FullName, "ssotme.key"));
            else ssotmeKeyFI = new FileInfo(Path.Combine(SSoTmeDir.FullName, String.Format("ssotme_{0}.key", accountUsername)));

            return ssotmeKeyFI;
        }

        public override string ToString()
        {
            return this.EmailAddress;
        }

        internal static void SetSSoTmeKey(object sSoTmeKey, string account)
        {
            throw new NotImplementedException();
        }
    }
}
