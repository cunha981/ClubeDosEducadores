package br.com.cde.lib.minerador.business;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.cde.lib.minerador.dao.CargoDAO;

@Service
public class CargoBusiness {

	@Autowired
	private CargoDAO cargoDAO;
	
	public Integer obterCargoIdPorCodCargo(String substring) {
		return cargoDAO.findCargoIdByCodCargo(substring);
	}

}
