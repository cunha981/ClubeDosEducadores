package br.com.cde.lib.smesp.core;

import java.util.List;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.core.GenericType;
import javax.ws.rs.core.MediaType;

import br.com.cde.lib.smesp.model.Unidade;

public class UnidadeService {
	private static final String REST_URI 
    = "http://portal.sme.prefeitura.sp.gov.br///School/List?AdministrativeUnitSuperiorId=&AdministrativeUnitTypeId=&port=:80&schoolName=";

  private Client client = ClientBuilder.newClient();

  public List<Unidade> findUnidade(String nome) throws Exception {
	  
	  if(nome == null) {
		  throw new Exception("Nome da unidade não pode ser null");
	  }
	  
      return client
    		  	.target(REST_URI + replaceSpacesToUriFormat(nome))
		        .request(MediaType.APPLICATION_JSON)
		        .header("PageSize", "500")
		        .header("CurrentPage", "0")
		        .get(new GenericType<List<Unidade>>(){});
  }
  
  public List<Unidade> findUnidade(String nome, Integer pagina) throws Exception {
	  if(nome == null) {
		  throw new Exception("Nome da unidade não pode ser null");
	  }
	  
      return client
    		  	.target(REST_URI + replaceSpacesToUriFormat(nome))
		        .request(MediaType.APPLICATION_JSON)
		        .header("PageSize", "500")
		        .header("CurrentPage", pagina == null ? "0" : pagina.toString())
		        .get(new GenericType<List<Unidade>>(){});
  }
  
  private String replaceSpacesToUriFormat(String value)
  {
	  return value.replace(" ", "%20");
		  
  }
}
