#!/usr/bin/env bash

#set -e

container_name=baby-predictions
network_name=websites_default

echo Checking if the service already exists within the network...
exists_in_network=$(docker network inspect $network_name | grep $container_name)

if [ -z "$exists_in_network" ]
then
  echo Attaching service container $container_name to website routing network $network_name...
  docker network connect --alias baby-predictions-web $network_name $container_name
fi

echo Refreshing network $network_name...
docker exec websites_nginx-server_1 nginx -t
docker exec websites_nginx-server_1 nginx -s reload
