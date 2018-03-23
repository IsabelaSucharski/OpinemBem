create database OpinemBem
use OpinemBem


create table tipoUsuario -- 1..n com usuario
(
	idTipo int primary key identity(1,1),
	nome varchar(100)
);

create table usuario
(
	idUsuario int identity(1,1) primary key,
	nome varchar(100),
	dataNasc datetime,
	cpf varchar(15) unique,
	email varchar(50),
	--tipoUsuario int (eNUM NO C#)
	senha varchar(20)
	--sexo int (eNUM NO C#),
	--frentepli varchar(250),
	--religiao varchar(250),
	--email varchar(200) unique,
	--nivel int
)


create table administrador
(
	idAdmin int identity(1,1) primary key,
	nome varchar(100),
	dataNasc datetime,
	cpf varchar(15) unique,
	email varchar(50),
	--tipoUsuario int references 
	senha varchar(20),
	--confirmaSenha varchar(20),
	codigoAdmin varchar(15)
)


create table projetoDeLei
(
	idProjeto int identity(1,1) primary key,
	nome varchar(300),
	descricao varchar(max),
	vantagens varchar(max),
	desvantagens varchar(max)
)

create table voto --1..n com administrador e usuario
(
idUsuario int references usuario,
idProjeto int references projetoDeLei,
constraint pk_proj_usu primary key (idusuario,idprojeto),
voto char,
votado bit
)







