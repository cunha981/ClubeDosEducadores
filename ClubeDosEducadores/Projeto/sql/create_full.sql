CREATE DATABASE [DataBaseRF]

CREATE TABLE [dbo].[Cargo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [nvarchar](max) NOT NULL,
	[Abreviacao] [nvarchar](max) NOT NULL,
	[Codigo] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Cargo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[EnderecoUnidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	
 CONSTRAINT [PK_dbo.EnderecoUnidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Evento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[Descricao] [nvarchar](max) NOT NULL,
	[Resumo] [nvarchar](max) NOT NULL,
	[TipoEvento] [int] NOT NULL,
	[EnviarEmail] [bit] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[DtEvento] [datetime] NOT NULL,
	[DtCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Evento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Funcionario](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[CargoId] [int] NOT NULL,
	[Logradouro] [nvarchar](max) NULL,
	[Numero] [nvarchar](max) NULL,
	[Bairro] [nvarchar](max) NULL,
	[Cidade] [nvarchar](max) NULL,
	[Uf] [nvarchar](max) NULL,
	[Cep] [nvarchar](max) NULL,
	[Complemento] [nvarchar](max) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[UnidadeTrabalhoId] [int] NULL,
	[Telefone] [nvarchar](max) NULL,
	[Celular] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Funcionario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[MailTemplate](
	[Key] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MailTemplate] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Noticia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[Conteudo] [nvarchar](max) NOT NULL,
	[DtPublicacao] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Noticia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[PerguntaFrequente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pergunta] [nvarchar](max) NOT NULL,
	[Resposta] [nvarchar](max) NOT NULL,
	[Tags] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.PerguntaFrequente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Permuta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObservacaoFuncionario] [nvarchar](max) NOT NULL,
	[ObservacaoPermuta] [nvarchar](max) NOT NULL,
	[DtPublicacao] [datetime] NOT NULL,
	[DtExclusao] [datetime] NULL,
	[MotivoExclusao] [nvarchar](max) NULL,
	[FuncionarioId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Permuta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[PermutaRegiaoUnidade](
	[PermutaId] [int] NOT NULL,
	[RegiaoUnidadeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PermutaRegiaoUnidade] PRIMARY KEY CLUSTERED 
(
	[PermutaId] ASC,
	[RegiaoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PermutaTipoUnidade](
	[PermutaId] [int] NOT NULL,
	[TipoUnidadeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PermutaTipoUnidade] PRIMARY KEY CLUSTERED 
(
	[PermutaId] ASC,
	[TipoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[RegiaoUnidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Regiao] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.RegiaoUnidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Remocao](
	[FuncionarioId] [int] NOT NULL,
	[Preferencia] [int] NOT NULL,
	[Data] [datetime] NOT NULL,
	[VagaRemocaoId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Remocao] PRIMARY KEY CLUSTERED 
(
	[VagaRemocaoId] ASC,
	[FuncionarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[TipoUnidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [nvarchar](max) NOT NULL,
	[Tipo] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.TipoUnidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Unidade](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[Codigo] [nvarchar](max) NULL,
	[TelefonePrincipal] [nvarchar](max) NULL,
	[TelefoneSecundario] [nvarchar](max) NULL,
	[TipoUnidadeId] [int] NOT NULL,
	[DificilAcesso] [int] NULL,
	[Email] [nvarchar](max) NULL,
	[NomeDiarioOficial] [nvarchar](max) NULL,
	[RegiaoUnidadeId] [int] NULL,
	[Url] [nvarchar](max) NULL,
	[EnderecoCompleto] [nvarchar](max) NOT NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_dbo.Unidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[UnidadeAvaliacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comentario] [nvarchar](max) NULL,
	[Nota] [int] NOT NULL,
	[DtAvaliacao] [datetime] NOT NULL,
	[FuncionarioId] [int] NOT NULL,
	[UnidadeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UnidadeAvaliacao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[UsuarioAtributo](
	[UsuarioId] [int] NOT NULL,
	[Atributo] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UsuarioAtributo] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC,
	[Atributo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[UsuarioFuncionario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Senha] [nvarchar](max) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[SessionId] [nvarchar](max) NULL,
	[DtAtividade] [datetime] NULL,
	[SendEmailPermuta] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UsuarioFuncionario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[UsuarioNotificacao](
	[UsuarioId] [int] NOT NULL,
	[TipoEvento] [int] NOT NULL,
	[Notificar] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UsuarioNotificacao] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC,
	[TipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[VagaRemocao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnidadeId] [int] NOT NULL,
	[CargoId] [int] NOT NULL,
	[Vagas] [int] NOT NULL,
	[Data] [datetime] NOT NULL,
	[VagasPotenciais] [int] NULL,
	[VagasIniciais] [int] NULL,
 CONSTRAINT [PK_dbo.VagaRemocao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[VagaRemocaoPrecario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnidadeId] [int] NOT NULL,
	[CargoId] [int] NOT NULL,
	[Vagas] [int] NOT NULL,
	[Data] [datetime] NOT NULL,
	[VagasPotenciais] [int] NULL,
	[VagasIniciais] [int] NULL,
 CONSTRAINT [PK_dbo.VagaRemocaoPrecario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_Id]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_Id] ON [dbo].[EnderecoUnidade]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CargoId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_CargoId] ON [dbo].[Funcionario]
(
	[CargoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Id]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_Id] ON [dbo].[Funcionario]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnidadeTrabalhoId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_UnidadeTrabalhoId] ON [dbo].[Funcionario]
(
	[UnidadeTrabalhoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FuncionarioId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_FuncionarioId] ON [dbo].[Permuta]
(
	[FuncionarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermutaId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_PermutaId] ON [dbo].[PermutaRegiaoUnidade]
(
	[PermutaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegiaoUnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_RegiaoUnidadeId] ON [dbo].[PermutaRegiaoUnidade]
(
	[RegiaoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermutaId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_PermutaId] ON [dbo].[PermutaTipoUnidade]
(
	[PermutaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TipoUnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_TipoUnidadeId] ON [dbo].[PermutaTipoUnidade]
(
	[TipoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia]    Script Date: 12/01/2018 22:07:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia] ON [dbo].[Remocao]
(
	[FuncionarioId] ASC,
	[VagaRemocaoId] ASC,
	[Preferencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegiaoUnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_RegiaoUnidadeId] ON [dbo].[Unidade]
(
	[RegiaoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TipoUnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_TipoUnidadeId] ON [dbo].[Unidade]
(
	[TipoUnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnidadeAvaliacao_FuncionarioId_UnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UnidadeAvaliacao_FuncionarioId_UnidadeId] ON [dbo].[UnidadeAvaliacao]
(
	[FuncionarioId] ASC,
	[UnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuarioId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioId] ON [dbo].[UsuarioAtributo]
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuarioId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioId] ON [dbo].[UsuarioNotificacao]
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CargoId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_CargoId] ON [dbo].[VagaRemocao]
(
	[CargoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnidadeId]    Script Date: 12/01/2018 22:07:43 ******/
CREATE NONCLUSTERED INDEX [IX_UnidadeId] ON [dbo].[VagaRemocao]
(
	[UnidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cargo] ADD  DEFAULT ('') FOR [Abreviacao]
GO
ALTER TABLE [dbo].[Evento] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DtCadastro]
GO
ALTER TABLE [dbo].[Remocao] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [Data]
GO
ALTER TABLE [dbo].[Remocao] ADD  DEFAULT ((0)) FOR [VagaRemocaoId]
GO
ALTER TABLE [dbo].[TipoUnidade] ADD  DEFAULT ('') FOR [Tipo]
GO
ALTER TABLE [dbo].[UsuarioFuncionario] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DtAtividade]
GO
ALTER TABLE [dbo].[UsuarioFuncionario] ADD  DEFAULT ((1)) FOR [SendEmailPermuta]
GO
ALTER TABLE [dbo].[EnderecoUnidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EnderecoUnidade_dbo.Unidade_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Unidade] ([Id])
GO
ALTER TABLE [dbo].[EnderecoUnidade] CHECK CONSTRAINT [FK_dbo.EnderecoUnidade_dbo.Unidade_Id]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Funcionario_dbo.Cargo_CargoId] FOREIGN KEY([CargoId])
REFERENCES [dbo].[Cargo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_dbo.Funcionario_dbo.Cargo_CargoId]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Funcionario_dbo.Unidade_UnidadeTrabalhoId] FOREIGN KEY([UnidadeTrabalhoId])
REFERENCES [dbo].[Unidade] ([Id])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_dbo.Funcionario_dbo.Unidade_UnidadeTrabalhoId]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Funcionario_dbo.UsuarioFuncionario_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[UsuarioFuncionario] ([Id])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_dbo.Funcionario_dbo.UsuarioFuncionario_Id]
GO
ALTER TABLE [dbo].[Permuta]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Permuta_dbo.Funcionario_FuncionarioId] FOREIGN KEY([FuncionarioId])
REFERENCES [dbo].[Funcionario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permuta] CHECK CONSTRAINT [FK_dbo.Permuta_dbo.Funcionario_FuncionarioId]
GO
ALTER TABLE [dbo].[PermutaRegiaoUnidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PermutaRegiaoUnidade_dbo.Permuta_PermutaId] FOREIGN KEY([PermutaId])
REFERENCES [dbo].[Permuta] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermutaRegiaoUnidade] CHECK CONSTRAINT [FK_dbo.PermutaRegiaoUnidade_dbo.Permuta_PermutaId]
GO
ALTER TABLE [dbo].[PermutaRegiaoUnidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PermutaRegiaoUnidade_dbo.RegiaoUnidade_RegiaoUnidadeId] FOREIGN KEY([RegiaoUnidadeId])
REFERENCES [dbo].[RegiaoUnidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermutaRegiaoUnidade] CHECK CONSTRAINT [FK_dbo.PermutaRegiaoUnidade_dbo.RegiaoUnidade_RegiaoUnidadeId]
GO
ALTER TABLE [dbo].[PermutaTipoUnidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PermutaTipoUnidade_dbo.Permuta_PermutaId] FOREIGN KEY([PermutaId])
REFERENCES [dbo].[Permuta] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermutaTipoUnidade] CHECK CONSTRAINT [FK_dbo.PermutaTipoUnidade_dbo.Permuta_PermutaId]
GO
ALTER TABLE [dbo].[PermutaTipoUnidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PermutaTipoUnidade_dbo.TipoUnidade_TipoUnidadeId] FOREIGN KEY([TipoUnidadeId])
REFERENCES [dbo].[TipoUnidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermutaTipoUnidade] CHECK CONSTRAINT [FK_dbo.PermutaTipoUnidade_dbo.TipoUnidade_TipoUnidadeId]
GO
ALTER TABLE [dbo].[Remocao]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Remocao_dbo.Funcionario_FuncionarioId] FOREIGN KEY([FuncionarioId])
REFERENCES [dbo].[Funcionario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Remocao] CHECK CONSTRAINT [FK_dbo.Remocao_dbo.Funcionario_FuncionarioId]
GO
ALTER TABLE [dbo].[Remocao]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Remocao_dbo.VagaRemocao_VagaRemocaoId] FOREIGN KEY([VagaRemocaoId])
REFERENCES [dbo].[VagaRemocao] ([Id])
GO
ALTER TABLE [dbo].[Remocao] CHECK CONSTRAINT [FK_dbo.Remocao_dbo.VagaRemocao_VagaRemocaoId]
GO
ALTER TABLE [dbo].[Unidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Unidade_dbo.RegiaoUnidade_RegiaoUnidade_Id] FOREIGN KEY([RegiaoUnidadeId])
REFERENCES [dbo].[RegiaoUnidade] ([Id])
GO
ALTER TABLE [dbo].[Unidade] CHECK CONSTRAINT [FK_dbo.Unidade_dbo.RegiaoUnidade_RegiaoUnidade_Id]
GO
ALTER TABLE [dbo].[Unidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Unidade_dbo.TipoUnidade_TipoUnidadeId] FOREIGN KEY([TipoUnidadeId])
REFERENCES [dbo].[TipoUnidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Unidade] CHECK CONSTRAINT [FK_dbo.Unidade_dbo.TipoUnidade_TipoUnidadeId]
GO
ALTER TABLE [dbo].[UnidadeAvaliacao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UnidadeAvaliacaos_dbo.Funcionario_FuncionarioId] FOREIGN KEY([FuncionarioId])
REFERENCES [dbo].[Funcionario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnidadeAvaliacao] CHECK CONSTRAINT [FK_dbo.UnidadeAvaliacaos_dbo.Funcionario_FuncionarioId]
GO
ALTER TABLE [dbo].[UnidadeAvaliacao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UnidadeAvaliacaos_dbo.Unidade_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnidadeAvaliacao] CHECK CONSTRAINT [FK_dbo.UnidadeAvaliacaos_dbo.Unidade_UnidadeId]
GO
ALTER TABLE [dbo].[UsuarioAtributo]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsuarioAtributo_dbo.UsuarioFuncionario_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[UsuarioFuncionario] ([Id])
GO
ALTER TABLE [dbo].[UsuarioAtributo] CHECK CONSTRAINT [FK_dbo.UsuarioAtributo_dbo.UsuarioFuncionario_UsuarioId]
GO
ALTER TABLE [dbo].[UsuarioNotificacao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsuarioNotificacao_dbo.UsuarioFuncionario_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[UsuarioFuncionario] ([Id])
GO
ALTER TABLE [dbo].[UsuarioNotificacao] CHECK CONSTRAINT [FK_dbo.UsuarioNotificacao_dbo.UsuarioFuncionario_UsuarioId]
GO
ALTER TABLE [dbo].[VagaRemocao]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.VagaRemocao_dbo.Cargo_CargoId] FOREIGN KEY([CargoId])
REFERENCES [dbo].[Cargo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VagaRemocao] CHECK CONSTRAINT [FK_dbo.VagaRemocao_dbo.Cargo_CargoId]
GO
ALTER TABLE [dbo].[VagaRemocao]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.VagaRemocao_dbo.Unidade_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VagaRemocao] CHECK CONSTRAINT [FK_dbo.VagaRemocao_dbo.Unidade_UnidadeId]
GO
ALTER TABLE [dbo].[VagaRemocaoPrecario]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.VagaRemocaoPrecario_dbo.Cargo_CargoId] FOREIGN KEY([CargoId])
REFERENCES [dbo].[Cargo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VagaRemocaoPrecario] CHECK CONSTRAINT [FK_dbo.VagaRemocaoPrecario_dbo.Cargo_CargoId]
GO
ALTER TABLE [dbo].[VagaRemocaoPrecario]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.VagaRemocaoPrecario_dbo.Unidade_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VagaRemocaoPrecario] CHECK CONSTRAINT [FK_dbo.VagaRemocaoPrecario_dbo.Unidade_UnidadeId]
GO
/****** Object:  StoredProcedure [dbo].[GerarLista]    Script Date: 12/01/2018 22:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GerarLista] 
	@FuncionarioId int
AS
	
SELECT 
	TOP 600
	RegiaoUnidade.Regiao AS 'Região',
	Unidade.Nome AS 'Unidade', 
	EnderecoUnidade.EnderecoCompleto AS 'Endereço', 
	CASE WHEN 
		Unidade.DificilAcesso IS NULL THEN ''
		ELSE CONCAT(Unidade.DificilAcesso, ' %') 
	END AS 'Difícil Acesso', 
	CASE WHEN 
		Unidade.TelefonePrincipal IS NULL THEN ''
		ELSE Unidade.TelefonePrincipal
	END AS 'Telefone',
	CASE WHEN 
		Unidade.Email IS NULL THEN ''
		ELSE Unidade.Email
	END AS 'E-mail' ,
	VagaRemocao.Vagas AS 'Vagas',
	ROUND(
		(6371 * 
				ACOS( 
					COS( RADIANS(Funcionario.Latitude) )  
					* COS( RADIANS( EnderecoUnidade.Latitude ) )
					* COS( RADIANS( EnderecoUnidade.Longitude ) - RADIANS(Funcionario.Longitude) ) 
					+ SIN( RADIANS(Funcionario.Latitude) ) 
					* SIN( RADIANS( EnderecoUnidade.Latitude) ) 
				) 
		), 
	3) AS 'Distância (Kms)',
	CONCAT(
		'https://maps.google.ca/maps?saddr=',
		Funcionario.Latitude,
		',',
		Funcionario.Longitude,
		'&daddr=',
		EnderecoUnidade.Latitude,',',
		EnderecoUnidade.Longitude,
		'&dirflg=d'
	) AS 'Google Maps'
FROM VagaRemocao
	INNER JOIN Cargo ON VagaRemocao.CargoId = Cargo.Id
	INNER JOIN Funcionario ON Cargo.Id = Funcionario.CargoId
	INNER JOIN Unidade ON VagaRemocao.UnidadeId = Unidade.Id
	INNER JOIN TipoUnidade ON Unidade.TipoUnidadeId = TipoUnidade.Id
	INNER JOIN EnderecoUnidade ON EnderecoUnidade.Id = Unidade.Id
	INNER JOIN RegiaoUnidade ON RegiaoUnidade.Id = Unidade.RegiaoUnidadeId
WHERE Funcionario.Id = @FuncionarioId 
		AND YEAR(VagaRemocao.Data) = (YEAR(GETDATE()) + 1)
		AND EnderecoUnidade.Latitude IS NOT NULL 
		AND EnderecoUnidade.Longitude IS NOT NULL
		AND VagaRemocao.Vagas > 0
ORDER BY 'Distância (Kms)'
GO
/****** Object:  StoredProcedure [dbo].[GerarListaP]    Script Date: 12/01/2018 22:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GerarListaP] 
	@FuncionarioId int
AS
	
SELECT 
	TOP 600
	RegiaoUnidade.Regiao AS 'Região',
	Unidade.Nome AS 'Unidade', 
	EnderecoUnidade.EnderecoCompleto AS 'Endereço', 
	CASE WHEN 
		Unidade.DificilAcesso IS NULL THEN ''
		ELSE CONCAT(Unidade.DificilAcesso, ' %') 
	END AS 'Difícil Acesso', 
	CASE WHEN 
		Unidade.TelefonePrincipal IS NULL THEN ''
		ELSE Unidade.TelefonePrincipal
	END AS 'Telefone',
	CASE WHEN 
		Unidade.Email IS NULL THEN ''
		ELSE Unidade.Email
	END AS 'E-mail' ,
	VagaRemocao.VagasIniciais AS 'Vagas Iniciais',
	VagaRemocao.VagasPotenciais AS 'Vagas Potênciais',
	ROUND(
		(6371 * 
				ACOS( 
					COS( RADIANS(Funcionario.Latitude) )  
					* COS( RADIANS( EnderecoUnidade.Latitude ) )
					* COS( RADIANS( EnderecoUnidade.Longitude ) - RADIANS(Funcionario.Longitude) ) 
					+ SIN( RADIANS(Funcionario.Latitude) ) 
					* SIN( RADIANS( EnderecoUnidade.Latitude) ) 
				) 
		), 
	3) AS 'Distância (Kms)',
	CONCAT(
		'https://maps.google.ca/maps?saddr=',
		Funcionario.Latitude,
		',',
		Funcionario.Longitude,
		'&daddr=',
		EnderecoUnidade.Latitude,',',
		EnderecoUnidade.Longitude,
		'&dirflg=d'
	) AS 'Google Maps'
FROM VagaRemocao
	INNER JOIN Cargo ON VagaRemocao.CargoId = Cargo.Id
	INNER JOIN Funcionario ON Cargo.Id = Funcionario.CargoId
	INNER JOIN Unidade ON VagaRemocao.UnidadeId = Unidade.Id
	INNER JOIN TipoUnidade ON Unidade.TipoUnidadeId = TipoUnidade.Id
	INNER JOIN EnderecoUnidade ON EnderecoUnidade.Id = Unidade.Id
	INNER JOIN RegiaoUnidade ON RegiaoUnidade.Id = Unidade.RegiaoUnidadeId
WHERE Funcionario.Id = @FuncionarioId 
		AND YEAR(VagaRemocao.Data) = (YEAR(GETDATE()) + 1)
		AND EnderecoUnidade.Latitude IS NOT NULL 
		AND EnderecoUnidade.Longitude IS NOT NULL
		AND VagaRemocao.Vagas > 0
ORDER BY 'Distância (Kms)'
GO
/****** Object:  StoredProcedure [dbo].[GerarListaPrecario]    Script Date: 12/01/2018 22:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GerarListaPrecario] 
	@FuncionarioId int
AS
	
SELECT 
	RegiaoUnidade.Regiao AS 'Região',
	Unidade.Nome AS 'Unidade', 
	EnderecoUnidade.EnderecoCompleto AS 'Endereço', 
	CASE WHEN 
		Unidade.DificilAcesso IS NULL THEN ''
		ELSE CONCAT(Unidade.DificilAcesso, ' %') 
	END AS 'Difícil Acesso', 
	CASE WHEN 
		Unidade.TelefonePrincipal IS NULL THEN ''
		ELSE Unidade.TelefonePrincipal
	END AS 'Telefone',
	CASE WHEN 
		Unidade.Email IS NULL THEN ''
		ELSE Unidade.Email
	END AS 'E-mail' ,
	VagaRemocaoPrecario.Vagas AS 'Vagas',
	ROUND(
		(6371 * 
				ACOS( 
					COS( RADIANS(Funcionario.Latitude) )  
					* COS( RADIANS( EnderecoUnidade.Latitude ) )
					* COS( RADIANS( EnderecoUnidade.Longitude ) - RADIANS(Funcionario.Longitude) ) 
					+ SIN( RADIANS(Funcionario.Latitude) ) 
					* SIN( RADIANS( EnderecoUnidade.Latitude) ) 
				) 
		), 
	3) AS 'Distância (Kms)',
	CONCAT(
		'https://maps.google.ca/maps?saddr=',
		Funcionario.Latitude,
		',',
		Funcionario.Longitude,
		'&daddr=',
		EnderecoUnidade.Latitude,',',
		EnderecoUnidade.Longitude,
		'&dirflg=d'
	) AS 'Google Maps'
FROM VagaRemocaoPrecario
	INNER JOIN Cargo ON VagaRemocaoPrecario.CargoId = Cargo.Id
	INNER JOIN Funcionario ON Cargo.Id = Funcionario.CargoId
	INNER JOIN Unidade ON VagaRemocaoPrecario.UnidadeId = Unidade.Id
	INNER JOIN TipoUnidade ON Unidade.TipoUnidadeId = TipoUnidade.Id
	INNER JOIN EnderecoUnidade ON EnderecoUnidade.Id = Unidade.Id
	INNER JOIN RegiaoUnidade ON RegiaoUnidade.Id = Unidade.RegiaoUnidadeId
WHERE Funcionario.Id = @FuncionarioId 
		AND YEAR(VagaRemocaoPrecario.Data) = (YEAR(GETDATE()))
		AND EnderecoUnidade.Latitude IS NOT NULL 
		AND EnderecoUnidade.Longitude IS NOT NULL
ORDER BY 'Distância (Kms)'
GO
USE [master]
GO
ALTER DATABASE [DataBaseRF] SET  READ_WRITE 
GO
