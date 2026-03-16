FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/KTGKNhom8.Web.Mvc/KTGKNhom8.Web.Mvc.csproj", "src/KTGKNhom8.Web.Mvc/"]
COPY ["src/KTGKNhom8.Web.Core/KTGKNhom8.Web.Core.csproj", "src/KTGKNhom8.Web.Core/"]
COPY ["src/KTGKNhom8.Application/KTGKNhom8.Application.csproj", "src/KTGKNhom8.Application/"]
COPY ["src/KTGKNhom8.Core/KTGKNhom8.Core.csproj", "src/KTGKNhom8.Core/"]
COPY ["src/KTGKNhom8.EntityFrameworkCore/KTGKNhom8.EntityFrameworkCore.csproj", "src/KTGKNhom8.EntityFrameworkCore/"]
WORKDIR "/src/src/KTGKNhom8.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/KTGKNhom8.Web.Mvc", "src/KTGKNhom8.Web.Mvc"]
COPY ["src/KTGKNhom8.Web.Core", "src/KTGKNhom8.Web.Core"]
COPY ["src/KTGKNhom8.Application", "src/KTGKNhom8.Application"]
COPY ["src/KTGKNhom8.Core", "src/KTGKNhom8.Core"]
COPY ["src/KTGKNhom8.EntityFrameworkCore", "src/KTGKNhom8.EntityFrameworkCore"]
WORKDIR "/src/src/KTGKNhom8.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "KTGKNhom8.Web.Mvc.dll"]
