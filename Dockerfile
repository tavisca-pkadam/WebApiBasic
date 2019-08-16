FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /home_dir

COPY  . ./


RUN dotnet publish -c Release -o out 

ENTRYPOINT [ "dotnet", "/home_dir/WebApplication2/out/WebApi.dll" ]




