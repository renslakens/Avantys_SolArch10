# Use the official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExamEventHandler.csproj", "."]
RUN dotnet restore "./ExamEventHandler.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ExamEventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExamEventHandler.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamEventHandler.dll"]
