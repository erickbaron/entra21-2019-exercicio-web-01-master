﻿CREATE TABLE escolas(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(100),
);

CREATE TABLE alunos(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(100), 
cpf VARCHAR(14),
nota1 DECIMAL(4,2),
nota2 DECIMAL(4,2),
nota3 DECIMAL(4,2)
);

SELECT * FROM alunos;