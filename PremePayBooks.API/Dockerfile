#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PremePayBooks.API/PremePayBooks.API.csproj", "PremePayBooks.API/"]
RUN dotnet restore "PremePayBooks.API/PremePayBooks.API.csproj"
COPY . .
WORKDIR "/src/PremePayBooks.API"
RUN dotnet build "PremePayBooks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PremePayBooks.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PremePayBooks.API.dll"]