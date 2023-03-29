FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PressureApp/PressureApp.csproj", "PressureApp/"]
RUN dotnet restore "PressureApp/PressureApp.csproj"
COPY . .
WORKDIR "/src/PressureApp"
RUN dotnet build "PressureApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PressureApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PressureApp.dll"]
