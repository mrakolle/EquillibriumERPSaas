#!/bin/bash

if [ -z "$1" ]; then
  echo "❌ Please provide a migration name"
  echo "Example:"
  echo "./scripts/add-master-migration.sh InitialMaster"
  exit 1
fi

MIGRATION_NAME=$1

dotnet ef migrations add $MIGRATION_NAME \
  --project EquillibriumERP.Infrastructure \
  --startup-project EquillibriumERP.Api \
  --context MasterDbContext \
  --output-dir Persistence/Migrations/Master

echo "✅ Master migration created successfully"
