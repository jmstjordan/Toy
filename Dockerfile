FROM mcr.microsoft.com/dotnet/core/sdk:2.1

COPY Toy/* Toy/
COPY Toy.Tests/* Toy.Tests/
WORKDIR /Toy
RUN dotnet build

ENTRYPOINT ["dotnet", "run"]