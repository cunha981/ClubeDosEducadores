package br.com.cde.lib.smesp.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Unidade {
	private AdministrativeSuperior AdministrativeSuperior;
	private String UrlMoreInfo;
	private Address Address;
	private City City;
	private String Phone;
	private String Email;
	private String Uae_numero;
	private long Id;
	private String Code;
	private String Name;
	private String AdministrativeUnitTypeId;
	private String AdministrativeUnitSuperiorId;
	private String Latitude;
	private String Longitude;
	
	@JsonProperty("AdministrativeSuperior")
	public AdministrativeSuperior getAdministrativeSuperior() {
		return AdministrativeSuperior;
	}
	public void setAdministrativeSuperior(AdministrativeSuperior administrativeSuperior) {
		AdministrativeSuperior = administrativeSuperior;
	}
	@JsonProperty("UrlMoreInfo")
	public String getUrlMoreInfo() {
		return UrlMoreInfo;
	}
	public void setUrlMoreInfo(String urlMoreInfo) {
		UrlMoreInfo = urlMoreInfo;
	}
	@JsonProperty("Address")
	public Address getAddress() {
		return Address;
	}
	public void setAddress(Address address) {
		Address = address;
	}
	
	
	@JsonProperty("City")
	public City getCity() {
		return City;
	}
	public void setCity(City city) {
		City = city;
	}
	
	@JsonProperty("Phone")
	public String getPhone() {
		return Phone;
	}
	public void setPhone(String phone) {
		Phone = phone;
	}
	@JsonProperty("Email")
	public String getEmail() {
		return Email;
	}
	public void setEmail(String email) {
		Email = email;
	}
	@JsonProperty("Uae_numero")
	public String getUae_numero() {
		return Uae_numero;
	}
	public void setUae_numero(String uae_numero) {
		if(uae_numero != null && !uae_numero.isEmpty()) {
			Uae_numero = uae_numero.trim();
			return;
		}
		
		Uae_numero = uae_numero;
	}
	@JsonProperty("Id")
	public long getId() {
		return Id;
	}
	public void setId(long id) {
		Id = id;
	}
	@JsonProperty("Code")
	public String getCode() {
		return Code;
	}
	public void setCode(String code) {
		Code = code;
	}
	@JsonProperty("Name")
	public String getName() {
		return Name;
	}
	public void setName(String name) {
		Name = name;
	}
	@JsonProperty("AdministrativeUnitTypeId")
	public String getAdministrativeUnitTypeId() {
		return AdministrativeUnitTypeId;
	}
	public void setAdministrativeUnitTypeId(String administrativeUnitTypeId) {
		AdministrativeUnitTypeId = administrativeUnitTypeId;
	}
	@JsonProperty("AdministrativeUnitSuperiorId")
	public String getAdministrativeUnitSuperiorId() {
		return AdministrativeUnitSuperiorId;
	}
	public void setAdministrativeUnitSuperiorId(String administrativeUnitSuperiorId) {
		AdministrativeUnitSuperiorId = administrativeUnitSuperiorId;
	}
	@JsonProperty("Latitude")
	public String getLatitude() {
		return Latitude;
	}
	public void setLatitude(String latitude) {
		Latitude = latitude;
	}
	@JsonProperty("Longitude")
	public String getLongitude() {
		return Longitude;
	}
	public void setLongitude(String longitude) {
		Longitude = longitude;
	}
	
}
