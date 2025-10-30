# �tape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les d�pendances
COPY *.csproj ./
RUN dotnet restore
COPY . ./

# Publier le projet en Release
RUN dotnet publish -c Release -o out

# �tape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copier les fichiers publi�s depuis l'�tape build
COPY --from=build /app/out .

# Configurer l'URL pour Railway
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Lancer l'application
ENTRYPOINT ["dotnet", "ScanApp2.dll"]
