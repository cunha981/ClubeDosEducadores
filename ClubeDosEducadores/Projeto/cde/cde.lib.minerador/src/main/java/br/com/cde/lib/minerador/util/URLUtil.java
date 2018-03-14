package br.com.cde.lib.minerador.util;

import java.net.MalformedURLException;
import java.net.URL;
import java.time.LocalDate;

import br.com.cde.lib.minerador.enumeration.Mes;

public class URLUtil {
	private static final int DOIS_DIGITO = 10;
	private static final int TRES_DIGITO = 100;
	private static final int QUATRO_DIGITO = 1000;
	
	public static URL formatarURL(Integer pagina, LocalDate data ,String url) throws MalformedURLException {
		String paginaFormat = formatarPaginaURL(pagina);
		String dataFormat = formatarDataURL(data);
		
		String urlFormat = String.format(url, dataFormat,paginaFormat);
		
		return new URL(urlFormat);
	}

	private static String formatarDataURL(LocalDate data) {
		Integer ano = data.getYear();
		Integer dia = data.getDayOfMonth();
		String mes = Mes.valueOf(data.getMonthValue()).getMes();
		
		String dataFormat = ano + "/" + mes + "/" + dia;
		
		return dataFormat;
	}

	private static String formatarPaginaURL(Integer pagina) {
		String paginaFormat = null;
		
		if (pagina < DOIS_DIGITO) {
			paginaFormat =  "000" + pagina.toString();
		} else if (pagina < TRES_DIGITO) {
			paginaFormat =  "00" + pagina.toString();
		} else if (pagina < QUATRO_DIGITO) {
			paginaFormat = "0" + pagina.toString();
		} else {
			paginaFormat = pagina.toString();
		}
		
		return paginaFormat;
	}

}
