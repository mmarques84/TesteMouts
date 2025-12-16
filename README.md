# TesteMouts
Tech challenge description
# TesteTecnicoMarcus

## Visão Geral

Este projeto é uma **API REST** desenvolvida em **ASP.NET Core (.NET 8)** seguindo o padrão de **Clean Architecture**, preparada para execução via **Docker**.

O objetivo é facilitar a execução do projeto em qualquer ambiente, sem necessidade de configurar SDKs localmente.
atencao: existe um script para inserir um usuario admin e cadastrar os cargos 
---


--inseri cargos
  INSERT INTO JobTitles (Id, Name, Department, IsActive) VALUES

(NEWID(), 'Analista Financeiro', 1, 1),
(NEWID(), 'Coordenador de Recursos Humanos', 4, 1),
(NEWID(), 'Assistente Administrativo', 0, 1),
(NEWID(), 'Gerente Comercial', 2, 1),
(NEWID(), 'Diretor de Operações', 5, 1),
(NEWID(),
 'Gerente',
 0,
 1);

select * from JobTitles

INSERT INTO Employees
(
    Id,
    FirstName,
    LastName,
    Email,
    DocNumber,
    BirthDate,
    Phone,
    Role,
    JobTitleId,
    PasswordHash,
    IsActive,
    CreatedAt
)
VALUES
(
    NEWID(),
    'Admin',
    'Master',
    'admin@teste.com',
    '00000000000',
    '1990-01-01',
    '11999999999',
    0,  -- ERole.Admin
    'EE8E6BF4-23DF-4065-BFEC-DA444E76C067',  -- JobTitle Admin
    'AQAAAAIAAYagAAAAEOwKRjSAG3FDkPgtQ2OpuM8vMyZDt2L3yvjVlCJn0hC3PL5l+tNfQjnq4z2TQGRcQ==', -- senha: Admin@123
    1,
    GETUTCDATE()
);
select * from Employees
--DELETE FROM Employees


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


