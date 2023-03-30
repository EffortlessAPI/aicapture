FROM public.ecr.aws/lambda/dotnet:6

WORKDIR /var/task

# This COPY command copies the .NET Lambda project's build artifacts from the host machine into the image. 
# The source of the COPY should match where the .NET Lambda project publishes its build artifacts. If the Lambda function is being built 
# with the AWS .NET Lambda Tooling, the `--docker-host-build-output-dir` switch controls where the .NET Lambda project
# will be built. The .NET Lambda project templates default to having `--docker-host-build-output-dir`
# set in the aws-lambda-tools-defaults.json file to "bin/Release/lambda-publish".
#
# Alternatively Docker multi-stage build could be used to build the .NET Lambda project inside the image.
# For more information on this approach checkout the project's README.md file.
COPY "bin/Release/net6.0"  .

WORKDIR /tmp/aicapture
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
RUN yum install dotnet-sdk-6.0 -y
ENV PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/share/dotnet:/opt/awscli/bin"
RUN dotnet --version
RUN yum install -y gcc-c++ make 
RUN curl -sL https://rpm.nodesource.com/setup_16.x | bash - 
RUN yum install -y nodejs
RUN yum install -y git
WORKDIR /var/task
RUN git clone -b lambda https://github.com/EffortlessAPI/aicapture.git
WORKDIR /var/task/aicapture
RUN npm install .

WORKDIR /var/task