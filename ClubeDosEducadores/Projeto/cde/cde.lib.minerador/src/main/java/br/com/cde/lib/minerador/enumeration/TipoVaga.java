package br.com.cde.lib.minerador.enumeration;

import java.util.HashMap;

public enum TipoVaga {
	Inicial(1, 1), Potencial(2, 1), Precaria(3, 2);

	private int valorView;
	private int id;
	private static HashMap<Integer, TipoVaga> map = new HashMap<Integer, TipoVaga>();

	TipoVaga(int valorView, int id) {
		this.valorView = valorView;
		this.id = id;
	}

	static {
		for (TipoVaga pageType : TipoVaga.values()) {
			map.put(pageType.valorView, pageType);
		}
	}

	public int getValorView() {
		return valorView;
	}

	public int getId() {
		return id;
	}
}
