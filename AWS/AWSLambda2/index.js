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
    
    //check if input is xml, if it is write it to tmp folder
    if (event.body.startsWith('<')) {
        const xmlFilePath = `${dirPath}/transpilerequest.xml`;
        fs.writeFileSync(xmlFilePath, event.body);
        //read the file, if there is a fileset node, write those out to their own files

    }

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
                const xmlContents = fs.readFileSync(`${dirPath}/${xmlFile}`, 'utf8');
                //write the xmlfile into the output variable
                output = xmlContents;
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
