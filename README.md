# Fintech-lite

Monorepo com:

* API Host (.NET 8) + Swagger + Health + Ping
* Worker Host (.NET 8) (placeholder por enquanto)
* Web (React + TS + Vite) servida via Nginx no Docker
* Infra local: Postgres + RabbitMQ (management)

## Como rodar (1 comando)

```bash
docker compose up -d --build
```

## URLs

* Web: [http://localhost:5173](http://localhost:5173)
* API (Swagger): [http://localhost:5080/swagger](http://localhost:5080/swagger)
* API Health: [http://localhost:5080/health](http://localhost:5080/health)
* API Ping: [http://localhost:5080/api/ping](http://localhost:5080/api/ping)
* RabbitMQ Management: [http://localhost:15672](http://localhost:15672) (fintech/fintech)
* Postgres: localhost:5432 (postgres/postgres) db: fintechlite

## Reset (limpar volumes)

```bash
docker compose down -v --remove-orphans
docker compose up -d --build
docker compose ps
```

## Logs úteis

```bash
docker compose logs -f api
docker compose logs -f web
docker compose logs -f worker
docker compose logs -f postgres
docker compose logs -f rabbitmq
```

## Dev (opcional)

Rodar API no Docker e Web com Vite (hot reload):

1. Suba infra + API:

```bash
docker compose up -d --build postgres rabbitmq api
```

2. Rode o Vite:

```bash
cd apps/web
npm install
npm run dev
```

Observação:

* No modo Vite, a Web usa proxy para a API via variável `VITE_API_TARGET` (ex.: [http://localhost:5080](http://localhost:5080)).
* No modo Docker (Nginx), a Web acessa a API pelos endpoints publicados (ex.: [http://localhost:5080](http://localhost:5080)) ou via proxy do Nginx (se configurado).
