namespace DataAccess.Migrations
{
    using Helper;
    using Model;
    using Model.Enums;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
            EmailTemplateSeed(context);
            FaqSeed(context);
            UsuarioNotificacaoSeed(context);

            context.SaveChanges();
        }

        private static void FaqSeed(Context context)
        {
            var perguntas = context.Set<PerguntaFrequente>();

            if(!perguntas.Any())
            {
                perguntas.AddRange(new[]
                {
                    new PerguntaFrequente
                    {
                        Pergunta = "Que dia cai o pagamento?",
                        Resposta = "No �ltimo dia do m�s normalmente.",
                        Tags = "dia,pagamento,sal�rio,remunera��o,remuneracao"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quais s�o os meses de pagamento do PDE (Pr�mio de Desenvolvimento Educacional)?",
                        Resposta = "Metade em Janeiro e o restante em Julho.",
                        Tags = "pde,premio,desenvolvimento,educacional,janeiro,julho"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quanto tempo dura o est�gio probat�rio?",
                        Resposta = "O est�gio probat�rio tem 3 anos de dura��o",
                        Tags = "est�gio,estagio,probat�rio,probatorio,avaliacao,estabilidade"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Qual a finalidade do est�gio probat�rio?",
                        Resposta = "Ele serve como um per�odo de experi�ncia e depois dos 3 anos, adquire-se a estabilidade.",
                        Tags = "est�gio,estagio,probat�rio,probatorio,avaliacao,estabilidade"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "� poss�vel ser exonerado durante o est�gio probat�rio",
                        Resposta = "Sim, pelos motivos de inassiduidade, inefici�ncia, indisciplina, insubordina��o, falta de dedica��o ao servi�o ou m� conduta.",
                        Tags = "est�gio,estagio,probat�rio,probatorio,avaliacao,estabilidade,exoneracao,exonera��o"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "� poss�vel perder o cargo p�blico ap�s passado o est�gio probat�rio?",
                        Resposta = "Sim, ap�s passado o est�gio probat�rio, o servidor p�blico est�vel s� perder� o cargo em virtude de senten�a judicial transitada em julgado, ou mediante processo administrativo em que lhe seja assegurada ampla defesa ou mediante procedimento de avalia��o peri�dica de desempenho, na forma de lei complementar, assegurada ampla defesa.",
                        Tags = "estabilidade,exoneracao,exonera��o,servidor,cargo,demissao,perder,cargo"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "O que � posse?",
                        Resposta = "Posse � o ato pelo qual a pessoa � investida em cargo p�blico, verificar-se-� mediante a assinatura, pela autoridade competente e pelo funcion�rio, do termo pelo qual este se compromete a observar fielmente os deveres e atribui��es do cargo, bem como as exig�ncias deste Estatuto.",
                        Tags = "posse,funcionario,funcion�rio,cargo,publico"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Ap�s a escolha da unidade, quanto tempo tenho para tomar posse?",
                        Resposta = "15 dias contados da publica��o em Di�rio Oficial, e prorrog�veis por mais 15 dias, entretanto, deve comparecer no local que trabalhar� nos primeiros 15 dias, e justificar a prorroga��o. Caso n�o compare�a no per�odo indicado, perde-se todos os efeitos.",
                        Tags = "posse,15,dias,unidade,diario,oficial,di�rio"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "O que � remo��o?",
                        Resposta = "Remo��o � o deslocamento do funcion�rio de uma unidade para outra. A remo��o poder� ser feita no final de cada ano ou por permuta.",
                        Tags = "remocao,remo��o"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Como funciona a remo��o por concurso?",
                        Resposta = "Todo final de ano a SME divulga a lista de vagas dispon�veis para remo��o, ent�o atrav�s desta lista os funcion�rios que se inscreveram para remo��o devem cadastrar sua lista de unidades que h� prefer�ncia de se remover, � recomend�vel sempre colocar o m�ximo de unidades poss�vel na lista para n�o correr o risco de n�o conseguir se remover, pois o concurso de remo��o � classificado por tempo de antiguidade e outros crit�rios que agregam pontos na classifica��o.",
                        Tags = "remocao,remo��o,concurso,deslocamento"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Como funciona a remo��o por permuta?",
                        Resposta = "Ser� processada a pedido escrito dos interessados, com a concord�ncia das respectivas chefias, e caso o outro servidor queira a sua Unidade e vice-versa, comparecer�o a Diretoria Regional respectiva e devem requerer a solicita��o ap�s aprovado por suas chefias.",
                        Tags = "remocao,remo��o,concurso,deslocamento,permuta"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quando deve ser assumido o cargo quando o servidor for removido para outra Unidade?",
                        Resposta = "O funcion�rio removido dever� assumir de imediato o exerc�cio na unidade para a qual foi deslocado, salvo quando em f�rias, licen�a ou desempenho de cargo em comiss�o, hip�teses em que dever� apresentar-se no primeiro dia �til ap�s o t�rmino do impedimento.",
                        Tags = "remocao,remo��o,concurso,deslocamento,permuta,assumir"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "� poss�vel acumular cargos?",
                        Resposta = "S� � poss�vel acumular cargos nos seguintes casos: <br />I - de ju�z com um cargo de professor; <br />II - a de dois cargos de professor; <br /> III - a de uma cargo de professor com outro t�cnico ou cient�fico; ou IV -a de dois cargos privativos de m�dico.<br />� 1� - Em qualquer dos casos previstos neste artigo, a acumula��o somente ser� permitida havendo correla��o de mat�rias e compatibilidade de hor�rio.<br />� 2� - A proibi��o de acumular se estende a cargos, fun��es e empregos em autarquias, empresas p�blicas e sociedade de economia mista.<br />� 3� - A proibi��o de acumular proventos n�o se aplica aos aposentados quanto ao exerc�cio de mandato eletivo, quanto ao de um cargo em comiss�o, ou quanto a contrato para presta��o de servi�os t�cnicos ou especializados.",
                        Tags = "cargo,acumulo,cumulo,acumular,cumular"
                    }
                });
            }
        }

        private static void EmailTemplateSeed(Context context)
        {
            var emailsTemplateDB = context.Set<MailTemplate>().ToArray();
            var emailsTemplate = MailTemplate.GetTemplates().Where(a => !emailsTemplateDB.Any(b => (a.Key == b.Key && a.Version == b.Version)));

            emailsTemplate.Foreach(emailTemplate =>
            {
                if (!emailsTemplateDB.Any(a => a.Key == emailTemplate.Key && a.Version == emailTemplate.Version))
                {
                    context.Set<MailTemplate>().AddOrUpdate(emailTemplate);
                }

                return emailTemplate;
            });
        }

        private static void UsuarioNotificacaoSeed(Context context)
        {
            if(context.Set<UsuarioNotificacao>().Any())
            {
                return;
            }

            var tiposEvento = EnumExtensions.GetEnumerable<TipoEventoEnum>();
            var usersId = context.Set<UsuarioFuncionario>().Select(a => a.Id).ToArray();

            foreach (var tipoEvento in tiposEvento)
            {
                context.Set<UsuarioNotificacao>().AddRange(usersId.Select(userId => new UsuarioNotificacao
                {
                    TipoEvento = tipoEvento,
                    UsuarioId = userId,
                    Notificar = true
                }));
            }
        }
    }
}
