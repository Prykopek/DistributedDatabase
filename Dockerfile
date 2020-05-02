FROM mcr.microsoft.com/dotnet/core/sdk:3.1

COPY ./DistributedDatabase.csproj /app/
WORKDIR /app/
RUN dotnet --info
RUN dotnet restore
ADD ./ /app/
RUN dotnet publish -c DEBUG -o out
ENTRYPOINT ["dotnet", "out/DistributedDatabase.dll"]
