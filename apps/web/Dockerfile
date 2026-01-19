FROM node:20-bookworm-slim AS build
WORKDIR /app

# IMPORTANTE: não copiar o package-lock.json aqui (lock gerado no Windows)
COPY package.json ./
RUN npm install --no-audit --no-fund

COPY . .
RUN npm run build

FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
