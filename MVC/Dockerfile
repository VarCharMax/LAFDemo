FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS publish
WORKDIR /src

COPY *.sln .
COPY MySQLRESTDataProvider/. ./MySQLRESTDataProvider/
COPY Classes/. ./Classes/
COPY Services/. ./Services/
COPY Middleware/. ./Middleware/
COPY BusinessLogic/. ./BusinessLogic/
COPY MVC/. ./MVC/

WORKDIR /src/MVC

RUN dotnet restore --runtime linux-x64

RUN dotnet publish -c Release -o /app/publish \
  --runtime linux-x64 \
  --no-restore \
  --self-contained true \
  /p:IncludeAllContentForSelfExtract=true \
  /p:PublishTrimmed=true \
  /p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine3.19 AS final

WORKDIR /app
COPY ./MVC/Scripts/ ./
COPY ./MVC/sudoers /etc

RUN chmod +x installscript.sh \
  && chmod +x create-dotnet-user.sh \
  && ./installscript.sh \
  && /usr/bin/expect ./create-dotnet-user.sh

USER dotnetuser
EXPOSE 80/tcp 443/tcp

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MVC"]
