#!/usr/bin/env bash

#set -e

version=$1
network_name=baby-predictions
container_name=baby-predictions

# Load the image from the tarball
docker load -i ~/baby-predictions.tar

# Create the baby-predictions network if it does not already exist
network_exists=$(docker network ls | grep $network_name)

if [ -z "$network_name" ]
then
	echo $network_name network does not exist, creating...
	docker network create $network_name
fi

# Stop and remove the container
container_running=$(docker ps | grep $container_name)
container_exists=$(docker ps -a | grep $container_name)

if [ ! -z "$container_running" ]
then
	echo $container_name container is running, stopping...
	docker stop $container_name
fi

if [ ! -z "$container_exists" ]
then
	echo $container_name container exists, removing...
	docker rm $container_name
fi

# Spin up a new container within the baby-predictions network
echo Starting a new instance of the container $container_name with version $version...
docker run -d \
	--restart always \
	--name $container_name \
	--network=$network_name \
	$container_name:$version
