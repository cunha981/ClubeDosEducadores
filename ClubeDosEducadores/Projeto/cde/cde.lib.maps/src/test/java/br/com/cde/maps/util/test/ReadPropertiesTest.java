package br.com.cde.maps.util.test;

import static org.junit.Assert.assertEquals;

import java.io.IOException;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import br.com.cde.maps.util.ReadProperties;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration
public class ReadPropertiesTest {

	@Test	
	public void testGetProperty() {
		try {
			assertEquals("Teste", ReadProperties.getProperty("teste"));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
