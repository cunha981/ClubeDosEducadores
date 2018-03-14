package br.com.cde.lib.smesp.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Address {
	private String end_cep;
	private String end_logradouro;
	private String end_bairro;
	private String end_distrito;
	private String end_id;
	
	@JsonProperty("end_cep")
	public String getEnd_cep() {
		return end_cep;
	}
	public void setEnd_cep(String end_cep) {
		this.end_cep = end_cep;
	}
	@JsonProperty("end_logradouro")
	public String getEnd_logradouro() {
		return end_logradouro;
	}
	public void setEnd_logradouro(String end_logradouro) {
		this.end_logradouro = end_logradouro;
	}
	@JsonProperty("end_bairro")
	public String getEnd_bairro() {
		return end_bairro;
	}
	public void setEnd_bairro(String end_bairro) {
		this.end_bairro = end_bairro;
	}
	@JsonProperty("tua_id")
	public String end_distrito() {
		return end_distrito;
	}
	public void setEnd_distrito(String end_distrito) {
		this.end_distrito = end_distrito;
	}
	@JsonProperty("end_id")
	public String getEnd_id() {
		return end_id;
	}
	public void setEnd_id(String end_id) {
		this.end_id = end_id;
	}
}
