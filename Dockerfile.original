#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#WORKDIR /src
#COPY ["BananaReader_audiobooks_ms.csproj", "."]
#RUN dotnet restore "./BananaReader_audiobooks_ms.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "BananaReader_audiobooks_ms.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "BananaReader_audiobooks_ms.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BananaReader_audiobooks_ms.dll"]

FROM gcr.io/google-appengine/aspnetcore:3.1
ADD ./ /app 
ENV ASPNETCORE_URLS=http://*:${PORT} 
WORKDIR /app 
ENTRYPOINT [ "dotnet", "BananaReader_audiobooks_ms.csproj" ]