const { spawn } = require('child_process');
const { log } = require('console');

exports.handler = async (event) => {

    const input = event.input || '-help';
    return new Promise((resolve, reject) => {
        const process = spawn('aic', [input]);
        log(event);
        let output = '';
        process.stdout.on('data', (data) => {
            output += data.toString();
        });

        process.stderr.on('data', (data) => {
            console.error(`stderr: ${data}`);
        });

        process.on('close', (code) => {
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

        process.stdin.write('a');
        process.stdin.end();
    });
};
