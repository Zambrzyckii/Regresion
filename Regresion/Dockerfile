FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

RUN apt-get update && apt-get install -y python3 python3-pandas

COPY Encoding/encode.py .

COPY Regresion.sln ./
COPY Regresion/Regresion.csproj ./Regresion/

RUN dotnet restore ./Regresion/Regresion.csproj

COPY Regresion/ ./Regresion/

RUN dotnet publish ./Regresion/Regresion.csproj -c Release -o out

ENTRYPOINT ["dotnet", "out/Regresion.dll"]
