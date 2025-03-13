# Build da aplicação Angular
FROM node:20 AS build-stage

WORKDIR /app

# Copiar apenas package.json e package-lock.json para instalar dependências primeiro
COPY package*.json ./

# Instalar dependências e Angular CLI
RUN npm cache clean --force && \
    npm install && \
    npm install -g @angular/cli@18.2.11

# Copiar todo o projeto e rodar o build de produção
COPY . ./

# Executar o build
RUN ng build

# Configuração do Nginx
FROM nginx:alpine

# Copiar configuração do Nginx
COPY nginx/nginx.conf /etc/nginx/nginx.conf

# Copiar arquivos da build Angular para o Nginx
COPY --from=build-stage /app/dist/ /usr/share/nginx/html

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