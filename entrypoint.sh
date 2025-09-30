#!/bin/bash

# Export environment variables from Render's mounted secret files
declare -A secrets=(
  ["FREEPIK__WEBHOOK"]="/etc/secrets/FREEPIK__WEBHOOK"
  ["FREEPIK__APIKEY"]="/etc/secrets/FREEPIK__APIKEY"
  ["FREEPIK__BASEURL"]="/etc/secrets/FREEPIK__BASEURL"

  ["EmailConfiguration__Password"]="/etc/secrets/EmailConfiguration__Password"
  ["EmailConfiguration__UserName"]="/etc/secrets/EmailConfiguration__UserName"
  ["CloudinarySettings__CloudName"]="/etc/secrets/CloudinarySettings__CloudName"
  ["CloudinarySettings__ApiKey"]="/etc/secrets/CloudinarySettings__ApiKey"
  ["CloudinarySettings__ApiSecret"]="/etc/secrets/CloudinarySettings__ApiSecret"
  ["JWT__Secret"]="/etc/secrets/JWT__Secret"

  ["ConnectionStrings__ProdDb"]="/etc/secrets/ConnectionStrings__ProdDb"
)

for key in "${!secrets[@]}"; do
  if [ -f "${secrets[$key]}" ]; then
    export "$key"="$(cat "${secrets[$key]}")"
  fi
done

# Start the app
exec dotnet Austistic.Api.dll
