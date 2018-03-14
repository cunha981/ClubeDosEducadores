package br.com.cde.maps.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Element {
	private Node distance;
	private Node duration;
	private Node fare;
	private String status;
	
	public Node getDistance() {
		return distance;
	}
	public void setDistance(Node distance) {
		this.distance = distance;
	}
	public Node getDuration() {
		return duration;
	}
	public void setDuration(Node duration) {
		this.duration = duration;
	}
	public Node getFare() {
		return fare;
	}
	public void setFare(Node fare) {
		this.fare = fare;
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
