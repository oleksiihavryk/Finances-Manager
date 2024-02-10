#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Server/OleksiiHavryk.Finances.API/OleksiiHavryk.Finances.API.csproj", "Server/OleksiiHavryk.Finances.API/"]
COPY ["Server/OleksiiHavryk.Finances.API.Data/OleksiiHavryk.Finances.API.Data.csproj", "Server/OleksiiHavryk.Finances.API.Data/"]
COPY ["Server/OleksiiHavryk.Finances.API.Domain/OleksiiHavryk.Finances.API.Domain.csproj", "Server/OleksiiHavryk.Finances.API.Domain/"]
RUN dotnet restore "Server/OleksiiHavryk.Finances.API/OleksiiHavryk.Finances.API.csproj"
COPY . .
WORKDIR "/src/Server/OleksiiHavryk.Finances.API"
RUN dotnet build "OleksiiHavryk.Finances.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OleksiiHavryk.Finances.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OleksiiHavryk.Finances.API.dll"]