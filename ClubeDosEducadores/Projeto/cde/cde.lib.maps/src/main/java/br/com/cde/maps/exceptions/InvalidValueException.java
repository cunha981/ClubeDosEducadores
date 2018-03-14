package br.com.cde.maps.exceptions;

public class InvalidValueException extends Exception{

	private static final long serialVersionUID = 1L;
	
	public InvalidValueException() {
		super("Invalid value");
	}
	
	public InvalidValueException(String value) {
		super(value + " is invalid");
	}

}