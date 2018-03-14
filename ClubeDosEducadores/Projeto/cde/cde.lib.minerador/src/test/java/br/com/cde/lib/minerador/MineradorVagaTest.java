package br.com.cde.lib.minerador;

import static org.junit.Assert.assertTrue;

import java.io.IOException;
import java.time.LocalDate;
import java.util.List;

import org.apache.pdfbox.pdmodel.encryption.InvalidPasswordException;
import org.junit.FixMethodOrder;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.MethodSorters;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringBootConfiguration;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import br.com.cde.lib.minerador.business.VagaRemocaoBusiness;
import br.com.cde.lib.minerador.enumeration.TipoVaga;
import br.com.cde.lib.minerador.exception.BusinessException;
import br.com.cde.lib.minerador.model.VagaRemocao;

@SpringBootTest
@SpringBootConfiguration
@RunWith(SpringJUnit4ClassRunner.class)	
@FixMethodOrder(MethodSorters.NAME_ASCENDING)
@ComponentScan("br.com.cde.lib.minerador.*")
public class MineradorVagaTest {
	
	@Autowired
	private VagaRemocaoBusiness vagaRemocaoBusiness;

	@Test
	public void minerarVagaRemocaoDOSPInicial() throws InvalidPasswordException, IOException, BusinessException {
		List<VagaRemocao> listaVagaRemocao = vagaRemocaoBusiness.minerar(41, LocalDate.of(2017, 9, 29), TipoVaga.Inicial);
		VagaRemocao r = listaVagaRemocao.get(0);
		
		assertTrue(r != null);
		assertTrue(r.getUnidadeId() > 0);
	}
	@Test
	public void minerarVagaRemocaoDOSPPotencial() throws InvalidPasswordException, IOException, BusinessException {
		List<VagaRemocao> listaVagaRemocao = vagaRemocaoBusiness.minerar(123, LocalDate.of(2017, 10, 27), TipoVaga.Potencial);
		VagaRemocao r = listaVagaRemocao.get(0);
		
		assertTrue(r != null);
	}
}
