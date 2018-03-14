package br.com.cde.maps.exceptions;

public class LimitExceededException extends Exception{

	private static final long serialVersionUID = 1L;
	
	public LimitExceededException() {
		super("Value exceeded the limit");
	}
	
	public LimitExceededException(String value, int limit) {
		super(value + " cannot exceed " + limit);
	}

}
