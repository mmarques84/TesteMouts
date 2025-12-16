# TesteMouts
Tech challenge description
# TesteTecnicoMarcus

## Visão Geral

Este projeto é uma **API REST** desenvolvida em **ASP.NET Core (.NET 8)** seguindo o padrão de **Clean Architecture**, preparada para execução via **Docker**.

O objetivo é facilitar a execução do projeto em qualquer ambiente, sem necessidade de configurar SDKs localmente.
atencao: existe um script para inserir um usuario admin e cadastrar os cargos 
---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Clean Architecture
- Docker
- Swagger (OpenAPI)

---


---

## Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- **Docker Desktop**
  - Download: https://www.docker.com/products/docker-desktop/

Verifique a instalação:

```bash
docker --version
cd C:\teste\TesteTecnicoMarcus\src

Build da imagem Docker
docker build -t teste-tecnico-api -f TesteTecnicoMarcus.Api/Dockerfile .

Executar o container
docker run -d -p 5000:8080 --name teste-tecnico-api teste-tecnico-api

docker compose up -d --build


