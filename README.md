
# Escola de Idiomas

Um projeto CRUD de alunos e turmas


## Tecnologias

- Web Api em .NET 7
- SQL Server



## downloads

Programas necessários
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/)
- [SDK do .NET 7](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Instalação do SDK do .NET 7
```bash
  - Acesse o site oficial da Microsoft para o SKD do .NET7 no link acima
  - Baixe o instalador da última versão lançada para o seu sistema operacional
  - Após o download, execute o arquivo do instalador
  - Precione Instalar e aguarde o SDK ser instalado
  - Pronto, o SDK está instalado
```

### Instalação do SQL Server
```bash
  - Acesse o site oficial da Microsoft para o SQL Server no link acima
  - Escolha e baixe a versão Developer
  - Após o download, execute o arquivo do instalador 
  - O instalador abrirá o SQL Server Installation Center. Clique em "New SQL Server
    stand-alone installation or add feature to an existing installation".
  - Seguir as Etapas do Instalador
  - Após a conclusão, clique em "Concluir" para finalizar o processo.
```

### Instalação do Visual Studio
```bash
  - Acesse o site oficial da Microsoft para o Visual Studio 2022 no link acima.
  - Clique em "Baixar" para a versão que você deseja (Community, Professional ou Enterprise).
  - Após o download, execute o arquivo do instalador, o instalador irá iniciar e você verá uma tela
    com opções de carga de trabalho 
  - Escolha as cargas de trabalho: 
    -> ASP.NET e desenvolvimento Web
    -> Desenvolvimento para desktop com .NET7
    -> Processamento e armazenamento de dados
  - Após selecionar as cargas de trabalho, clique em "Instalar".
  - Quando a instalação estiver concluída, você verá uma tela indicando que o Visual Studio foi instalado com sucesso.
```

### Uso do Visual Studio
```bash
  - Abra aa pasta EscolaDeIdiomas e selecione EscolaDeIdiomas.sln para abrir o Visual Studio 2022
  - Na barra de cima, passe o mouse sobre "Exibir" e em "Outras Janelas" e selecione o "Console do Gerenciador de Pacotes"
  - No Console de Gerenciador de Pacotes, ao lado do PM>, escreva Update-Database
  - Se tudo deu certo, você pode fechar o Console e clicar na parte de cima o simbolo com uma seta verde com o nome https
    para rodar o Swagger no navegador
```
## Documentação da API

#### Retorna todos os alunos

```http
  GET /api/Alunos/Todos
```

#### Retorna um aluno pela Id

```http
  GET /api/Alunos/id/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. A ID do aluno selecionado |

 #### Retorna um aluno pelo nome

```http
  GET /api/Alunos/nome/{nome}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `nome`      | `string` | **Obrigatório**. O nome completo do aluno selecionado |

#### Retorna as turmas que o aluno selecionado está cursando

```http
  GET /api/Alunos/turmas/{alunoId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `alunoId`      | `int` | **Obrigatório**. A ID do aluno selecionado |

#### Cria um aluno, obrigatoriamente já cursando alguma turma existente

```http
  POST /api/Alunos/CadastrarAluno
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `turmaId`      | `int` | **Obrigatório**. A Id da turma que o aluno vai cursar |
| `id`      | `int` | O Id do aluno a ser criado, você pode apagar esse parâmetro que uma Id será cadastrada automaticamente |
| `nome`      | `string` | **Obrigatório**. O nome do aluno a ser cadastrado |
| `cpf`      | `string` | **Obrigatório**. O CPF do aluno a ser cadastrado, tem que ser um cpf válido  |
| `email`      | `string` | **Obrigatório**. O Email do aluno a ser cadastrado, tem que ser um email válido |

#### Modifica as informações do aluno selecionado

```http
  PUT /api/Alunos/ModificarAluno/{alunoId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `alunoId`      | `int` | **Obrigatório**. A Id do aluno a ser modificado |
| `Id`      | `int` | **Obrigatório**. A Id do aluno a ser modificado, esse parametro deve ser exatamento do acima |
| `nome`      | `string` |**Obrigatório**. O nome do aluno a ser modificado |
| `cpf`      | `string` | **Obrigatório**. O CPF do aluno a ser modificado, tem que ser um cpf válido  |
| `email`      | `string` | **Obrigatório**. O Email do aluno a ser modificado, tem que ser um email válido |

#### Deleta o aluno selecionado, o aluno não pode estar em nenhuma turma para ser deletado

```http
  DELETE /api/Alunos/DeletarAluno/{alunoId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `alunoId`      | `int` | **Obrigatório**. A Id do aluno a ser deletado |

#### Vincula o aluno selecionado na turma selecionada, a turma não pode ter mais de 5 alunos

```http
  POST /api/AlunosTurmas/AdicionarAlunoNaTurma
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `alunoId`      | `int` | **Obrigatório**. A Id do aluno a ser vinculado a turma  |
| `turmaId`      | `int` | **Obrigatório**. A Id da turma que queira que o aluno seja vinculado  |

#### Desvincula o aluno selecionado da turma selecionada

```http
  DELETE /api/AlunosTurmas/DesvincularAlunoDaTurma
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `alunoId`      | `int` | **Obrigatório**. A Id do aluno a ser desvinculado da turma  |
| `turmaId`      | `int` | **Obrigatório**. A Id da turma que queira que o aluno seja desvinculado  |

#### Retorna todos as turmas

```http
  GET /api/Turmas/Todas
```

#### Retorna uma turma pela Id

```http
  GET /api/Turmas/id/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. A ID da turma selecionada |

 #### Retorna uma lista de todas as turmas com o idioma selecionado

```http
  GET /api/Turmas/idioma/{idioma}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `idioma`      | `string` | **Obrigatório**. O idioma da/s turma/s selecionada/s |

 #### Retorna uma lista de todas as turmas com o nivel selecionado

```http
  GET /api/Turmas/nivel/{nivel}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `nivel`      | `string` | **Obrigatório**. O nivel da/s turma/s selecionada/s |

#### Retorna todos os alunos cursando a turma selecionada

```http
  GET /api/Turmas/aluno/{turmaId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `turmaId`      | `int` | **Obrigatório**. A ID da turma selecionada |

#### Cria uma turma

```http
  POST /api/Turmas/CriarTurma
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | A Id da turma ser criada, você pode apagar esse parametro pois um será criado automaticamente |
| `idioma`      | `string` | **Obrigatório**. O idioma da turma a ser criada |
| `nivel`      | `string` | **Obrigatório**. O nivel da turma a ser criado, sendo eles básico, intermediário e avançado  |
| `numero`      | `int` | **Obrigatório**. O número da turma a ser criado, uma turma não pode ter o mesmo número que outra |
| `anoLetivo`      | `int` | **Obrigatório**. O ano em que se passará o Ano Letivo da turma |

#### Modifica as informações da turma selecionada
 
```http
  PUT /api/Turmas/ModificarTurma/{turmaId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `turmaId`      | `int` | **Obrigatório**. A Id da turma a ser modificada |
| `Id`      | `int` | **Obrigatório**. A Id da turma a ser modificada, esse parametro deve ser exatamento do acima |
| `idioma`      | `string` | **Obrigatório**. O idioma da turma a ser modificada |
| `nivel`      | `string` | **Obrigatório**. O nivel da turma a ser modificada, sendo eles básico, intermediário e avançado  |
| `numero`      | `int` | **Obrigatório**. O número da turma a ser modificada, uma turma não pode ter o mesmo número que outra |
| `anoLetivo`      | `int` | **Obrigatório**. O ano em que se passará o Ano Letivo da turma |

#### Deleta a turma, a turma não pode ter alunos quando for deletada

```http
  DELETE /api/Turmas/DeletarTurma/{turmaId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `turmaId`      | `int` | **Obrigatório**. A Id da turma a ser deletada |