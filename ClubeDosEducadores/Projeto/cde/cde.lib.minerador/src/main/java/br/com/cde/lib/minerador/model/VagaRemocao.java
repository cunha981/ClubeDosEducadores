package br.com.cde.lib.minerador.model;

import java.time.LocalDate;

public class VagaRemocao {
	private Integer id;
	private Integer unidadeId;
	private Integer cargoId;
	private Integer vagas;
	private LocalDate periodo;
	private Integer vagasPotenciais;
	private Integer vagasIniciais;
	private Integer tipoVagaId;
	private LocalDate dataIniciais;
	private LocalDate dataPotenciais;
	
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
	public Integer getUnidadeId() {
		return unidadeId;
	}
	public void setUnidadeId(Integer unidadeId) {
		this.unidadeId = unidadeId;
	}
	public Integer getCargoId() {
		return cargoId;
	}
	public void setCargoId(Integer cargoId) {
		this.cargoId = cargoId;
	}
	public Integer getVagas() {
		return vagas;
	}
	public void setVagas(Integer vagas) {
		this.vagas = vagas;
	}
	public LocalDate getPeriodo() {
		return periodo;
	}
	public void setPeriodo(LocalDate periodo) {
		this.periodo = periodo;
	}
	public Integer getVagasPotenciais() {
		return vagasPotenciais;
	}
	public void setVagasPotenciais(Integer vagasPotenciais) {
		this.vagasPotenciais = vagasPotenciais;
	}
	public Integer getVagasIniciais() {
		return vagasIniciais;
	}
	public void setVagasIniciais(Integer vagasIniciais) {
		this.vagasIniciais = vagasIniciais;
	}
	public Integer getTipoVagaId() {
		return tipoVagaId;
	}
	public void setTipoVagaId(Integer tipoVagaId) {
		this.tipoVagaId = tipoVagaId;
	}
	public LocalDate getDataIniciais() {
		return dataIniciais;
	}
	public void setDataIniciais(LocalDate dataIniciais) {
		this.dataIniciais = dataIniciais;
	}
	public LocalDate getDataPotenciais() {
		return dataPotenciais;
	}
	public void setDataPotenciais(LocalDate dataPotenciais) {
		this.dataPotenciais = dataPotenciais;
	}
}
