/*
DROP TABLE DB_SMARTSCHOOL.ALUNOSDISCIPLINAS;
DROP TABLE DB_SMARTSCHOOL.ALUNOCURSOS;
DROP TABLE DB_SMARTSCHOOL.CURSOS;
DROP TABLE DB_SMARTSCHOOL.DISCIPLINAS;
DROP TABLE DB_SMARTSCHOOL.PROFESSORES;
DROP TABLE DB_SMARTSCHOOL.ALUNOS;
*/

CREATE TABLE DB_SMARTSCHOOL.ALUNOS (
 ID INT PRIMARY KEY AUTO_INCREMENT
,ATIVO TINYINT(1)
,DATAFIM DATETIME
,DATAINI DATETIME
,DATANASC DATETIME
,MATRICULA INT
,NOME VARCHAR(255)
,SOBRENOME VARCHAR(255)
,TELEFONE VARCHAR(255));

CREATE TABLE DB_SMARTSCHOOL.CURSOS (
 ID INT PRIMARY KEY AUTO_INCREMENT
,NOME VARCHAR(255));

CREATE TABLE DB_SMARTSCHOOL.ALUNOCURSOS (
 DATAINI DATETIME
,DATAFIM DATETIME
,ALUNOID INT
,CURSOID INT);

CREATE TABLE DB_SMARTSCHOOL.PROFESSORES (
 ID INT PRIMARY KEY AUTO_INCREMENT
,REGISTRO INT
,NOME VARCHAR(255)
,SOBRENOME VARCHAR(255)
,TELEFONE VARCHAR(255)
,DATAINI DATETIME
,DATAFIM DATETIME
,ATIVO TINYINT(1));

CREATE TABLE DB_SMARTSCHOOL.DISCIPLINAS (
 ID INT PRIMARY KEY AUTO_INCREMENT
,NOME VARCHAR(255)
,CARGAHORARIA INT
,PREREQUISITOID INT
,PROFESSORID INT
,CURSOID INT);

CREATE TABLE DB_SMARTSCHOOL.ALUNOSDISCIPLINAS (
 DATAINI DATETIME
,DATAFIM DATETIME
,NOTA INT
,ALUNOID INT
,DISCIPLINAID INT);

ALTER TABLE ALUNOSDISCIPLINAS ADD CONSTRAINT FK_DISCP_ALUNO_ID FOREIGN KEY (ALUNOID) REFERENCES ALUNOS(ID);
ALTER TABLE ALUNOSDISCIPLINAS ADD CONSTRAINT FK_DISCIPLINA_ID FOREIGN KEY (DISCIPLINAID) REFERENCES DISCIPLINAS(ID);

ALTER TABLE DISCIPLINAS ADD CONSTRAINT FK_PROFESSOR_ID FOREIGN KEY (PROFESSORID) REFERENCES PROFESSORES(ID);
ALTER TABLE DISCIPLINAS ADD CONSTRAINT FK_PREREQUISITO_ID FOREIGN KEY (PREREQUISITOID) REFERENCES DISCIPLINAS(ID);

ALTER TABLE ALUNOCURSOS ADD CONSTRAINT FK_ALUNO_ID FOREIGN KEY (ALUNOID) REFERENCES ALUNOS(ID);
ALTER TABLE ALUNOCURSOS ADD CONSTRAINT FK_CURSO_ID FOREIGN KEY (CURSOID) REFERENCES CURSOS(ID);



insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 1, "2022-07-01 10:00:00", "2005-05-28", "Marta", "Kent", "33225555");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 2, "2022-07-01 10:00:00", "2005-05-28", "Paula", "Isabela", "3354288");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 3, "2022-07-01 10:00:00", "2005-05-28", "Laura", "Antonia", "55668899");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 4, "2022-07-01 10:00:00", "2005-05-28", "Luiza", "Maria", "6565659");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 5, "2022-07-01 10:00:00", "2005-05-28", "Lucas", "Machado", "565685415");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 6, "2022-07-01 10:00:00", "2005-05-28", "Pedro", "Alvares", "456454545");
insert into alunos(Ativo, Matricula, DataIni, DataNasc, Nome, Sobrenome, Telefone) values (1, 7, "2022-07-01 10:00:00", "2005-05-28", "Paulo", "José", "9874512");

insert into professores(Registro, Nome, Sobrenome) values(1, "Lauro", "Oliveira");
insert into professores(Registro, Nome, Sobrenome) values(2, "Roberto", "Soares");
insert into professores(Registro, Nome, Sobrenome) values(3, "Ronaldo", "Marconi");
insert into professores(Registro, Nome, Sobrenome) values(4, "Rodrigo", "Carvalho");
insert into professores(Registro, Nome, Sobrenome) values(5, "Alexandre", "Montanha");

insert into cursos(Nome) values("Tecnologia da Informação");
insert into cursos(Nome) values("Sistemas de Informação");
insert into cursos(Nome) values("Ciência da Computação");

insert into disciplinas(Nome, Professorid, Cursoid) values("Matemática", 1, 1);
insert into disciplinas(Nome, Professorid, Cursoid) values("Matemática", 1, 3);
insert into disciplinas(Nome, Professorid, Cursoid) values("Física", 2, 3);
insert into disciplinas(Nome, Professorid, Cursoid) values("Português", 3, 1);
insert into disciplinas(Nome, Professorid, Cursoid) values("Inglês", 4, 1);
insert into disciplinas(Nome, Professorid, Cursoid) values("Inglês", 4, 2);
insert into disciplinas(Nome, Professorid, Cursoid) values("Inglês", 4, 3);
insert into disciplinas(Nome, Professorid, Cursoid) values("Programação", 5, 1);
insert into disciplinas(Nome, Professorid, Cursoid) values("Programação", 5, 2);
insert into disciplinas(Nome, Professorid, Cursoid) values("Programação", 5, 3);

insert into alunosdisciplinas (Alunoid, DisciplinaId) values(1,2);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(1,4);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(1,5);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(2,1);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(2,2);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(2,5);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(3,1);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(3,2);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(3,3);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(4,1);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(4,4);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(4,5);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(5,4);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(5,5);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(6,1);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(6,2);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(6,3);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(6,4);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(7,1);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(7,2);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(7,3);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(7,4);
insert into alunosdisciplinas (Alunoid, DisciplinaId) values(7,5);