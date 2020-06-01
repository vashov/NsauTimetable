# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY ./NsauT.Web/*.csproj NsauT.Web/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/NsauT.Web
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
EXPOSE 8080
CMD ["dotnet", "NsauT.Web.dll"]
