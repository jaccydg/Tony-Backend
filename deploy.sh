#!/bin/bash
cd /home/ubuntu/tony
docker compose down
docker image remove tonybackendapi:latest
docker compose up -d