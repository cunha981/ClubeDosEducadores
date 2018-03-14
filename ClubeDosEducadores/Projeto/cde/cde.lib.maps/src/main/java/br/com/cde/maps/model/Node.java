package br.com.cde.maps.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Node {
	private String text;
	private double value;
	
	public String getText() {
		return text;
	}
	public void setText(String text) {
		this.text = text;
	}
	public double getValue() {
		return value;
	}
	public void setValue(double value) {
		this.value = value;
	}
}
