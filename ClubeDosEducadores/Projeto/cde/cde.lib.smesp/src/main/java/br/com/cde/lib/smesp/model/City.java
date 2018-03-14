package br.com.cde.lib.smesp.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class City {
	private String cid_nome;
	private String cid_id;
	
	@JsonProperty("cid_nome")
	public String getCid_nome() {
		return cid_nome;
	}
	public void setCid_nome(String cid_nome) {
		this.cid_nome = cid_nome;
	}
	@JsonProperty("cid_id")
	public String getCid_id() {
		return cid_id;
	}
	public void setCid_id(String cid_id) {
		this.cid_id = cid_id;
	}
}
