package br.com.cde.lib.minerador.enumeration;

import java.util.HashMap;

public enum Mes {
	Janeiro(1, "Janeiro"), Fevereiro(2, "Fevereiro"), Marco(3, "Marco"), Abril(4, "Abril"), Maio(5, "Maio"), Junho(6,
			"Junho"), Julho(7, "Julho"), Agosto(8, "Agosto"), Setembro(9,
					"Setembro"), Outubro(10, "Outubro"), Novembro(11, "Novembro"), Dezembro(12, "Dezembro");

	private int valor;
	private String mes;
	private static HashMap<Integer, Mes> map = new HashMap<Integer, Mes>();

	private Mes(int valor, String mes) {
		this.valor = valor;
		this.mes = mes;
	}

	static {
		for (Mes pageType : Mes.values()) {
			map.put(pageType.valor, pageType);
		}
	}

	public static Mes valueOf(Integer valor) {
		return map.get(valor);
	}

	public int getValor() {
		return valor;
	}

	public String getMes() {
		return mes;
	}
}
