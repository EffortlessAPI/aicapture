﻿/*******************************************
 Initially Generated by SSoT.me - codee42 & odxml42
 Created By: EJ Alexandra - 2017
             An Abstract Level, llc
 License:    Mozilla Public License 2.0
 *******************************************/
using CLI;
using Plossum.CommandLine;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RMQActors;
using SSoTme.OST.Lib.DataClasses;
using SSoTme.OST.Lib.Extensions;
using SSoTme.OST.Lib.SassySDK.Derived;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SSoTme.OST.Lib.CLIOptions
{


    public partial class SSoTmeCLIHandler
    {
        private SSOTMEPayload result;

        public SMQAccountHolder AccountHolder { get; private set; }
        public DMProxy CoordinatorProxy { get; private set; }


        private SSoTmeCLIHandler()
        {
            this.account = "";
            this.waitTimeout = 30000;
            this.input = new List<string>();
            this.parameters = new List<string>();
            this.addSetting = new List<string>();
            this.removeSetting = new List<string>();
        }

        public static SSoTmeCLIHandler CreateHandler(string commandLine)
        {
            var cliOptions = new SSoTmeCLIHandler();
            cliOptions.commandLine = commandLine;
            cliOptions.ParseCommand();
            return cliOptions;
        }


        public static SSoTmeCLIHandler CreateHandler(string[] args)
        {
            var cliOptions = new SSoTmeCLIHandler();
            cliOptions.args = args;
            cliOptions.ParseCommand();
            return cliOptions;
        }


        private void ParseCommand()
        {
            try
            {

                CommandLineParser parser = new CommandLineParser(this);
                if (!String.IsNullOrEmpty(this.commandLine)) parser.Parse(this.commandLine, false);
                else parser.Parse(this.args, false);

                this.HasRemainingArguments = parser.RemainingArguments.Any();

                bool continueToLoad = false;

                if (String.IsNullOrEmpty(this.transpiler))
                {
                    this.transpiler = parser.RemainingArguments.FirstOrDefault().SafeToString();
                    if (this.transpiler.Contains("/"))
                    {
                        this.account = this.transpiler.Substring(0, this.transpiler.IndexOf("/"));
                        this.transpiler = this.transpiler.Substring(this.transpiler.IndexOf("/") + 1);
                    }
                }

                var additionalArgs = parser.RemainingArguments.Skip(1).ToList();
                var pathParam = this.parameters.Find(param =>            
                    param.StartsWith("path=")
                );
                pathParam = pathParam.Replace("path=", "");
                Environment.CurrentDirectory = pathParam;
                for (var i = 0; i < additionalArgs.Count; i++)
                {
                    this.parameters.Add(String.Format("param{0}={1}", i + 1, additionalArgs[i]));
                }

                if (this.help)
                {

                    Console.WriteLine(parser.UsageInfo.GetHeaderAsString(78));

                    Console.WriteLine("\n\nSyntax: aicapture [account/]transpiler [Options]\n\n");

                    Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
                    this.SuppressTranspile = true;
                }
                else if (this.init)
                {
                    if (String.IsNullOrEmpty(this.projectName))
                    {
                        this.projectName = Path.GetFileName(Environment.CurrentDirectory);
                    }

                    var force = this.args.Count() == 2 &&
                                this.args[1] == "force";

                    DataClasses.AICaptureProject.Init(force, this.projectName);

                    continueToLoad = true;
                    this.build = true;
                }
                else if (parser.HasErrors)
                {
                    var curColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(parser.UsageInfo.GetErrorsAsString(78));
                    this.ParseResult = -1;
                    Console.ForegroundColor = curColor;
                    this.SuppressTranspile = true;
                }
                else if (this.authenticate || this.discuss)
                {
                    continueToLoad = false;
                }
                else continueToLoad = true;

                // Check for api keys

                if (continueToLoad)
                {
                    if (String.IsNullOrEmpty(this.setAccountAPIKey) && !this.help && !this.authenticate)
                    {
                        this.AICaptureProject = SSoTmeProject.LoadOrFail(new DirectoryInfo(Environment.CurrentDirectory), false);

                        foreach (var projectSetting in this.AICaptureProject.ProjectSettings)
                        {
                            if (!this.parameters.Any(anyParam => anyParam.StartsWith(String.Format("{0}=", projectSetting.Name))))
                            {
                                this.parameters.Add(String.Format("{0}={1}", projectSetting.Name, projectSetting.Value));
                            }
                        }
                    }

                    this.LoadInputFiles();

                    var key = SSOTMEKey.GetSSoTmeKey(this.runAs);
                    if (key.APIKeys.ContainsKey(this.account))
                    {
                        this.parameters.Add(String.Format("apiKey={0}", key.APIKeys[this.account]));
                    }

                    if (!ReferenceEquals(this.FileSet, null))
                    {
                        this.ZFSFileSetFile = this.FileSet.FileSetFiles.FirstOrDefault(fodFileSetFile => fodFileSetFile.RelativePath.EndsWith(".zfs", StringComparison.OrdinalIgnoreCase));
                    }
                }
            }
            catch (Exception ex)
            {
                var curColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\n********************************\nERROR: {0}\n********************************\n\n", ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\n\nPress any key to continue...\n");
                Console.WriteLine("\n\n");
                Console.ForegroundColor = curColor;

                if (Console.IsInputRedirected)
                {
                    Console.ReadLine();
                }
                else
                {
                    Console.ReadKey();
                }

            }
        }

        public int TranspileProject(ProjectTranspiler projectTranspiler = null)
        {
            bool updateProject = false;
            try
            {
                var hasRemainingArguments = this.HasRemainingArguments;
                var zfsFileSetFile = this.ZFSFileSetFile;
                if (this.authenticate)
                {
                    if (!this.CheckAuthenticationNow())
                    {
                        return -1;
                    }
                }
                else if (this.describe)
                {
                    this.AICaptureProject.Describe(Environment.CurrentDirectory);
                }
                else if (this.descibeAll)
                {
                    this.AICaptureProject.Describe();
                }
                else if (this.listSettings)
                {
                    this.AICaptureProject.ListSettings();
                }
                else if (this.addSetting.Any())
                {
                    foreach (var setting in this.addSetting)
                    {
                        this.AICaptureProject.AddSetting(setting);
                    }

                    this.AICaptureProject.Save();
                }
                else if (this.removeSetting.Any())
                {
                    foreach (var setting in this.removeSetting)
                    {
                        this.AICaptureProject.RemoveSetting(setting);
                    }
                    this.AICaptureProject.Save();
                }
                else if (!String.IsNullOrEmpty(this.setAccountAPIKey))
                {
                    var key = SSOTMEKey.GetSSoTmeKey(this.runAs);
                    if (ReferenceEquals(key.APIKeys, null)) key.APIKeys = new Dictionary<String, String>();
                    var apiKey = this.setAccountAPIKey.SafeToString().Replace("=", "/");
                    var values = apiKey.Split("/".ToCharArray());
                    if (!values.Skip(1).Any()) throw new Exception("Sytnax: -setAccountAPIKey=account/KEY");
                    key.APIKeys[values[0]] = values[1];
                    SSOTMEKey.SetSSoTmeKey(key, this.runAs);
                }
                else if (!String.IsNullOrEmpty(this.execute))
                {
                    this.ProcessCommandLine(this.execute);
                    if (this.install)
                    {
                        object o = 1; // Need to write code to save the execute command line
                    }
                }
                else if (this.build)
                {
                    this.AICaptureProject.Rebuild(Environment.CurrentDirectory, this.includeDisabled);
                    if (this.checkResults) this.AICaptureProject.CreateDocs();
                }
                else if (this.buildAll)
                {
                    this.AICaptureProject.Rebuild(this.includeDisabled);
                    this.AICaptureProject.CreateDocs();

                }
                else if (this.discuss)
                {
                    var aicManager = AICManager.Create(this.Auth0SID);
                    aicManager.Start();
                }
                else if (this.checkResults || this.createDocs && !hasRemainingArguments)
                {
                    if (this.checkResults) this.AICaptureProject.CheckResults();
                    else this.AICaptureProject.CreateDocs();
                    updateProject = true;
                }
                else if (this.clean && !ReferenceEquals(zfsFileSetFile, null))
                {
                    var zfsFI = new FileInfo(zfsFileSetFile.RelativePath);
                    if (zfsFI.Exists)
                    {
                        var zippedFileSet = File.ReadAllBytes(zfsFI.FullName);
                        zippedFileSet.CleanZippedFileSet();
                        if (!this.preserveZFS) zfsFI.Delete();
                    }
                }
                else if (this.clean && !hasRemainingArguments)
                {
                    this.AICaptureProject.Clean(Environment.CurrentDirectory, this.preserveZFS);
                }
                else if (this.cleanAll && !hasRemainingArguments)
                {
                    this.AICaptureProject.Clean(this.preserveZFS);
                }
                else if (!hasRemainingArguments && !this.clean)
                {
                    ShowError("Missing argument name of transpiler");
                    return -1;
                }
                else
                {
                    StartTranspile();

                    if (!ReferenceEquals(result.Exception, null))
                    {
                        ShowError("ERROR: " + result.Exception.Message);
                        ShowError(result.Exception.StackTrace);
                        return -1;
                    }
                    else
                    {
                        var finalResult = 0;

                        if (!ReferenceEquals(result.Transpiler, null))
                        {
                            Console.WriteLine("\n\nTRANSPILER MATCHED: {0}\n\n", result.Transpiler.Name);
                        }

                        if (this.clean) result.CleanFileSet();
                        else
                        {
                            finalResult = result.SaveFileSet(this.skipClean);
                            updateProject = true;
                        }
                        return finalResult;

                    }
                }

                return 0;
            }
            finally
            {
                if (!ReferenceEquals(AccountHolder, null)) AccountHolder.Disconnect();
                if (updateProject)
                {
                    if (this.install) this.AICaptureProject.Install(result, this.transpilerGroup);
                    else if (!ReferenceEquals(projectTranspiler, null))
                    {
                        this.AICaptureProject.Update(projectTranspiler, result);
                    }
                }
            }
        }


        private bool CheckAuthenticationNow()
        {
            Console.WriteLine("Authenticating with auth0.");
            if (!String.IsNullOrEmpty(this.Auth0SID))
            {
                this.PrintAuth();
                Console.WriteLine("Already authenticated.  Reauthenticate now? y/N");
                if (Console.ReadKey().Key != ConsoleKey.Y) return true;
            }
            
            var startInfo = new ProcessStartInfo();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                startInfo.FileName = "cmd";
                startInfo.Arguments = $"/c start chrome {this.AICaptureHost}/cli-login";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                startInfo.FileName = "xdg-open";
                startInfo.Arguments = $"{this.AICaptureHost}/cli-login";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                startInfo.FileName = "open";
                startInfo.Arguments = $"{this.AICaptureHost}/cli-login";
            }
            else
            {
                throw new InvalidOperationException("Unsupported operating system");
            }

            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.CreateNoWindow = true;

            var process = new Process { StartInfo = startInfo };
            process.Start();
            process.StandardInput.Close();

            var result = LocalServer.StartServerAsync("http://localhost:8080/complete-cli/");
            result.Wait(300000);
            if (result.IsCompleted)
            {
                object jwtFromRedirect = $"result: {result.Result}";
                this.Auth0SID = result.Result;
                this.PrintAuth();
                return true;
            }
            else
            {
                ShowError("Syntax: aicapture unable to authenticate within the specified timeout period.");
                return false;
            }
        }

        private void PrintAuth()
        {
            var webClient = new WebClient();
            var payload = $"{{ \"Auth0SID\":\"{this.Auth0SID}\" }}";
            var resultBytes = webClient.UploadData($"{this.AICaptureHost}/whoami", Encoding.UTF8.GetBytes(payload));
            var jwt = Encoding.UTF8.GetString(resultBytes);
            Console.WriteLine(jwt);
        }

        private void PublicUser_ReplyTo(object sender, PayloadEventArgs<SSOTMEPayload> e)
        {
            if (e.Payload.IsLexiconTerm(LexiconTermEnum.publicuser_ping_ssotmecoordinator))
            {
                this.CoordinatorProxy = new DMProxy(e.Payload.DirectMessageQueue);
                var payload = this.PublicUser.CreatePayload();
                throw new Exception("This: payload.EmailAddress = this.emailAddress; isn't really a thing anymore.");
                this.PublicUser.PublicUserAuthenticate(payload, this.CoordinatorProxy);
            }
            else if (e.Payload.IsLexiconTerm(LexiconTermEnum.publicuser_authenticate_ssotmecoordinator))
            {
                Console.WriteLine("We sent an auth key to {0}.", e.Payload.EmailAddress);
                Console.Write("AUTH Code: ");
                e.Payload.AuthToken = Console.ReadLine();
                if (!String.IsNullOrEmpty(e.Payload.AuthToken))
                {
                    Console.WriteLine("Validating AUTH Code.  One moment...");
                    this.PublicUser.PublicUserValidateAuthToken(e.Payload, this.CoordinatorProxy);
                }
                else
                {
                    Console.WriteLine("Aborting. No AUTH Code recieved.");
                }
            }
            else if (e.Payload.IsLexiconTerm(LexiconTermEnum.publicuser_validateauthtoken_ssotmecoordinator))
            {
                if (String.IsNullOrEmpty(e.Payload.Secret))
                {
                    Console.WriteLine("AUTH Code Validated Failed.");
                }
                else
                {
                    Console.WriteLine("AUTH Code Validated.");
                    try
                    {
                        var key = SSOTMEKey.GetSSoTmeKey(this.account);
                        key.EmailAddress = e.Payload.EmailAddress;
                        key.Secret = e.Payload.Secret;
                        SSOTMEKey.SetSSoTmeKey(key, this.account);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: {0}", ex.Message);
                    }
                }
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                this.PublicUser.Disconnect();
            }
        }

        private static void ShowError(string msg)
        {
            var curColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = curColor;
        }

        private void ProcessCommandLine(string commandLine)
        {
            var process = Process.Start(commandLine);
            process.WaitForExit(this.waitTimeout);
            if (!process.HasExited)
            {
                process.Close();
                throw new Exception(String.Format("Timed out waiting for process to complete: {0}", commandLine));
            }

        }

        internal void LoadOutputFiles(String lowerHyphoneName, String basePath, bool includeContents)
        {
            var rootPath = ReferenceEquals(this.AICaptureProject, null) ? Environment.CurrentDirectory : this.AICaptureProject.RootPath;
            var zfsRelativePath = String.Format("{0}.zfs", lowerHyphoneName);
            basePath = basePath.Trim("\\/".ToCharArray());
            var zfsFileName = Path.Combine(rootPath, ".ssotme", basePath, zfsRelativePath);

            var zfsFI = new FileInfo(zfsFileName);
            if (zfsFI.Exists)
            {
                var fileSetXml = File.ReadAllBytes(zfsFI.FullName).UnzipToString();
                var fs = fileSetXml.ToFileSet();
                foreach (var fsf in fs.FileSetFiles)
                {
                    var relativePath = fsf.RelativePath.Trim("\\/\r\n\t ".ToCharArray());
                    fsf.OriginalRelativePath = Path.Combine(basePath, relativePath).Replace("\\", "/");
                    if (!includeContents) fsf.ClearContents();
                }
                this.OutputFileSet = fs;
            }
        }

        private void StartTranspile()
        {
            this.AccountHolder = new SMQAccountHolder();
            var currentSSoTmeKey = SSOTMEKey.GetSSoTmeKey(this.runAs);
            result = null;

            this.AccountHolder.ReplyTo += AccountHolder_ReplyTo;
            this.AccountHolder.Init(currentSSoTmeKey.EmailAddress, currentSSoTmeKey.Secret);


            var waitForCook = Task.Factory.StartNew(() =>
            {
                while (ReferenceEquals(result, null)) Thread.Sleep(100);
            });

            waitForCook.Wait(this.waitTimeout);

            if (ReferenceEquals(result, null))
            {
                result = AccountHolder.CreatePayload();
                result.Exception = new TimeoutException("Timed out waiting for cook");
            }
            result.SSoTmeProject = this.AICaptureProject;
        }

        public string inputFileContents = "";
        public string transpiler = "";
        public string inputFileSetXml;
        public string[] args;
        public string commandLine;
        private SMQPublicUser PublicUser;
        public string inputFileSetJson;

        public FileSet FileSet { get; private set; }
        public bool HasRemainingArguments { get; private set; }
        public FileSetFile ZFSFileSetFile { get; private set; }
        public SSoTmeProject AICaptureProject { get; set; }
        public int ParseResult { get; private set; }
        public FileSet OutputFileSet { get; private set; }
        public bool SuppressTranspile { get; private set; }
        public string AICaptureHost
        {
            get
            {
#if DEBUG
                return "https://localhost:7033";
#else
                return "https://aicapture.io";
#endif
            }
        }
        public string Auth0SID
        {
            get
            {
                if (Auth0TokenFI.Exists) return File.ReadAllText(Auth0TokenFI.FullName);
                else return String.Empty;
            }
            set
            {
                File.WriteAllText(Auth0TokenFI.FullName, value);
            }
        }

        private static FileInfo Auth0TokenFI
        {
            get
            {
                var auth0TokenPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".aicapture", "auth0.key");
                var auth0TokenFI = new FileInfo(auth0TokenPath);
                if (!auth0TokenFI.Directory.Exists) auth0TokenFI.Directory.Create();
                return auth0TokenFI;
            }
        }


        public void LoadInputFiles()
        {
            var fs = new FileSet();
            if (!ReferenceEquals(this.input, null) && this.input.Any())
            {
                foreach (var input in this.input)
                {
                    if (!String.IsNullOrEmpty(input))
                    {
                        var inputFilePatterns = input.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (var filePattern in inputFilePatterns)
                        {
                            this.ImportFile(filePattern, fs);
                        }

                        if (fs.FileSetFiles.Any()) this.inputFileContents = fs.FileSetFiles.First().FileContents;

                    }
                }
            }
            this.inputFileSetXml = fs.ToXml();
            this.FileSet = fs;
        }

        private void ImportFile(string filePattern, FileSet fs)
        {
            var fileNameReplacement = String.Empty;
            if (filePattern.Contains("="))
            {
                fileNameReplacement = filePattern.Substring(0, filePattern.IndexOf("="));
                filePattern = filePattern.Substring(filePattern.IndexOf("=") + 1);
            }
            var di = new DirectoryInfo(Path.Combine(".", Path.GetDirectoryName(filePattern)));
            filePattern = Path.GetFileName(filePattern);

            var matchingFiles = new FileInfo[] { };
            if (di.Exists)
            {
                matchingFiles = di.GetFiles(filePattern);
            }
            if (!matchingFiles.Any())
            {
                var curColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\nWARNING:\n\n - No INPUT files matched {0} in {1}\n", filePattern, di.FullName);
                var fsf = new FileSetFile();
                fsf.RelativePath = Path.GetFileName(filePattern);
                fs.FileSetFiles.Add(fsf);

                Console.ForegroundColor = curColor;

            }

            foreach (var fi in matchingFiles)
            {
                var fsf = new FileSetFile();
                fsf.RelativePath = String.IsNullOrEmpty(fileNameReplacement) ? fi.Name : fileNameReplacement;
                fsf.OriginalRelativePath = fi.FullName.Substring(this.AICaptureProject.RootPath.Length).Replace("\\", "/");
                fs.FileSetFiles.Add(fsf);

                if (fi.Exists)
                {
                    if (fi.IsBinaryFile())
                    {
                        fsf.ZippedBinaryFileContents = File.ReadAllBytes(fi.FullName).Zip();
                    }
                    else
                    {
                        fsf.ZippedFileContents = File.ReadAllText(fi.FullName).Zip();
                    }
                }
                else
                {
                    var curColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("INPUT Format: {0} did not match any files in {1}", filePattern, di.FullName);
                    Console.ForegroundColor = curColor;
                }
            }
        }



        private void AccountHolder_ReplyTo(object sender, SassyMQ.Lib.RabbitMQ.PayloadEventArgs<SSOTMEPayload> e)
        {
            if (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_ping_ssotmecoordinator))
            {
                CoordinatorProxy = new DMProxy(e.Payload.DirectMessageQueue);
                Console.WriteLine("Got ping response");
                var payload = AccountHolder.CreatePayload();
                payload.SaveCLIOptions(this);
                payload.TranspileRequest = new TranspileRequest();
                payload.TranspileRequest.ZippedInputFileSet = this.inputFileSetXml.Zip();
                payload.CLIInputFileContents = String.Empty;
                AccountHolder.AccountHolderCommandLineTranspile(payload, CoordinatorProxy);
            }
            else if (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator) ||
                    (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator)))
            {
                result = e.Payload;
            }
        }
    }
}
