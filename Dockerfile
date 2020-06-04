# ---- Run Build ----
# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY ./src /app/
WORKDIR /app/Pilllar.Vocal.Api/
RUN dotnet restore
RUN dotnet publish -c release -o out

# ---- Run App ----
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as release
WORKDIR /app
COPY --from=build /app/Pilllar.Vocal.Api/out .
ENTRYPOINT ["dotnet", "Pilllar.Vocal.Api.dll"]




