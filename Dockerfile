FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
ENV SOLUTION_DLL="WebApi.dll"

WORKDIR /home_dir

COPY  ./artifacts/. ./
ENTRYPOINT ["dotnet ",${SOLUTION_DLL}]


