FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY ./ /app
WORKDIR /app

RUN dotnet restore ./DotUrl/DotUrl.csproj
RUN dotnet build ./DotUrl/DotUrl.csproj
EXPOSE 5000/tcp
EXPOSE 5001/tcp
RUN dotnet run --project ./DotUrl/DotUrl.csproj