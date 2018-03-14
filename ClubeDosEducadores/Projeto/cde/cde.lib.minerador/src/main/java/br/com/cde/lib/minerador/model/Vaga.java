package br.com.cde.lib.minerador.model;

import java.time.LocalDate;

import br.com.cde.lib.minerador.enumeration.TipoVaga;

public class Vaga {
	private String codCargo;
	private Integer cargoId;
	private LocalDate dataDO;
	private TipoVaga tipoVaga;
	private Integer qtdInicial;
	private Integer qtdPotencial;

	public String getCodCargo() {
		return codCargo;
	}

	public void setCodCargo(String codCargo) {
		this.codCargo = codCargo;
	}

	public Integer getCargoId() {
		return cargoId;
	}

	public void setCargoId(Integer cargoId) {
		this.cargoId = cargoId;
	}

	public LocalDate getDataDO() {
		return dataDO;
	}

	public void setDataDO(LocalDate dataDO) {
		this.dataDO = dataDO;
	}

	public TipoVaga getTipoVaga() {
		return tipoVaga;
	}

	public void setTipoVaga(TipoVaga tipoVaga) {
		this.tipoVaga = tipoVaga;
	}

	public Integer getQtdInicial() {
		return qtdInicial;
	}

	public void setQtdInicial(Integer qtdInicial) {
		this.qtdInicial = qtdInicial;
	}

	public Integer getQtdPotencial() {
		return qtdPotencial;
	}

	public void setQtdPotencial(Integer qtdPotencial) {
		this.qtdPotencial = qtdPotencial;
	}
}
