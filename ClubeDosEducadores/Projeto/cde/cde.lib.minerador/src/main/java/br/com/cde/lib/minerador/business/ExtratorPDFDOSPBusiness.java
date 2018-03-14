package br.com.cde.lib.minerador.business;

import static br.com.cde.lib.minerador.util.RegexUtil.*;

import java.awt.Rectangle;
import java.io.IOException;
import java.net.URL;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPage;
import org.apache.pdfbox.pdmodel.encryption.InvalidPasswordException;
import org.apache.pdfbox.text.PDFTextStripperByArea;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import br.com.cde.lib.minerador.util.URLUtil;

@Service
public class ExtratorPDFDOSPBusiness {

	@Value("${url-pdf-dosp}")
	private String URL_BASE_PDF;

	private Boolean leuFinal;

	private Boolean leuInicio;

	public List<String> listarVagaRemocao(Integer paginaInicial, LocalDate dataDO)
			throws InvalidPasswordException, IOException {
		StringBuilder dest = new StringBuilder();
		leuFinal = false;
		leuInicio = false;

		if (paginaInicial != null & paginaInicial > 0) {
			for (int pagina = paginaInicial; !leuFinal; pagina++) {

				URL urlPagina = URLUtil.formatarURL(pagina, dataDO, URL_BASE_PDF);
				obterVagasRemocaoPDF(urlPagina, dest);
			}
		} else {
			throw new IllegalArgumentException("O parametro passado deve ser maior que zero.");
		}
		String[] listaExtraida = dest.toString().split(LINE_BREAK_PATTERN);
		List<String> listaVagaRemocao = tratarExtracao(listaExtraida);

		return listaVagaRemocao;
	}

	private List<String> tratarExtracao(String[] listaExtraida) {
		List<String> listaVagaTratada = new ArrayList<>(Arrays.asList(listaExtraida));
		removerLinha(listaVagaTratada, ".*INICIAIS.*POTENCIAIS.*", ".*CARGO.*VAGAS.*");
		removerQuebraLinha(listaVagaTratada);

		return listaVagaTratada;
	}
	//TODO: tratar extracao vagas precarias, diretoria antes da unidade
	private void removerQuebraLinha(List<String> listaVagaTratada) {
		for (int i = 0, k = 1; k < listaVagaTratada.size(); i++, k++) {
			String li = listaVagaTratada.get(i);
			if (li.matches(START_LETTER_PATTERN)) {
				String liNext = listaVagaTratada.get(k);
				if (!liNext.matches(START_5_DIGITS_TRACE_PATTERN)) {
					
					if(liNext.matches(PREFIX_NAME_ANY_PATTERN)){
						listaVagaTratada.remove(i--);
						k--;
						continue;
					}
					
					if (!li.equals(liNext)) {
						if (li.matches(".*[A-Z]-$")) {
							li = li.replaceFirst("-$", "") + liNext;
						} 
						//else if (li.matches(".*U$|.*PRO$|.*R$|.*/$|.*N$|.*TRE$")) {
							//li += liNext;
						//}
						else {
							li += liNext;
						}
					}
					listaVagaTratada.remove(k);
					listaVagaTratada.remove(i);
					listaVagaTratada.add(i, li);

					String liBack = listaVagaTratada.get(i - 1);
					if (li.equals(liBack)) {
						listaVagaTratada.remove(i);
					}
				}
			} else {
				Pattern pattern = Pattern.compile(MIDDLE_5_DIGITS_TRACE_PATTERN);
				Matcher matcher = pattern.matcher(li);
				if (matcher.find()) {
					int length_5_digits_trace = 6;
					int index = matcher.end() - length_5_digits_trace;

					String linhaAtual = li.substring(0, index).trim();
					String linhaSeguinte = li.substring(index);
					linhaSeguinte += " " + listaVagaTratada.get(k);

					listaVagaTratada.remove(k);
					listaVagaTratada.remove(i);

					listaVagaTratada.add(i, linhaAtual);
					listaVagaTratada.add(k, linhaSeguinte);
				}
			}
		}
	}

	private void removerLinha(List<String> lista, String... regexs) {
		for (String regex : regexs) {
			lista.removeIf(x -> x.matches(regex));
		}
	}

	private void obterVagasRemocaoPDF(URL urlPagina, StringBuilder dest) throws IOException {
		StringBuilder copiaTexto = clonarPagina(urlPagina);
		copiaTexto = tratarCopiaVagaRemocao(copiaTexto);

		dest.append(copiaTexto);

	}

	private StringBuilder tratarCopiaVagaRemocao(StringBuilder copiaTexto) {
		if (!leuInicio) {
			copiaTexto = new StringBuilder(getTextoInicioDocumento(copiaTexto.toString()));
		} else {
			copiaTexto = new StringBuilder(getTextoFimDocumento(copiaTexto.toString()));
		}
		return copiaTexto;
	}

	private String getTextoFimDocumento(String copiaTexto) {
		String textoFim = copiaTexto;
		int index = obterIndexUltimaVaga(copiaTexto);
		if (index != -1) {
			textoFim = copiaTexto.substring(0, index - 1);
			leuFinal = true;
		}
		return textoFim;
	}

	/*
	 * Se a linha nao for vaga, verifico as proximas 5 linhas, se elas nao forem
	 * vagas significa que é o fim do pdf
	 */
	private static int obterIndexUltimaVaga(String copiaTexto) {
		int index = -1;
		int check_5_next_lines = 6;
		int isLine5 = 5;
		String[] linhas = copiaTexto.split(LINE_BREAK_PATTERN);

		for (int i = 0; i < linhas.length; i++) {
			if (!linhas[i].matches(START_5_DIGITS_TRACE_PATTERN)) {
				check: for (int t = 1; t < check_5_next_lines; t++) {
					if ((i + t) < linhas.length) {
						String liNext = linhas[i + t];
						if (!liNext.matches(START_5_DIGITS_TRACE_PATTERN) && t == isLine5) {
							index = copiaTexto.indexOf(linhas[i]);
							return index;
						} else if (liNext.matches(START_5_DIGITS_TRACE_PATTERN)) {
							break check;
						}
					} else {
						break;
					}
				}
			}
		}
		return index;
	}

	private String getTextoInicioDocumento(String copiaTexto) {
		String textoInicio = copiaTexto;
		int indexInicio = obterIndexPrimeiraUnidade(copiaTexto);
		if (indexInicio != -1) {
			textoInicio = copiaTexto.substring(indexInicio, copiaTexto.length() - 1);
		}
		leuInicio = true;
		return textoInicio;
	}

	private static int obterIndexPrimeiraUnidade(String copiaTexto) {
		String TEXT_TO_FIND_BEGIN = "CARGO VAGAS INICIAIS";
		int space_next_line = 2;
		int index = copiaTexto.indexOf(TEXT_TO_FIND_BEGIN);
		if (index > -1) {
			index += TEXT_TO_FIND_BEGIN.length() + space_next_line;
			return index;
		}
		String[] linhas = copiaTexto.split(LINE_BREAK_PATTERN);
		String liNext;

		for (int i = 0; i < linhas.length; i++) {
			if (linhas[i].matches(START_LETTER_PATTERN)) {
				liNext = linhas[i + 1];
				if (liNext.matches(START_5_DIGITS_TRACE_PATTERN)) {
					return copiaTexto.indexOf(linhas[i]);
				}
			}
		}
		return index;
	}

	private static StringBuilder clonarPagina(URL urlPagina) throws IOException {
		int larguraColuna = 179;
		int alturaColuna = 1125;
		int eixoY = 50;
		int eixoX = 39;
		int espacoEntreColuna = 0;
		int numeroDeColuna = 4;
		PDPage paginaPDF = null;
		StringBuilder copiaTexto = new StringBuilder();
		PDFTextStripperByArea stripper = new PDFTextStripperByArea();
		stripper.setSortByPosition(true);

		try (PDDocument document = PDDocument.load(urlPagina.openStream())) {
			if (!document.isEncrypted()) {
				paginaPDF = document.getDocumentCatalog().getPages().get(0);

				for (int i = 0; i < numeroDeColuna; i++) {
					// a cada iteracao movo o eixo X para a proxima coluna
					if (i > 0)
						eixoX += larguraColuna + espacoEntreColuna;

					Rectangle areaDeCopia = new Rectangle(eixoX, eixoY, larguraColuna, alturaColuna);
					stripper.addRegion("coluna", areaDeCopia);
					stripper.extractRegions(paginaPDF);

					copiaTexto.append(stripper.getTextForRegion("coluna"));
				}
			}
		}
		return copiaTexto;
	}
}
