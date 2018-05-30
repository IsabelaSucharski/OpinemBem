create database OpinemBem
go

use OpinemBem
go

create table estado
(
	id_estado int identity(1,1) primary key,
	nome varchar(100) not null,
	sigla varchar(2) not null
);

--select * from projeto_de_lei

create table cidade
(
	id_cidade int identity(1,1) primary key,
	nome varchar(100) not null,
	id_estado int references estado (id_estado) 
);

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
	id_estado int references estado (id_estado),
	id_cidade int references cidade (id_cidade),
	sexo integer
);

insert into usuario (nome, cpf, email, senha, administrador, sexo) 
values ('Administrador', '111.111.111-11', 'admin@opinembem.com.br', '123', 1, 1);

insert into usuario (nome, cpf, email, senha, administrador, sexo) 
values ('Usuário 1', '222.222.222-22', 'usuario1@opinembem.com.br', '123', 0, 2);

create table categoria
(
	id_categoria int identity(1,1) primary key,
	nome varchar(200)	
);

insert into categoria values('Religião');
insert into categoria values('Sociedade');
insert into categoria values('Esporte');
insert into categoria values('Indústria');
insert into categoria values('Meio Ambiente');
insert into categoria values('Saúde');
insert into categoria values('Segurança');
insert into categoria values('Crianças e Adolescentes');
insert into categoria values('Mulher');
insert into categoria values('Transporte');
insert into categoria values('Educação');

create table projeto_de_lei
(
	id_projeto int identity(1,1) primary key,
	nome varchar(500),
	id_categoria int not null references categoria (id_categoria),
	id_usuario int not null references usuario(id_usuario),
	data_cadastro datetime not null default getdate(),
	descricao varchar(max),
	vantagens varchar(max),
	desvantagens varchar(max),
	tempo_disponivel varchar(10),
	publicado bit not null default 0
);

create table voto
(
	id_voto int identity(1,1) primary key,
	id_usuario int not null references usuario,
	id_projeto int not null references projeto_de_lei,
	data_voto datetime not null default getdate(),
	valor char(1)
);

create table comentario
(
	id_comentario int identity(1,1) primary key,
	id_usuario int not null references usuario,
	id_projeto int not null references projeto_de_lei,
	data_comentario datetime not null default getdate(),
	mensagem varchar(max)
);
go

-- criando campo calculado de quantidade de votos contra ou à favor
create function dbo.get_votos(@id int, @valor char(1)) returns int
as 
begin
    return ( 
        select 
			coalesce(count(*), 0) as qtd_voto
		from voto v
		where v.id_projeto = @id
		and v.valor = @valor
    );
end
go

-- criando campo de quantidade de votos à favor
alter table projeto_de_lei add votos_a_favor as dbo.get_votos(id_projeto, 'S');
go

-- criando campo de quantidade de votos contra
alter table projeto_de_lei add votos_contra as dbo.get_votos(id_projeto, 'N');
go

-- criando campo calculado de quantidade total de votos
create function dbo.get_total(@id int) returns int
as 
begin
    return ( 
        select 
			coalesce(count(*), 0) as qtd_voto
		from voto v
		where v.id_projeto = @id
    );
end
go

-- criando campo de quantidade de votos contra
alter table projeto_de_lei add total_votos as dbo.get_total(id_projeto);
go



--http://opinembem.azurewebsites.net/Login