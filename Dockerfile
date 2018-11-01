FROM microsoft/dotnet:2.1-sdk AS builder

RUN mkdir /src
COPY ./BabyPredictions/ /src/
WORKDIR /src

RUN dotnet build -c Release
RUN dotnet publish --no-build --no-restore -c Release -o /dist

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime

COPY --from=builder /dist/ /app
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "BabyPredictions.dll" ]
