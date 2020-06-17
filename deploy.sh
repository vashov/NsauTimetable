#!/bin/bash
# This script is started on VPS by Github Actions

function writeHeader {
	echo "---------------------------------------- $1"
}

function moveToProjectFolder {
	writeHeader "Move to project repository folder"
	cd ~/projects/nsau_timetable
}

function createPublishFolder {
	writeHeader "Create publish folder"
	mkdir -p --verbose $outputDirectory
}

function cleanPublishFolder {
	writeHeader "Clean publish folder"
	rm -rf --verbose $outputDirectory/*
}

function buildWebRelease {
	writeHeader "Build web release"
	dotnet publish ./NsauT.Web/NsauT.Web.csproj -c Release -o ${outputDirectory}
}

#function runWebRelease {
#	writeHeader "Run web release"
#	local executingPath="${outputDirectory}/NsauT.Web.dll"
#	dotnet $executingPath
#}

function restartNsauWebService {
	writeHeader "Restart web service"
	systemctl restart nsau_web.service
}

function checkWebApp {
	sleep 5 # wait 5 seconds for restarting service 
	writeHeader "Check web app"
	curl localhost:5000
}

outputDirectory="/var/www/nsau_web"

moveToProjectFolder && 
createPublishFolder &&
cleanPublishFolder && 
buildWebRelease && 
restartNsauWebService &&
#runWebRelease && 
checkWebApp || exit 1
