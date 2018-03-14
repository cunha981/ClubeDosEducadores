CREATE DATABASE ClubeDosEducadores;
USE ClubeDosEducadores

CREATE TABLE Cargo(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	Descricao nvarchar(200) NOT NULL,
	Abreviacao nvarchar(200) NOT NULL,
	Codigo nvarchar(20) NULL
);

CREATE TABLE TipoUnidade(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	Descricao nvarchar(200) NOT NULL,
	Tipo nvarchar(20) NULL,
    Chave nvarchar(100) NOT NULL
);

CREATE TABLE RegiaoUnidade(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	Regiao nvarchar(200) NOT NULL,
    Chave nvarchar(100) NOT NULL
);

CREATE TABLE DiretoriaRegional(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	Nome nvarchar(200) NOT NULL,
    Chave nvarchar(100) NOT NULL
);

CREATE TABLE Unidade(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
	Nome nvarchar(200) NOT NULL,
	Codigo nvarchar(20) NULL,
    Chave nvarchar(100) NOT NULL,
	TelefonePrincipal nvarchar(20) NULL,
	TelefoneSecundario nvarchar(20) NULL,
	TipoUnidadeId int NOT NULL,
	DificilAcesso int NULL,
	Email nvarchar(200) NULL,
	NomeDiarioOficial nvarchar(200) NULL,
	RegiaoUnidadeId int NULL,
    DiretoriaRegionalId int null,
	Url nvarchar(200) NULL,
    Uf nvarchar(2) null,
    Cidade nvarchar(50) null,
    Bairro nvarchar(50) null,
    Logradouro nvarchar(100) null,
    Cep nvarchar(10) null,
    Numero nvarchar(50) null,
    Endereco nvarchar(200) null,
    Latitude float null,
    Longitude float null
);

ALTER TABLE Unidade  ADD  CONSTRAINT FK_Unidade_RegiaoUnidade_RegiaoUnidade_Id FOREIGN KEY(RegiaoUnidadeId) REFERENCES RegiaoUnidade (Id);
ALTER TABLE Unidade  ADD  CONSTRAINT FK_Unidade_DiretoriaReginal_Id FOREIGN KEY(DiretoriaRegionalId) REFERENCES DiretoriaRegional (Id);
ALTER TABLE Unidade  ADD  CONSTRAINT FK_Unidade_TipoUnidade_TipoUnidadeId FOREIGN KEY(TipoUnidadeId) REFERENCES TipoUnidade (Id);

CREATE TABLE Funcionario(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	Nome nvarchar(200) NOT NULL,
    Email nvarchar(200) NOT NULL,
    Senha nvarchar(200) NOT NULL,
    Ativo bit			NOT NULL,
	CargoId int NOT NULL,
	Logradouro nvarchar(50) NULL,
	Numero nvarchar(50) NULL,
	Bairro nvarchar(50) NULL,
	Cidade nvarchar(50) NULL,
	Uf nvarchar(2) NULL,
	Cep nvarchar(10) NULL,
	Complemento nvarchar(50) NULL,
	Latitude float NULL,
	Longitude float NULL,
	UnidadeTrabalhoId int NULL,
	Telefone nvarchar(20) NULL,
	Celular nvarchar(20) NULL
);

ALTER TABLE Funcionario  ADD  CONSTRAINT FK_Funcionario_Cargo_CargoId FOREIGN KEY(CargoId) REFERENCES Cargo (Id);
ALTER TABLE Funcionario  ADD  CONSTRAINT FK_Funcionario_Unidade_UnidadeTrabalhoId FOREIGN KEY(UnidadeTrabalhoId) REFERENCES Unidade (Id);

CREATE TABLE TipoVaga(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
    Descricao nvarchar(200) NOT NULL
);

CREATE TABLE VagaRemocao(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
	UnidadeId int NOT NULL,
	CargoId int NOT NULL,
	Vagas int NOT NULL,
	Periodo date NOT NULL,
	VagasPotenciais int NULL,
	VagasIniciais int NULL,
    TipoVagaId int NOT NULL,
    DtIniciais datetime null,
    DtPotenciais datetime null
);

ALTER TABLE VagaRemocao  ADD  CONSTRAINT FK_VagaRemocao_Cargo_CargoId FOREIGN KEY(CargoId) REFERENCES Cargo (Id);
ALTER TABLE VagaRemocao  ADD  CONSTRAINT FK_VagaRemocao_Unidade_UnidadeId FOREIGN KEY(UnidadeId) REFERENCES Unidade (Id);
ALTER TABLE VagaRemocao  ADD  CONSTRAINT FK_VagaRemocao_TipoVaga_TipoVagaId FOREIGN KEY(TipoVagaId) REFERENCES TipoVaga (Id);

CREATE TABLE FuncionarioUnidadeDistancia(
	FuncionarioId int NOT NULL,
    UnidadeId int NOT NULL,
	Distancia int NOT NULL,
	Duracao int NOT NULL,
    CustoTransporte decimal(15,2) NOT NULL,
    Valido bit NOT NULL,
    DtConsulta datetime NOT NULL,
    PRIMARY KEY(FuncionarioId, UnidadeId)
);

ALTER TABLE FuncionarioUnidadeDistancia  ADD  CONSTRAINT FK_FuncionarioUnidadeDistancia_Funcionario_FuncionarioId FOREIGN KEY(FuncionarioId) REFERENCES Funcionario (Id);
ALTER TABLE FuncionarioUnidadeDistancia  ADD  CONSTRAINT FK_FuncionarioUnidadeDistancia_Unidade_UnidadeId FOREIGN KEY(UnidadeId) REFERENCES Unidade (Id);

CREATE TABLE TipoProduto(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
    Descricao nvarchar(200) NOT NULL
);

CREATE TABLE FormaPagamento(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
    Descricao nvarchar(200) NOT NULL
);


CREATE TABLE Pagamento(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY NOT NULL,
    FuncionarioId int not null,
    Valor decimal(15,2) not null,
    Desconto decimal(15,2) not null,
    ValorLiquido decimal(15,2) not null,
    Observacao nvarchar(300) null,
    DtCompra datetime not null,
    DtPamento datetime null,
    FormaPagamentoId int not null,
    Produto nvarchar(200) not null,
    TipoProdutoId int not null
);

ALTER TABLE Pagamento  ADD  CONSTRAINT FK_Pagamento_Funcionario_FuncionarioId FOREIGN KEY(FuncionarioId) REFERENCES Funcionario (Id);
ALTER TABLE Pagamento  ADD  CONSTRAINT FK_Pagamento_TipoProduto_TipoProdutoId FOREIGN KEY(TipoProdutoId) REFERENCES TipoProduto (Id);
ALTER TABLE Pagamento  ADD  CONSTRAINT FK_Pagamento_FormaPagamento_FormaPagamentoId FOREIGN KEY(FormaPagamentoId) REFERENCES FormaPagamento (Id);


-- INSERTS *************************************************************************************************

INSERT INTO TipoVaga (Descricao) VALUES ("Remoção Efetivos");
INSERT INTO TipoVaga (Descricao) VALUES ("Remoção Precários");