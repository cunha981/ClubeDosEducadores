package br.com.cde.lib.smesp.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class AdministrativeSuperior {
	private String tua_id;
	private String uad_nome;
	private String uad_id;
	
	@JsonProperty("tua_id")
	public String getTua_id() {
		return tua_id;
	}
	public void setTua_id(String tua_id) {
		this.tua_id = tua_id;
	}
	@JsonProperty("uad_nome")
	public String getUad_nome() {
		return uad_nome;
	}
	public void setUad_nome(String uad_nome) {
		this.uad_nome = uad_nome;
	}
	@JsonProperty("uad_id")
	public String getUad_id() {
		return uad_id;
	}
	public void setUad_id(String uad_id) {
		this.uad_id = uad_id;
	}
}
