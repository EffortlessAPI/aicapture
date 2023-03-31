FROM public.ecr.aws/lambda/nodejs:16 AS setup-image
# Copy function code
COPY index.js ${LAMBDA_TASK_ROOT}
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
RUN yum install dotnet-sdk-3.1 -y
RUN yum install tar -y
RUN yum install gzip -y
ENV PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/share/dotnet:/opt/awscli/bin"
RUN dotnet --version
RUN curl -fsSL https://rpm.nodesource.com/setup_16.x | bash -
RUN yum install -y nodejs
RUN yum install -y git

FROM setup-image
RUN git clone -b lambda https://github.com/EffortlessAPI/aicapture.git /aicapture
WORKDIR /aicapture
RUN npm install -g .
WORKDIR /
  
# Set the CMD to your handler (could also be done as a parameter override outside of the Dockerfile)
CMD [ "index.handler" ]