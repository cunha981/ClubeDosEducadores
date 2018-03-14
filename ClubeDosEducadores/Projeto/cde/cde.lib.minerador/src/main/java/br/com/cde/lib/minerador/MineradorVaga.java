package br.com.cde.lib.minerador;

import java.io.IOException;
import java.time.LocalDate;

import org.apache.pdfbox.pdmodel.encryption.InvalidPasswordException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import br.com.cde.lib.minerador.business.VagaRemocaoBusiness;
import br.com.cde.lib.minerador.enumeration.TipoVaga;
import br.com.cde.lib.minerador.exception.BusinessException;

@Component
public class MineradorVaga {
	
	@Autowired
	private VagaRemocaoBusiness vagaRemocaoBusiness;
	
	synchronized public void minerarVagaRemocaoDOSP(final Integer paginaInicial)
			throws InvalidPasswordException, IOException, BusinessException{
		vagaRemocaoBusiness.minerar(paginaInicial, LocalDate.now(), TipoVaga.Potencial);
		
		/*
		 * TODO:
		 * 	TipoVagaId é o mesmo para o Banco, muda apenas na extração(Inicial e Potencial)
		 * 	Verificar se unidade existe, sim: obter UnidadeId / nao: cadastrar e obter UnidadeId
		 * 	Obter CargoId
		 * 	Periodo - data do DO
		 * 	-> Chamar save
		 * */
	}
}
