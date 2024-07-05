#!/bin/bash
cd /home/ubuntu/tony/Tony-Backend.Infrastructure
export PATH="$PATH:$HOME/.dotnet/tools/"
# leave only last line once all migrations are done
dotnet ef database drop -f
rm -r Migrations
dotnet ef migrations add Initial
dotnet ef database update