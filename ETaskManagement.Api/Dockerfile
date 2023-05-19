FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ETaskManagement.Api/ETaskManagement.Api.csproj", "ETaskManagement.Api/"]
RUN dotnet restore "ETaskManagement.Api/ETaskManagement.Api.csproj"
COPY . .
WORKDIR "/src/ETaskManagement.Api"
RUN dotnet build "ETaskManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETaskManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETaskManagement.Api.dll"]
