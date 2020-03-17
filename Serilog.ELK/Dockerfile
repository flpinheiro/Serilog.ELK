#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Serilog.ELK/Serilog.ELK.csproj", "Serilog.ELK/"]
RUN dotnet restore "Serilog.ELK/Serilog.ELK.csproj"
COPY . .
WORKDIR "/src/Serilog.ELK"
RUN dotnet build "Serilog.ELK.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Serilog.ELK.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Serilog.ELK.dll"]

FROM docker.elastic.co/beats/filebeat:7.6.1
COPY filebeat.yml /usr/share/filebeat/filebeat.yml
USER root
RUN chown root:filebeat /usr/share/filebeat/filebeat.yml
USER filebeat