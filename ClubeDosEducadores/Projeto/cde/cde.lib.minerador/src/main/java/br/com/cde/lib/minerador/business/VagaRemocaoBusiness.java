package br.com.cde.lib.minerador.business;

import static br.com.cde.lib.minerador.util.RegexUtil.START_LETTER_PATTERN;

import java.io.IOException;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;
import org.apache.pdfbox.pdmodel.encryption.InvalidPasswordException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.cde.lib.minerador.dao.VagaRemocaoDAO;
import br.com.cde.lib.minerador.enumeration.TipoVaga;
import br.com.cde.lib.minerador.exception.BusinessException;
import br.com.cde.lib.minerador.model.Unidade;
import br.com.cde.lib.minerador.model.VagaRemocao;

@Service
public class VagaRemocaoBusiness {

	@Autowired
	private ExtratorPDFDOSPBusiness extratorPDFDOSPBusiness;

	@Autowired
	private UnidadeBusiness unidadeBusiness;
	
	@Autowired
	private CargoBusiness cargoBusiness;
	
	@Autowired
	private VagaRemocaoDAO vagaRemocaoDAO;
	
	private List<String> unidadesNaoEncontradas = new ArrayList<>();
	

	final static Logger logger = Logger.getLogger(VagaRemocaoBusiness.class);

	public List<VagaRemocao> minerar(Integer paginaInicial, LocalDate dataDO, TipoVaga tipoVaga)
			throws InvalidPasswordException, IOException, BusinessException {
		List<String> listaVagaremocao = extratorPDFDOSPBusiness.listarVagaRemocao(paginaInicial, dataDO);

		List<VagaRemocao> vagasRemocao = converterStringParaVagaRemocao(tipoVaga, dataDO, listaVagaremocao);
		vagaRemocaoDAO.save(vagasRemocao);//TODO:
		
		if(unidadesNaoEncontradas.size()>0){
			throw new BusinessException("Unidades não encontrada", unidadesNaoEncontradas);
		}
		return vagasRemocao;
	}

	private List<VagaRemocao> converterStringParaVagaRemocao(TipoVaga tipoVaga, LocalDate dataDO,
			List<String> listaVagaRemocao) {
		List<VagaRemocao> vagasRemocao = new ArrayList<>();
		VagaRemocao vagaRemocao = null;
		Unidade unidade = null;
		for (String li : listaVagaRemocao) {
			if (li.matches(START_LETTER_PATTERN)) {
				unidade = unidadeBusiness.obterUnidadePorNome(li);
				if(unidade == null){
					unidadesNaoEncontradas.add(li);
					logger.warn("Unidade não encontrada: "+li);
				}
			} else if(unidade != null){
				vagaRemocao = new VagaRemocao();
				
				vagaRemocao.setUnidadeId(unidade.getId());
				vagaRemocao.setCargoId(cargoBusiness.obterCargoIdPorCodCargo(li.substring(0, 5)));
				vagaRemocao.setTipoVagaId(tipoVaga.getId());
				vagaRemocao.setPeriodo(dataDO);
				
				vagaRemocao.setId(vagaRemocaoDAO.findIdByUnidadeIdAndCargoIdAndPeriodo(unidade.getId(),vagaRemocao.getCargoId(), dataDO));
				
				extrairVaga(vagaRemocao, li, tipoVaga);
				
				vagasRemocao.add(vagaRemocao);
			}else{
				unidadesNaoEncontradas.add(li);
				logger.warn(li);
			}
		}
		return vagasRemocao;
	}

	private static void extrairVaga(VagaRemocao vagaRemocao, String linha, TipoVaga tipoVaga) {
		String[] vagasSplit = linha.split("\\s");
		int indexUltimaVaga = vagasSplit.length - 1;

		if (tipoVaga.equals(TipoVaga.Potencial)) {
			vagaRemocao.setDataPotenciais(LocalDate.now());
			obterVagaTipoPotencial(vagaRemocao, vagasSplit, indexUltimaVaga);
		} else if (tipoVaga.equals(TipoVaga.Inicial)) {
			vagaRemocao.setDataIniciais(LocalDate.now());
			obterVagaTipoInicial(vagaRemocao, vagasSplit, indexUltimaVaga);
		}
	}

	private static void obterVagaTipoInicial(VagaRemocao vagaRemocao, String[] vagasSplit, int indexUltimaVaga) {
		int qtdVagaInicial = Integer.parseInt(vagasSplit[indexUltimaVaga]);

		vagaRemocao.setVagasIniciais(qtdVagaInicial);
		vagaRemocao.setVagas(qtdVagaInicial);
	}

	private static void obterVagaTipoPotencial(VagaRemocao vagaRemocao, String[] vagasSplit, int indexUltimaVaga) {
		int qtdVagaPotencial = Integer.parseInt(vagasSplit[indexUltimaVaga]);
		int qtdVagaInicial = Integer.parseInt(vagasSplit[indexUltimaVaga - 1]);

		vagaRemocao.setVagasPotenciais(qtdVagaPotencial);
		vagaRemocao.setVagasIniciais(qtdVagaInicial);
		vagaRemocao.setVagas(qtdVagaInicial+qtdVagaPotencial);
	}
}
