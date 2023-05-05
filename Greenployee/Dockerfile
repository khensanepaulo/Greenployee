FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /Greenployee
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Greenployee.csproj", "."]
RUN dotnet restore "./Greenployee.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Greenployee.csproj" -c Release -o /Greenployee/build

FROM build AS publish
RUN dotnet publish "Greenployee.csproj" -c Release -o /Greenployee/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /Greenployee
COPY --from=publish /Greenployee/publish .
ENTRYPOINT ["dotnet", "Greenployee.dll"]