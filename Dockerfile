# Etapa 1: Imagen base con el runtime de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa 2: Imagen para compilar la API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API_FLORERIAOMEGA/API_FLORERIAOMEGA.csproj", "API_FLORERIAOMEGA/"]
RUN dotnet restore "API_FLORERIAOMEGA/API_FLORERIAOMEGA.csproj"
COPY . .
WORKDIR "/src/API_FLORERIAOMEGA"
RUN dotnet build "API_FLORERIAOMEGA.csproj" -c Release -o /app/build

# Etapa 3: Publicación
FROM build AS publish
RUN dotnet publish "API_FLORERIAOMEGA.csproj" -c Release -o /app/publish

# Etapa 4: Imagen final con la API lista para ejecutar
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_FLORERIAOMEGA.dll"]
