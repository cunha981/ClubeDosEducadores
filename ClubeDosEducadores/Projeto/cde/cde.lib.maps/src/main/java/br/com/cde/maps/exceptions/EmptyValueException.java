package br.com.cde.maps.exceptions;

public class EmptyValueException extends Exception{

	private static final long serialVersionUID = 1L;
	
	public EmptyValueException() {
		super("Value Cannot be empty");
	}
	
	public EmptyValueException(String value) {
		super(value + " cannot be empty");
	}
}
