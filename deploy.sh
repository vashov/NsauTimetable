#!/bin/bash
# This script is started on VPS by Github Actions

function writeHeader {
	echo "---------------------------------------- $1"
}

function setEnvVariables {
	writeHeader "Set environment variables"
	export DB_CONNECTION_STRING=$1
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
	local cleanPath="${outputDirectory}/*"
	rm -r $cleanPath
}

function buildWebRelease {
	writeHeader "Build web release"
	dotnet publish -c Release -o ${outputDirectory}
}

function runWebRelease {
	writeHeader "Run web release"
	local executingPath="${outputDirectory}/NsauT.Web"
	dotnet $executingPath
}

function checkWebApp {
	writeHeader "Check web app"
	curl localhost:5000
}

outputDirectory="/var/www/nsau_web"

setEnvVariables $1 &&
moveToProjectFolder && 
createPublishFolder &&
cleanPublishFolder && 
buildWebRelease && 
runWebRelease && 
checkWebApp
