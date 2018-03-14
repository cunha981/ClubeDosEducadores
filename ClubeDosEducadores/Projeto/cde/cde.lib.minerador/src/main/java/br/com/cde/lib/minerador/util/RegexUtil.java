package br.com.cde.lib.minerador.util;

public class RegexUtil {
	
	public final static String LINE_BREAK_PATTERN = "\\r\\n|\\r|\\n";
	public final static String START_LETTER_PATTERN = "^[A-Z].*";
	public final static String START_5_DIGITS_TRACE_PATTERN = "^[0-9]{5}-.*";
	public final static String MIDDLE_5_DIGITS_TRACE_PATTERN = "[0-9]{5}-.*?([0-9]{5}-)";
	public final static String PREFIX_NAME_PATTERN = "^CEI DIRET |^CEMEI |^CEU CEI |^CEU EMEF |^CEU EMEI |^CEU |^CIEJA |^CMCT |^EMEBS |^EMEF |^EMEI ";
	public final static String PREFIX_NAME_ANY_PATTERN = "^CEI DIRET.*|^CEMEI.*|^CEU CEI.*|^CEU EMEF.*|^CEU EMEI.*|^CEU.*|^CIEJA.*|^CMCT.*|^EMEBS.*|^EMEF.*|^EMEI.*";
}
