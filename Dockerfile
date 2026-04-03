FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore BCSSViewer.Wasm/BCSSViewer.Wasm.csproj
RUN dotnet publish BCSSViewer.Wasm/BCSSViewer.Wasm.csproj -c Release -o /app/publish

# Caddy runtime
FROM caddy:alpine

# static files
COPY --from=build /app/publish/wwwroot /usr/share/caddy

# custom caddy config
COPY Caddyfile /etc/caddy/Caddyfile
