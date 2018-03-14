package br.com.cde.lib.minerador.business;

import static br.com.cde.lib.minerador.util.RegexUtil.PREFIX_NAME_PATTERN;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.cde.lib.minerador.dao.UnidadeDAO;
import br.com.cde.lib.minerador.model.Unidade;
import br.com.cde.lib.smesp.core.UnidadeService;

@Service
public class UnidadeBusiness {

	@Autowired
	private UnidadeDAO unidadeDAO;

	public Unidade obterUnidadePorNome(String nome) {
		Unidade unidade = unidadeDAO.findUnidadeByName(nome);
		if (unidade == null) {
			UnidadeService us = new UnidadeService();
			String nomeTofind = nome.replaceAll(PREFIX_NAME_PATTERN, "");

			List<br.com.cde.lib.smesp.model.Unidade> unidades = null;
			try {
				unidades = us.findUnidade(nomeTofind);
			} catch (Exception e) {
				e.printStackTrace();
			}

			if (unidades != null && unidades.size() == 1) {
				br.com.cde.lib.smesp.model.Unidade uni = unidades.get(0);
				unidade = new Unidade();
				
				unidade.setNome(nome);
				unidade.setBairro(uni.getAddress().getEnd_bairro());
				unidade.setCep(uni.getAddress().getEnd_cep());
				unidade.setLogradouro(uni.getAddress().getEnd_logradouro());
				unidade.setCidade(uni.getCity().getCid_nome());
				unidade.setUf("SP");
				unidade.setNumero(uni.getUae_numero());
				unidade.setEndereco(uni.getAddress().getEnd_logradouro() + ", " + uni.getUae_numero() + " - "
						+ uni.getAddress().getEnd_bairro() + ", " + uni.getAddress().getEnd_cep());
				unidade.setLatitude(Double.parseDouble(uni.getLatitude()));
				unidade.setLongitude(Double.parseDouble(uni.getLongitude()));
				unidade.setEmail(uni.getEmail());
				unidade.setTelPrincipal(uni.getPhone());
				unidade.setUrl(uni.getUrlMoreInfo());
				unidade.setCodigo(uni.getCode());

				unidade.setDiretoriaregionalId(unidadeDAO.findDiretoriaRegionalIdByChave(uni.getAdministrativeUnitSuperiorId()));// TODO:
													// findDiretoriaRegionalIdByChave
				unidade.setRegiaoUnidadeId(unidadeDAO.findRegiaoUnidadeIdByChave(uni.getAdministrativeUnitSuperiorId()));// TODO:
												// findRegiaoUnidadeIdByChave
				String tipoUnidade = nome.replaceFirst("\\s.*", "");
				unidade.setTipoUnidadeId(unidadeDAO.findTipoUnidadeIdByTipo(tipoUnidade)); // TODO: 
												// findTipoUnidadeIdByTipo
				
				unidadeDAO.save(unidade);

			}
		}
		return unidade;
	}

}
