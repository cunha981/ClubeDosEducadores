package br.com.cde.lib.smesp.core.test;

import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import br.com.cde.lib.smesp.core.UnidadeService;
import br.com.cde.lib.smesp.model.Unidade;

public class UnidadeServiceTest {
	private UnidadeService service;
	
	public UnidadeServiceTest() {
		service = new UnidadeService();
	}
	
	
	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
	}

	@Before
	public void setUp() throws Exception {
	}

	@Test
	public void testFindUnidade() {
		List<Unidade> unidades = null;
		try {
			unidades = service.findUnidade("helena lopes");
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		assertTrue("Unidades não encontradas", unidades != null && !unidades.isEmpty());
	}

}
