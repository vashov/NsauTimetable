#!/bin/bash
# This script is started on VPS by Github Actions

function writeHeader {
	echo "---------------------------------------- $1"
}

writeHeader "Pull actual version"
git checkout master
git pull

writeHeader "Build docker for web"
docker build -t nsau_web -f NsauT.Web/Dockerfile .

writeHeader "Set environment variables"
export DB_CONNECTION_STRING=$1

writeHeader "Run docker for web"
docker run -p 80:8080 -e DB_CONNECTION_STRING -d nsau_web

writeHeader "Check web app"
curl localhost:80
