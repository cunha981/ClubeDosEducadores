package br.com.cde.lib.minerador.exception;

import java.util.List;

public class BusinessException extends Exception{
	private static final long serialVersionUID = 1L;
	
	private List<String> erros;
	
	public BusinessException(String msg,List<String> erros){
		super(msg);
		this.erros = erros;
	}
	public List<String> getErros() {
		return erros;
	}
}
