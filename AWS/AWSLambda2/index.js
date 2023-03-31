const { spawn } = require('child_process');
const { log } = require('console');
const { randomUUID } = require('crypto');
const fs = require('fs');

exports.handler = async (event) => {
    log("current event:", event);
    const uuid = randomUUID();
    const filePath = `/tmp/${uuid}/aicapture.json`;

    fs.mkdirSync(`/tmp/${uuid}`, { recursive: true });
    fs.writeFileSync(filePath, '{}');
    process.env.CurrentWorkingDirectory = `/tmp/${uuid}`;
    const input = event.body || '-help';
    return new Promise((resolve, reject) => {
        const aicProcess = spawn('aic', [input]);
        log("current input:", input);
        let output = '';
        aicProcess.stdout.on('data', (data) => {
            output += data.toString();
        });

        aicProcess.stderr.on('data', (data) => {
            console.error(`stderr: ${data}`);
        });

        aicProcess.on('close', (code) => {
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
