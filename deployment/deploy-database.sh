#!/usr/bin/env bash

#set -e

network_name=baby-predictions
container_name=baby-predictions-db

network_exists=$(docker network ls | grep $network_name)

if [ -z "$network_exists" ]
then
	echo $network_name network does not exist, creating...
	docker network create $network_name
fi

db_exists=$(docker ps | grep $container_name)

if [ -z "$db_exists" ]
then
  echo Database $container_name does not exist, creating...
  docker run -d \
    --name $container_name \
    --restart always \
    -e POSTGRES_USER=slade \
    -e POSTGRES_PASSWORD=slade123 \
    -v /etc/docker/$container_name/data:/var/lib/postgresql/data \
    library/postgres:10

  echo Attaching $container_name to $network_name network...
  docker network connect --alias db $network_name $container_name
fi

