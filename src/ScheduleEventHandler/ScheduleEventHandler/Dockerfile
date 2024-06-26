# Use the official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ScheduleEventHandler.csproj", "."]
RUN dotnet restore "./ScheduleEventHandler.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ScheduleEventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleEventHandler.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleEventHandler.dll"]
