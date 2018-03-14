package br.com.cde.maps.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Row {
	private Element[] elements;

	public Element[] getElements() {
		return elements;
	}

	public void setElements(Element[] elements) {
		this.elements = elements;
	}
}
