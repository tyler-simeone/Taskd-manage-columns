#!/bin/zsh

# Load environment variables from .env 
source .env

# Run the Docker container with specified environment variables and port mapping
docker run -d \
  --name test-columns \
  -p 5156:80 \
  --network my_custom_network \
  -e LocalDBConnection=$PROJECT_B_LOCAL_CONX \
  -e ManageTasksLocalConnection=$MANAGE_TASKS_LOCAL_CONX \
  -e UserPoolId=$USER_POOL_ID \
  -e Region=$REGION \
  tylersimeone/projectb/manage-columns:latest

if [ $? -ne 0 ]; then
  echo "Docker run command failed!"
  exit 1
fi

echo "Docker container started successfully."