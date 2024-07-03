#!/bin/bash
cd /home/ubuntu/tony
docker compose down
docker image remove tonybackendapi:latest
docker volume postgres_data
docker compose up -d