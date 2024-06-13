# Use the official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 81

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExamManagement.csproj", "."]
RUN dotnet restore "./ExamManagement.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ExamManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExamManagement.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamManagement.dll"]
