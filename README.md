# AI Capture CLI OST
[AICapture Platform](http://aicapture.io)
The Single Source of Truth Toolbox!

## Additional Docs
In addition to the summary below - there is [additional documentation](https://aicapture.github.io/AICapture-Open-Source-Tools/) on
the github.io site.

## About A Single Source of Truth
Software based on a single source of truth is software that does not tolorate duplication of 
Authority.  "Source Code" is frequently a terrible place for decisions about how software
should work.  SSoT development is based on the notion that there should always be one,
Authoritative place for these decisions.  

*AICapture* is a command line package manager like NPM, Bower, Nuget, etc - however, the packages
delivered are dynamic in nature.  If 100 projects install a bower package, they all get the
same bytes.

If, by contrast, 100 projects each install the same SSoT.me package, they will all get
different bytes - because they will each be required to provide a single source of truth
which describes their project.  Since each project will start with a different SSoT - 
they will each get a package which works with their project.  It will be the same KIND of 
content that the other projects all needed, but will differ in ways specific to each project.

Similarly - if you one project installs 10 AICapture packages, they will all match each other,
because they are all derived from the same SSoT.  And any time that SSoT changes, all 10
packages will also update themselves to match the new "truth".

## Open Source Tools
These tools are open source.  Eventually, the SSoT.me Website, Coordinator as well as the Codee42 
and Odxml42 toolsets will also be offered as open source tools as well.  It's just a matter of 
getting them cleaned up a little bit first.

## SSoT.me Architecture
SSoT.me is really a directory of Dynamic Packages (Transpilers).  The distinction between
static package managers like NPM and Bower is that each SSoT.me tool always requires INPUT.
That input is then turned into something else.  By Connecting these tools together end-to-end
a "Transpiler Pipeline" can be created which, in a very dynamic, responsive and flexible way
turns A into B.  The transaction always follows this basic script though:

1. The CLI gathers together the requeseted "input" (files, parameters, options, etc)
2. The CLI packages everything into a "Zipped Json" *Transpile Request*
3. The *Transpile Request* is sent to the SSoT.me coordinator
4. The SSoT.me Coordinator determins which tool is being requested
5. (down the road - The Coordinator Validates subscription and/or charges consumer)
6. The Coordinator forwards the request to a *Transpiler Host* for the requested tool
7. The Transpiler host processes the *Transpile Request*
8. The Transpiler sends the Output directly back to the requesting CLI (the Coordinator is not 
        involved in the response).


## Installation


Installing the Windows *AICapture executable* command line tool will automatically update the path to include the CLI.  

### Installation instructions for mac
If you already have a version of ssotme and/or aicaptureinstalled, do this first:

```
npm uninstall ssotme --force -g
npm uninstall aicapture --force -g
```

**Install npm using nvm**

curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.38.0/install.sh | bash
nvm install 18 (to install version 18)




**Install dotnet 3.1**

As of 10/23 with mac ventura 13.5, what worked was to 
  1. download an installer for dotnet core 3.1 macOS x64 from [here](https://dotnet.microsoft.com/en-us/download/dotnet/3.1

  2. and then add a link   (see [here](https://stackoverflow.com/questions/53030531/dotnet-command-not-found-in-mac)):
  ```
  ln -s /usr/local/share/dotnet/dotnet /usr/local/bin/  
  ``````


**install aicapture from source**
```
git clone https://github.com/effortlessapi/aicapture
cd aicapture
npm install -g .
```

test for success re install:
```aicapture -help```

Note: `aic` and `ssotme` will both work as aliases for aicapture

**authenticate**
Then authenticate:
```aic -authenticate```

This will open a webpage asking you to sign in. If you have not done this before,
you can set up a new account. If you sign in with google you will not need a new password.

The webpage will tell you to go back to your terminal window. You do not need to keep the 
auth0 debug key that prints.


Test for success:

```aicapture touppercase test```

success looks like:

```
> aicapture touppercase test


CONNECTED: S M Q Account Holder: Waiting for messages. To exit press CTRL+C

Got ping response
Connected account


TRANSPILER MATCHED: ToUpperCase


TEST
```

## Airtable Specific Configuration

Before running any airtable tools, you might see an error like this:

ERROR: apiKey parameter required

To address this, you'll need to provide your Airtable Personal Access Token to aicapture as follows:

`> aicapture -api airtable=patR3xfB...`

This will store the access token in $home/.ssotme/ssotme.key

**Note**: You can generate your airtable personal access token  by logging into airtable, clicking on your acct circle > `developer hub > personal access tokens.` 

**Note**: the personal access token needs read access for data.records and schema.bases for any airtable bases your tools need to access.

## Syntax: `aicapture -help`
This command will show the following help.

```
aicapture [account/]transpiler [parameters,...] [options]

options:
   -account, -a          The account which the transpiler belongs to
   -addSetting, -as      Adds a setting to the aicapture.json
   -build, -b            Build any transpilers in the
                         current folder (or children).
   -buildAll, -ba        Builds all transpilers in the project
   -checkResults, -cr    Checks the result of a build linking up input
                         and output files of the transpiles.  Creates a
                         SPXML file in the DSPXml folder of the project.
   -clean, -c            Don't output the final results - instead, clean
   -cleanAll, -ca        Don't output the final results - instead, clean
   -createDocs, -cd      Creates documentation based on a DSPXml
                         file created with the -checkResults flag.
   -descibeAll, -da      Descibe all of the transpiler in the project
   -describe, -d         Describes the current AICapture
                         project (and all transpilers)
   -emailAddress, -e     The email address for the account authenticating
   -execute, -exec       Executes the given command as a ProcessInfo.Start
   -help, -h             Show help about how to use the aicapture cli
   -includeDisabled,
   -id                   Include disabled tools in th ebuild
   -init                 Initialize the current folder as
                         the root of an SSOT.me project
   -input, -i            Input filename or comma separated list of file names
   -install              Saves the current command into the aicapture.json file
   -keyFile, -f          The keyfile to use.  By default
                         it looks for ~/.aicapture/aicapture.key.
   -listSettings, -ls    List of project settings
   -output, -o           Output filename
   -parameters, -p       A list of parameters
   -preserveZFS, -rz     Determines if the input should be preserved.
   -removeSetting, -rs   REmoves a setting from the aicapture project
   -runAs, -ra           Run as this user (look for this user's key file)
   -secret, -k           The secret associated with that email address
   -skipClean, -sc       Don't clean the output before cooking
   -uninstall            Removes the current command
                         from the AICapture project file
   -waitTimeout, -w      The amount of time to wait
                         for the command to continue
```

