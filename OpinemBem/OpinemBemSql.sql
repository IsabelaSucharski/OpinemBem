use master
go

create database OpinemBem
go

use OpinemBem
go

create table usuario
(
	id_usuario int identity(1,1) primary key,
	nome varchar(200) not null,
	cpf varchar(20) not null unique,
	email varchar(150) not null,
	senha varchar(100) not null,
	dataNasc datetime,
	administrador bit
);

create table categoria
(
	id_categoria int identity(1,1) primary key,
	nome varchar(200)	
);

create table projeto_de_lei
(
	id_projeto int identity(1,1) primary key,
	nome varchar(500),
	id_categoria int not null references categoria (id_categoria),
	descricao varchar(max),
	vantagens varchar(max),
	desvantagens varchar(max)
);

create table voto
(
	id_usuario int not null references usuario,
	id_projeto int not null references projeto_de_lei,
	data_voto datetime not null default getdate(),
	constraint pk_voto primary key (id_usuario, id_projeto)
);

create table comentario
(
	id_comentario int identity(1,1) primary key,
	id_usuario int not null references usuario,
	id_projeto int not null references projeto_de_lei,
	data_comentario datetime not null default getdate(),
	mensagem varchar(max)
);

--sexo int (eNUM NO C#),
--frentepli varchar(250),
--religiao varchar(250),
--email varchar(200) unique,
--nivel int
