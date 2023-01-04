# TryitterAPI
Este projeto  foi pensando para conclusão do curso de aceleração em C# oferecido pela Trybe e XPInc.
O objetivo é alimentar a API de um blog, onde é possível fazer um CRUD em duas rotas diferentes, a de estudantes e postagens, 
além de ser possível criar usuários pra autenticação

### Tecnologias e ferramentas utilizadas:

<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/git/git-original.svg" width="40" height="40"/><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="40" height="40"/><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/azure/azure-original.svg" width="40" height="40" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="40" height="40"/><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dot-net/dot-net-original.svg" width="40" height="40"/><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/github/github-original.svg" width="40" height="40" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" width="40" height="40" />

## Como executar o projeto
Para rodar a API localmente utilizando Docker, certifique-se de ter o Docker esteja instalado instalados em sua máquina.

Clone o repositório
git@github.com:Carolyla/TryitterAPI.git
Entre na pasta do repositório que você acabou de clonar:
cd TryitterAPI
Rode o comando no seu terminal para baixar a imagem do SQL Server
docker pull mcr.microsoft.com/mssql/server
Assim que o container do SQL já está rodando, atualize a connectionString no arquivo appsettings.json.
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1,1433;Database=Tryitter;User ID=sa;Password=PASSWORD;TrustServerCertificate=true"
},
Rode o comando para criar as tabelas no banco de dados:
dotnet ef database update OBS: É ter instalado EF Core Tools para rodar esse comando.
Agora é só rodar a aplicação! Ela abrirá direto no Swagger!!
dotnet run

          
          
## Vídeo da aplicação funcionando:        

https://user-images.githubusercontent.com/92409813/210576829-1964d5f6-81d5-4f3f-a3d3-e703a3e34b9c.mp4

