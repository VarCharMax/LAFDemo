FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src

COPY *.sln .
COPY MySQLRESTDataProvider/. ./MySQLRESTDataProvider/
COPY MySQLDatastore/. ./MySQLDatastore/
COPY Classes/. ./Classes/
COPY Services/. ./Services/
COPY Middleware/. ./Middleware/
COPY BusinessLogic/. ./BusinessLogic/
COPY MVC/. ./MVC/

WORKDIR /src/MySQLRESTDataProvider

RUN dotnet restore --runtime linux-x64
# RUN dotnet build -c Release -o /app/build

# FROM build
RUN dotnet publish -c release -o /app/publish \
  --runtime linux-x64 \
  --no-restore \
  --self-contained true \
  /p:IncludeAllContentForSelfExtract=true \
  /p:PublishTrimmed=true \
  /p:PublishSingleFile=true


FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine3.19 AS final

WORKDIR /app
COPY MySQLRESTDataProvider/Scripts/. ./
COPY MySQLRESTDataProvider/sudoers /etc
COPY MySQLRESTDataProvider/node_modules/wait-for-it.sh/bin/wait-for-it ./wait-for-it.sh

RUN chmod +x wait-for-it.sh \
  && chmod +x installscript.sh \
  && chmod +x create-dotnet-user.sh \
  && ./installscript.sh \
  && /usr/bin/expect ./create-dotnet-user.sh

USER dotnetuser
EXPOSE 80/tcp 443/tcp

COPY --from=build /app/publish ./

ENV WAITHOST=mysql WAITPORT=3306

# Wait for database to come online.
# ENTRYPOINT ./wait-for-it.sh $WAITHOST:$WAITPORT --timeout=0 && MySQLRESTDataProvider

ENTRYPOINT ["tail", "-f", "/dev/null"]
