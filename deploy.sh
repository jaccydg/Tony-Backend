#!/bin/bash
cd /home/ubuntu/tony
docker compose down
docker image remove tonybackendapi:latest
docker volume rm tony_postgres_data
docker compose up -d