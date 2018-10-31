#!/usr/bin/env bash

dbexists=$(docker ps | grep prediction-db)

if [ -n "$dbexists" ]
then
    echo Removing existing database container...

    docker stop prediction-db
    docker rm prediction-db
fi

echo Starting new database container...
docker run -d \
    -p 5432:5432 \
    --name prediction-db \
    --restart always \
    -e POSTGRES_USER=slade \
    -e POSTGRES_PASSWORD=slade123 \
    library/postgres:10
