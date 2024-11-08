# Build da aplicação
FROM node:18 AS build

WORKDIR /app

COPY ./src/GeekStore.Client/package*.json ./
RUN npm install
RUN npm install -g @angular/cli

COPY src/GeekStore.Client/ ./

RUN npm run build --prod

# Configuração do Nginx
FROM nginx:alpine

# Instalar Nginx
RUN apk add --no-cache nginx

# Configuração do Nginx para o container
COPY ./src/GeekStore.Client/nginx/nginx.conf /etc/nginx/nginx.conf

# Copiar arquivos da build Angular para o Nginx
COPY --from=build /app/dist/geek-store.client /usr/share/nginx/html

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