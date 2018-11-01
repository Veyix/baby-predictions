#!/usr/bin/env bash

set -e

version=1.0.0
server=sladesoftware-droplet-01

echo Building the production image and exporting as a tarball...
docker build -t baby-predictions:$version .
docker save -o baby-predictions.tar baby-predictions:$version

echo Copying the image tarball to the server...
scp ./baby-predictions.tar root@$server:~/

echo Running database deployment script on the server...
ssh root@$server 'bash -s' < ./deployment/deploy-database.sh

echo Running the service deployment script on the server...
ssh root@$server 'bash -s' < ./deployment/deploy-start-service.sh $version

echo Copying nginx configuration for service to server...
scp ./deployment/nginx.prod.conf \
  root@$server:/docker/nginx/conf/baby.sladesoftwareltd.com.conf

echo Running nginx configuration deployment on the server...
ssh root@$server 'bash -s' < ./deployment/deploy-nginx-configuration.sh

echo Running health check...
curl https://baby.sladesoftwareltd.com/

echo Deployment complete!
