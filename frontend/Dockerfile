FROM node:22-alpine AS builder

WORKDIR /app

# Copy only the dependency files first.
COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM alpine:latest AS production
WORKDIR /app
COPY --from=builder /app/dist ./dist

CMD ["sh", "-c", "echo 'Frontend build pronto' && sleep infinity"]