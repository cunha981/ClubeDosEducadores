package br.com.cde.lib.minerador.model;

import java.util.List;

public class Unidade {
	private Integer id;
	private String nome;
	private String codigo;
	private String telPrincipal;
	private long tipoUnidadeId;
	private String email;
	private long regiaoUnidadeId;
	private long diretoriaregionalId;
	private String url;
	private String uf;
	private String cidade;
	private String bairro;
	private String logradouro;
	private String numero;
	private String cep;
	private String endereco;
	private Double latitude;
	private Double longitude;
	private List<Vaga> vagas;
	
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
	public String getNome() {
		return nome;
	}
	public void setNome(String nome) {
		this.nome = nome;
	}
	public String getCodigo() {
		return codigo;
	}
	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}
	public String getTelPrincipal() {
		return telPrincipal;
	}
	public void setTelPrincipal(String telPrincipal) {
		this.telPrincipal = telPrincipal;
	}
	public long getTipoUnidadeId() {
		return tipoUnidadeId;
	}
	public void setTipoUnidadeId(long tipoUnidadeId) {
		this.tipoUnidadeId = tipoUnidadeId;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public long getRegiaoUnidadeId() {
		return regiaoUnidadeId;
	}
	public void setRegiaoUnidadeId(long regiaoUnidadeId) {
		this.regiaoUnidadeId = regiaoUnidadeId;
	}
	public long getDiretoriaregionalId() {
		return diretoriaregionalId;
	}
	public void setDiretoriaregionalId(long diretoriaregionalId) {
		this.diretoriaregionalId = diretoriaregionalId;
	}
	public String getUrl() {
		return url;
	}
	public void setUrl(String url) {
		this.url = url;
	}
	public String getUf() {
		return uf;
	}
	public void setUf(String uf) {
		this.uf = uf;
	}
	public String getCidade() {
		return cidade;
	}
	public void setCidade(String cidade) {
		this.cidade = cidade;
	}
	public String getBairro() {
		return bairro;
	}
	public void setBairro(String bairro) {
		this.bairro = bairro;
	}
	public String getLogradouro() {
		return logradouro;
	}
	public void setLogradouro(String logradouro) {
		this.logradouro = logradouro;
	}
	public String getNumero() {
		return numero;
	}
	public void setNumero(String numero) {
		this.numero = numero;
	}
	public String getCep() {
		return cep;
	}
	public void setCep(String cep) {
		this.cep = cep;
	}
	public String getEndereco() {
		return endereco;
	}
	public void setEndereco(String endereco) {
		this.endereco = endereco;
	}
	public Double getLatitude() {
		return latitude;
	}
	public void setLatitude(Double latitude) {
		this.latitude = latitude;
	}
	public Double getLongitude() {
		return longitude;
	}
	public void setLongitude(Double longitude) {
		this.longitude = longitude;
	}
	public List<Vaga> getVagas() {
		return vagas;
	}
	public void setVagas(List<Vaga> vagas) {
		this.vagas = vagas;
	}

}
