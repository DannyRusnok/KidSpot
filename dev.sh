#!/bin/bash

set -e

GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m'

cleanup() {
    echo ""
    echo -e "${YELLOW}Shutting down...${NC}"
    kill $BACKEND_PID $FRONTEND_PID 2>/dev/null
    wait $BACKEND_PID $FRONTEND_PID 2>/dev/null
    echo -e "${GREEN}Done.${NC}"
    exit 0
}

trap cleanup SIGINT SIGTERM

echo -e "${GREEN}=== KidSpot Dev ===${NC}"

# Backend
echo -e "${YELLOW}Starting backend (http://localhost:5000)...${NC}"
cd src/KidSpot.API
dotnet run &
BACKEND_PID=$!
cd ../..

# Frontend
echo -e "${YELLOW}Starting frontend (http://localhost:5173)...${NC}"
cd frontend
npm run dev &
FRONTEND_PID=$!
cd ..

echo ""
echo -e "${GREEN}Backend:  http://localhost:5000${NC}"
echo -e "${GREEN}Frontend: http://localhost:5173${NC}"
echo -e "${YELLOW}Press Ctrl+C to stop both.${NC}"

wait
