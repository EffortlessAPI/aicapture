#!/usr/bin/env node

'use strict';

const { spawn } = require('child_process');

spawn('dotnet', [`/var/task/aicapture/Windows/CLI/bin/Release/netcoreapp3.1/AICapture.OST.CLI.dll`, process.argv.slice(2).join(' ')], { stdio: 'inherit' });