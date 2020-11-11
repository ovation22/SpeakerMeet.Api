#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM  mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /

COPY ["tests/SpeakerMeet.Tests.Integration.DB/SpeakerMeet.Tests.Integration.DB.csproj", "tests/SpeakerMeet.Tests.Integration.DB/"]
RUN dotnet restore "tests/SpeakerMeet.Tests.Integration.DB/SpeakerMeet.Tests.Integration.DB.csproj"

COPY . .
WORKDIR "/tests/SpeakerMeet.Tests.Integration.DB"

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN /bin/bash -c 'ls -la /wait; chmod +x /wait; ls -la /wait'

CMD /wait && dotnet test --logger xunit --results-directory ./TestResults