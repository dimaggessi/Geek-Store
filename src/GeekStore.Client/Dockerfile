# Build da aplicação Angular
FROM node:18 AS build

WORKDIR /app

# Copiar apenas package.json e package-lock.json para instalar dependências primeiro
COPY ./src/GeekStore.Client/package*.json ./

RUN npm install
RUN npm install -g @angular/cli

# Copiar todo o projeto e rodar o build de produção
COPY src/GeekStore.Client/ ./

RUN npm run build -- --configuration=production

# Configuração do Nginx
FROM nginx:alpine

# Copiar configuração do Nginx
COPY ./src/GeekStore.Client/nginx/nginx.conf /etc/nginx/nginx.conf

# Copiar arquivos da build Angular para o Nginx
COPY --from=build /app/dist/geek-store.client /usr/share/nginx/html

# Copiar o arquivo env.js para a pasta de assets (verificando se ele existe)
COPY ./src/GeekStore.Client/src/assets/env.js /usr/share/nginx/html/assets/env.js

# Instalar openssl e gerar certificados de desenvolvimento
RUN apk add --no-cache openssl && \
    mkdir -p /etc/ssl/private && \
    openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout /etc/ssl/private/localhost.key \
    -out /etc/ssl/private/localhost.crt \
    -subj "/CN=localhost"

# Expor portas 80 e 443 para HTTP e HTTPS
EXPOSE 80 443

CMD ["nginx", "-g", "daemon off;"]
