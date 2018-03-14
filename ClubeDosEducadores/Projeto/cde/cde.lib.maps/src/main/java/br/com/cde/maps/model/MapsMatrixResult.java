package br.com.cde.maps.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class MapsMatrixResult {
	private String[] destination_addresses;
	private String[] origin_addresses;
	private Row[] rows;
	private String status;

	public String[] getDestination_addresses() {
		return destination_addresses;
	}

	public void setDestination_addresses(String[] destination_addresses) {
		this.destination_addresses = destination_addresses;
	}

	public String[] getOrigin_addresses() {
		return origin_addresses;
	}

	public void setOrigin_addresses(String[] origin_addresses) {
		this.origin_addresses = origin_addresses;
	}

	public Row[] getRows() {
		return rows;
	}

	public void setRows(Row[] rows) {
		this.rows = rows;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public StatusEnum GetStatusEnum() {
		switch (status) {
		case "OK":
			return StatusEnum.OK;
		case "INVALID_REQUEST":
			return StatusEnum.INVALID_REQUEST;
		case "MAX_ELEMENTS_EXCEEDED":
			return StatusEnum.MAX_ELEMENTS_EXCEEDED;
		case "OVER_QUERY_LIMIT":
			return StatusEnum.OVER_QUERY_LIMIT;
		case "REQUEST_DENIED":
			return StatusEnum.REQUEST_DENIED;
		case "UNKNOWN_ERROR":
			return StatusEnum.UNKNOWN_ERROR;
		default:
			return null;
		}
	}
}
