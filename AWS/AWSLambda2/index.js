const { spawn } = require('child_process');
const { log } = require('console');
const { randomUUID } = require('crypto');
const fs = require('fs');

const transpileRequest = {
    //to do...
    InputFileSet: {},
    ZippedInputFileSet: {},
    OutputFileSet: {},
}


exports.handler = async (event) => {
    log("current event:", event);
    const uuid = randomUUID();
    const dirPath = `/tmp/${uuid}`;
    const filePath = `${dirPath}/aicapture.json`;

    fs.mkdirSync(dirPath, { recursive: true });
    fs.writeFileSync(filePath, '{}');
    process.env.CurrentWorkingDirectory = dirPath;
    const input = event.body || '-help';
    return new Promise((resolve, reject) => {
        const aicProcess = spawn('aic', [input, `-p path=${dirPath}`, `-sc`]);
        log("current input:", input);
        let output = '';
        aicProcess.stdout.on('data', (data) => {
            output += data.toString();
        });

        aicProcess.stderr.on('data', (data) => {
            console.error(`stderr: ${data}`);
        });



        aicProcess.on('close', (code) => {
            //find a .xml file, and write it to the stdout
            const xmlFile = fs.readdirSync(dirPath).find(file => file.endsWith('.xml'));
            if (xmlFile) {
                //write the xmlfile into the output variable
                output = xmlFile;
            }
            if (code === 0) {
                resolve({
                    statusCode: 200,
                    body: output,
                });
            } else {
                reject({
                    statusCode: 500,
                    body: `Process exited with code ${code}`,
                });
            }
        });

        aicProcess.stdin.write('a');

        aicProcess.stdin.end();
    });
};
