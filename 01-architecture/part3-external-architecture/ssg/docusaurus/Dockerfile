#
# 빌드 스테이지
#
FROM node:lts-alpine3.20 AS builder

WORKDIR /app

COPY package*.json ./
RUN npm ci --omit=dev

COPY . .
RUN npm run build

#
# 실행 스테이지
#
FROM node:lts-alpine3.20

WORKDIR /app

COPY --from=builder /app/build /app/build
COPY --from=builder /app/package*.json ./
COPY --from=builder /app/node_modules /app/node_modules
COPY --from=builder /app/docusaurus.config.ts ./
RUN npm ci --omit=dev

EXPOSE 3000/tcp

CMD ["npm", "run", "serve", "--", "--host", "0.0.0.0", "--no-open"]
