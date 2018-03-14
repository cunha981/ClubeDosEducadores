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
                        Resposta = "No último dia do mês normalmente.",
                        Tags = "dia,pagamento,salário,remuneração,remuneracao"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quais são os meses de pagamento do PDE (Prêmio de Desenvolvimento Educacional)?",
                        Resposta = "Metade em Janeiro e o restante em Julho.",
                        Tags = "pde,premio,desenvolvimento,educacional,janeiro,julho"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quanto tempo dura o estágio probatório?",
                        Resposta = "O estágio probatório tem 3 anos de duração",
                        Tags = "estágio,estagio,probatório,probatorio,avaliacao,estabilidade"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Qual a finalidade do estágio probatório?",
                        Resposta = "Ele serve como um período de experiência e depois dos 3 anos, adquire-se a estabilidade.",
                        Tags = "estágio,estagio,probatório,probatorio,avaliacao,estabilidade"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "É possível ser exonerado durante o estágio probatório",
                        Resposta = "Sim, pelos motivos de inassiduidade, ineficiência, indisciplina, insubordinação, falta de dedicação ao serviço ou má conduta.",
                        Tags = "estágio,estagio,probatório,probatorio,avaliacao,estabilidade,exoneracao,exoneração"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "É possível perder o cargo público após passado o estágio probatório?",
                        Resposta = "Sim, após passado o estágio probatório, o servidor público estável só perderá o cargo em virtude de sentença judicial transitada em julgado, ou mediante processo administrativo em que lhe seja assegurada ampla defesa ou mediante procedimento de avaliação periódica de desempenho, na forma de lei complementar, assegurada ampla defesa.",
                        Tags = "estabilidade,exoneracao,exoneração,servidor,cargo,demissao,perder,cargo"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "O que é posse?",
                        Resposta = "Posse é o ato pelo qual a pessoa é investida em cargo público, verificar-se-á mediante a assinatura, pela autoridade competente e pelo funcionário, do termo pelo qual este se compromete a observar fielmente os deveres e atribuições do cargo, bem como as exigências deste Estatuto.",
                        Tags = "posse,funcionario,funcionário,cargo,publico"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Após a escolha da unidade, quanto tempo tenho para tomar posse?",
                        Resposta = "15 dias contados da publicação em Diário Oficial, e prorrogáveis por mais 15 dias, entretanto, deve comparecer no local que trabalhará nos primeiros 15 dias, e justificar a prorrogação. Caso não compareça no período indicado, perde-se todos os efeitos.",
                        Tags = "posse,15,dias,unidade,diario,oficial,diário"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "O que é remoção?",
                        Resposta = "Remoção é o deslocamento do funcionário de uma unidade para outra. A remoção poderá ser feita no final de cada ano ou por permuta.",
                        Tags = "remocao,remoção"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Como funciona a remoção por concurso?",
                        Resposta = "Todo final de ano a SME divulga a lista de vagas disponíveis para remoção, então através desta lista os funcionários que se inscreveram para remoção devem cadastrar sua lista de unidades que há preferência de se remover, é recomendável sempre colocar o máximo de unidades possível na lista para não correr o risco de não conseguir se remover, pois o concurso de remoção é classificado por tempo de antiguidade e outros critérios que agregam pontos na classificação.",
                        Tags = "remocao,remoção,concurso,deslocamento"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Como funciona a remoção por permuta?",
                        Resposta = "Será processada a pedido escrito dos interessados, com a concordância das respectivas chefias, e caso o outro servidor queira a sua Unidade e vice-versa, comparecerão a Diretoria Regional respectiva e devem requerer a solicitação após aprovado por suas chefias.",
                        Tags = "remocao,remoção,concurso,deslocamento,permuta"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "Quando deve ser assumido o cargo quando o servidor for removido para outra Unidade?",
                        Resposta = "O funcionário removido deverá assumir de imediato o exercício na unidade para a qual foi deslocado, salvo quando em férias, licença ou desempenho de cargo em comissão, hipóteses em que deverá apresentar-se no primeiro dia útil após o término do impedimento.",
                        Tags = "remocao,remoção,concurso,deslocamento,permuta,assumir"
                    },
                    new PerguntaFrequente
                    {
                        Pergunta = "É possível acumular cargos?",
                        Resposta = "Só é possível acumular cargos nos seguintes casos: <br />I - de juíz com um cargo de professor; <br />II - a de dois cargos de professor; <br /> III - a de uma cargo de professor com outro técnico ou científico; ou IV -a de dois cargos privativos de médico.<br />§ 1º - Em qualquer dos casos previstos neste artigo, a acumulação somente será permitida havendo correlação de matérias e compatibilidade de horário.<br />§ 2º - A proibição de acumular se estende a cargos, funções e empregos em autarquias, empresas públicas e sociedade de economia mista.<br />§ 3º - A proibição de acumular proventos não se aplica aos aposentados quanto ao exercício de mandato eletivo, quanto ao de um cargo em comissão, ou quanto a contrato para prestação de serviços técnicos ou especializados.",
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
