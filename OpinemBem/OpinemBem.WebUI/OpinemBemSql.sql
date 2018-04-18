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
	data_nasc datetime,
	administrador bit,
	foto varchar(1000),
	caminho_foto varchar(3000),
	sexo integer
);

insert into usuario (nome, cpf, email, senha, administrador, sexo) 
values ('Administrador', '111.111.111-11', 'admin@opinembem.com.br', '123', 1, 1);

insert into usuario (nome, cpf, email, senha, administrador, sexo) 
values ('Usu�rio 1', '222.222.222-22', 'usuario1@opinembem.com.br', '123', 0, 2);

create table categoria
(
	id_categoria int identity(1,1) primary key,
	nome varchar(200)	
);

insert into categoria values('Religi�o');
insert into categoria values('Sociedade');
insert into categoria values('Esporte');
insert into categoria values('Ind�stria');
insert into categoria values('Meio Ambiente');
insert into categoria values('Sa�de');
insert into categoria values('Seguran�a');
insert into categoria values('Crian�as e Adolescentes');
insert into categoria values('Mulher');
insert into categoria values('Transporte');
insert into categoria values('Educa��o');

create table projeto_de_lei
(
	id_projeto int identity(1,1) primary key,
	nome varchar(500),
	id_categoria int not null references categoria (id_categoria),
	id_usuario int not null references usuario(id_usuario),
	descricao varchar(max),
	vantagens varchar(max),
	desvantagens varchar(max),
	tempo_disponivel varchar(10),
	publicado bit not null default 0,
	votos int
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