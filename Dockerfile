FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /home_dir

COPY  ./WebApplication2/out/. ./

ENTRYPOINT [ "dotnet", "WebApi.dll" ]




