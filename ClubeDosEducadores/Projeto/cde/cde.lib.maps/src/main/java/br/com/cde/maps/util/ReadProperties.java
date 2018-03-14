package br.com.cde.maps.util;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * Classe responsavel pela leitura do arquivo properties
 * 
 * @author Igor Almeida
 * 
 */
public final class ReadProperties {
	private final static String FILENAME = "config.properties";

	/**
	 * Acessar o arquivo properties
	 * 
	 * @return O arquivo properties
	 * @throws IOException
	 */
	private final static Properties getProperties() throws IOException {
		Properties properties = new Properties();
		InputStream inputStream = null;
		try {
			inputStream = ReadProperties.class.getClassLoader().getResourceAsStream(FILENAME);

			properties.load(inputStream);
			return properties;
		} finally {
			if (inputStream != null) {
				inputStream.close();
			}
		}
	}

	/**
	 * Busca pela propriedade no arquivo properties se nao encontrado retorna
	 * NULL
	 * 
	 * @param name
	 *            O nome da propriedade
	 * @return O valor da propriedade
	 * @throws IOException
	 */
	public static String getProperty(String name) throws IOException {
		return getProperties().getProperty(name);
	}
}
